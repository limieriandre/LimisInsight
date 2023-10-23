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

            var horaMinimaA = float.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMinA").ParametroValor);
            var horaMaximaA = float.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMaxA").ParametroValor);
            var horaMinimaB = float.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMinB").ParametroValor);
            var horaMaximaB = float.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMaxB").ParametroValor);
            var horaMinimaC = float.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMinC").ParametroValor);
            var horaMaximaC = float.Parse(_localDbContext.ParametrosConfigs.FirstOrDefault(parametro => parametro.ParametroNome == "HoraMaxC").ParametroValor);

            // Carregar todos os registros justificados primeiro
            var datasJustificadas = await _localDbContext.DatasJustificadas.ToListAsync();
            var datasFeriado = await _localDbContext.DatasFeriados.ToListAsync();  // Adicionado

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
                             where !usuariosMultiTime.Contains(registro.MembersUserId)
                             && usuario.TeamId != 10189
                             && usuario.TeamId != 10137
                             && usuario.TeamId != 12318
                             && usuario.TeamId != 12317
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

            var todosMembros = await _localDbContext.TeamUsers
                        .Where(usuario => usuario.TeamId != 12336 && usuario.TeamId != 12317 && usuario.TeamId != 12318)  // Excluindo membros do time "12336" e 12317 e 12318
                        .ToListAsync();

            // 1. Gerar lista de datas
            var todasDatas = Enumerable.Range(0, 1 + dataFinal.Subtract(dataInicial).Days)
                .Select(offset => dataInicial.AddDays(offset))
                .Where(dt => dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday)
                .ToList();

            // 2. Assegurar registros para cada membro e data
            foreach (var membro in todosMembros)
            {
                foreach (var data in todasDatas)
                {
                    if (!resultado.Any(r => r.UserId == membro.UserId && r.DateAt == data))
                    {
                        resultado.Add(new
                        {
                            UserId = membro.UserId,
                            UserName = membro.UserName,
                            DateAt = data,
                            Time = membro.TeamName,
                            Soma = 0.0f,
                            Status = "INC"
                        });
                    }
                }
            }


            // Processando a lógica de validação das datas justificadas:
            var resultadoFinal = resultado.Select(registro =>
            {
                if (datasJustificadas.Any(dj => dj.MembersUserId == registro.UserId && dj.Data == registro.DateAt)
                    || datasFeriado.Any(df => df.Data == registro.DateAt))  // Verificando se a data é feriado
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

            // Ordenando por UserName e em seguida por DateAt
            resultadoFinal = resultadoFinal.OrderBy(r => r.UserName).ThenBy(r => r.DateAt).ToList();


            return Ok(resultadoFinal);
        }

        [HttpPost("GravaApontamentos")]
        public IActionResult GravaApontamentos([FromBody] List<ListaApontamento> resultadoFinal)
        {
            if (resultadoFinal == null || !resultadoFinal.Any())
            {
                return BadRequest("Nenhum registro fornecido.");
            }

            try
            {
                // Limpar registros existentes na tabela usando execução SQL direta
                _localDbContext.Database.ExecuteSqlRaw("DELETE FROM lista_apontamentos;");

                // Adicionar registros ao DbContext
                _localDbContext.ListaApontamentos.AddRange(resultadoFinal);
                _localDbContext.SaveChanges();

                return Ok($"Dados gravados com sucesso! Total: {resultadoFinal.Count} registros.");
            }
            catch (Exception ex)
            {
                // Isto é apenas um exemplo, você pode querer usar um serviço de log mais sofisticado
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Ocorreu um erro ao tentar gravar os registros.");
            }
        }

    }
}
