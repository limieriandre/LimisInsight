using Microsoft.AspNetCore.Mvc;
using LimisInsight.Models; // Lembre-se de incluir a namespace do seu modelo.
using Microsoft.EntityFrameworkCore;

namespace LimisInsight.Controllers
{
    [Route("ListaApontamento")]
    public class ListaApontamentoController : Controller
    {
        private readonly LocalDbContext _localDbContext;

        public ListaApontamentoController(LocalDbContext localDbContext)
        {
            _localDbContext = localDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("LoadLocalData")]
        public IActionResult LoadLocalApontamentoData()
        {
            var localData = _localDbContext.ListaApontamentos.ToList();
            return Json(localData);
        }

        [HttpGet("GeraApontamentos")]
        public async Task<IActionResult> GeraApontamentos()
        {
            // Obtendo as configurações da tabela `parametrosConfigs`
            var dataInicial = DateTime.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "DataInicial").ParametroValor);
            var dataFinal = DateTime.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "DataFinal").ParametroValor);

            var horaMinimaA = int.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMinA").ParametroValor);
            var horaMaximaA = int.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMaxA").ParametroValor);
            var horaMinimaB = int.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMinB").ParametroValor);
            var horaMaximaB = int.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMaxB").ParametroValor);
            var horaMinimaC = int.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMinC").ParametroValor);
            var horaMaximaC = int.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMaxC").ParametroValor);

            // Carregar todos os registros justificados primeiro
            var datasJustificadas = await _localDbContext.DatasJustificadas.ToListAsync();

            var usuariosCondicionalA = await _localDbContext.TeamUsers.Where(usuario => usuario.TeamId == 10137).Select(usuario => usuario.UserName).ToListAsync();
            var usuariosCondicionalB = await _localDbContext.TeamUsers.Where(usuario => usuario.TeamId == 10189).Select(usuario => usuario.UserName).ToListAsync();

            // Identificar usuários em múltiplos times de controle "B" e "C"
            var usuariosMultiTime = await _localDbContext.TeamUsers
                                                        .Where(usuario => usuario.TeamId == 10137 || usuario.TeamId == 10189)
                                                        .GroupBy(usuario => usuario.UserId)
                                                        .Where(grupo => grupo.Count() > 1)
                                                        .Select(grupo => grupo.Key)
                                                        .ToListAsync();

            // Carregando os registros necessários em memória
            var registros = await _localDbContext.TimeEntries
                                                 .Where(registro => registro.DateAt >= dataInicial && registro.DateAt <= dataFinal)
                                                 .ToListAsync();


            var resultado = (from registro in registros
                             join usuario in _localDbContext.TeamUsers on registro.MembersUserId equals usuario.UserId
                             where !(usuariosMultiTime.Contains(registro.MembersUserId) && usuario.TeamName == "Time \"C\"")
                             group registro by new { registro.MembersUserId, registro.MembersUserName, registro.DateAt, usuario.TeamName } into grupo
                             select new
                             {
                                 UserId = grupo.Key.MembersUserId,
                                 UserName = grupo.Key.MembersUserName,
                                 DateAt = grupo.Key.DateAt,
                                 Time = grupo.Key.TeamName,
                                 Soma = grupo.Sum(registro => registro.DurationHour),
                                 Status = usuariosCondicionalA.Contains(grupo.Key.MembersUserName)
                                         ? (grupo.Sum(registro => registro.DurationHour) >= horaMinimaB && grupo.Sum(registro => registro.DurationHour) <= horaMaximaB ? "OK" : "INC")
                                         : usuariosCondicionalB.Contains(grupo.Key.MembersUserName)
                                             ? (grupo.Sum(registro => registro.DurationHour) >= horaMinimaC && grupo.Sum(registro => registro.DurationHour) <= horaMaximaC ? "OK" : "INC")
                                             : (grupo.Sum(registro => registro.DurationHour) >= horaMinimaA && grupo.Sum(registro => registro.DurationHour) <= horaMaximaA ? "OK" : "INC")
                             })
                            .OrderBy(registro => registro.UserName)
                            .ThenBy(registro => registro.DateAt)
                            .ToList();

            // Processando a lógica de validação das datas justificadas:
            var resultadoFinal = resultado.Select(registro =>
            {
                if (datasJustificadas.Any(dj => dj.MembersUserId == registro.UserId && dj.Data == registro.DateAt))
                {
                    return new
                    {
                        registro.UserId,
                        registro.UserName,
                        registro.DateAt,
                        registro.Time,
                        registro.Soma,
                        Status = "OK"
                    };
                }
                return registro;
            }).ToList();


            return Ok(resultadoFinal);
        }

    }
}
