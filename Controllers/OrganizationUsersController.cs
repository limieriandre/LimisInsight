using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LimisInsight.Controllers
{
    [Route("OrganizationUsers")]
    public class OrganizationUsersController : Controller
    {
        private readonly OrigemDbContext _origemDbContext;
        private readonly LocalDbContext _localDbContext;

        public OrganizationUsersController(OrigemDbContext origemDbContext, LocalDbContext localDbContext)
        {
            _origemDbContext = origemDbContext;
            _localDbContext = localDbContext;
        }

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona
        public async Task<IActionResult> Index()
#pragma warning restore CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona
        {
            return View();
        }

        [HttpGet("GetOrganizationUsers")]
        public async Task<IActionResult> GetOrganizationUsers()
        {
            var users = await _origemDbContext.OrganizationUsers.ToListAsync();
            return Json(users);
        }

        [HttpGet("LoadLocalData")]
        public IActionResult LoadLocalData()
        {
            var localData = _localDbContext.OrganizationUsers.ToList();
            return Json(localData);
        }

        [HttpPost("CopyDataToLocalStorage")]
        public async Task<IActionResult> CopyDataToLocalStorage()
        {
            // Recuperando os dados da origem
            var dataFromSource = _origemDbContext.OrganizationUsers.ToList();

            // Verificando e excluindo/recriando a tabela no banco de dados local.
            _localDbContext.Database.ExecuteSqlRaw("DELETE FROM organization_53257_organization_users;");

            // Aplica a migração específica para criar a tabela.
            _localDbContext.Database.Migrate();  // Se a migração estiver pendente, ela será aplicada.

            // Inserindo os dados recuperados na tabela local.
            _localDbContext.OrganizationUsers.AddRange(dataFromSource);
            await _localDbContext.SaveChangesAsync();

            // Retornando o número de registros copiados.
            return Ok(dataFromSource.Count);
        }

    }
}
