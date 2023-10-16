using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LimisInsight.Controllers
{
    [Route("TeamUsers")]
    public class TeamUsersController : Controller
    {
        private readonly OrigemDbContext _origemDbContext;
        private readonly LocalDbContext _localDbContext;

        public TeamUsersController(OrigemDbContext origemDbContext, LocalDbContext localDbContext)
        {
            _origemDbContext = origemDbContext;
            _localDbContext = localDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("GetTeamUsers")]
        public async Task<IActionResult> GetTeamUsers()
        {
            var users = await _origemDbContext.TeamUsers.ToListAsync();
            return Json(users);
        }

        [HttpGet("LoadLocalData")]
        public IActionResult LoadLocalTeamData()
        {
            var localData = _localDbContext.TeamUsers.ToList();
            return Json(localData);
        }

        [HttpPost("CopyDataToLocalStorage")]
        public async Task<IActionResult> CopyTeamDataToLocalStorage()
        {
            // Recuperando os dados da origem
            var dataFromSource = _origemDbContext.TeamUsers.ToList();

            // Verificando e excluindo/recriando a tabela no banco de dados local.
            _localDbContext.Database.ExecuteSqlRaw("DELETE FROM organization_53257_teams_users;");

            // Aplica a migração específica para criar a tabela.
            _localDbContext.Database.Migrate();  // Se a migração estiver pendente, ela será aplicada.

            // Inserindo os dados recuperados na tabela local.
            _localDbContext.TeamUsers.AddRange(dataFromSource);
            await _localDbContext.SaveChangesAsync();

            // Retornando o número de registros copiados.
            return Ok(dataFromSource.Count);
        }
    }
}
