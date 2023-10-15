using LimisInsight.Data;
using LimisInsight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace LimisInsight.Controllers
{
    public class UsersController : Controller
    {
        private readonly LimisInsightContext _localContext;
        private readonly LimisInsightContext _originContext;

        public UsersController(IOptions<ConnectionStrings> connectionStrings)
        {
            var localOptions = new DbContextOptionsBuilder<LimisInsightContext>()
                .UseMySql(connectionStrings.Value.ArtiaLocalConnection,
                          new MySqlServerVersion(new Version(8, 0, 21)))
                .Options;
            _localContext = new LimisInsightContext(localOptions);

            var originOptions = new DbContextOptionsBuilder<LimisInsightContext>()
                .UseMySql(connectionStrings.Value.ArtiaOrigemConnection,
                          new MySqlServerVersion(new Version(8, 0, 21)))
                .Options;
            _originContext = new LimisInsightContext(originOptions);
        }

        public IActionResult Index()
        {

            // Renderiza a view sem dados.
            return View();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            // Obtenha os dados do usuário do banco de dados do Artia (Origem).
            var users = _originContext.Set<User>().ToList();

            // Retorna os dados como JSON.
            return Json(users);
        }

        public class ConnectionStrings
        {
            public string ArtiaOrigemConnection { get; set; }
            public string ArtiaLocalConnection { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> CloneData()
        {
            int recordsCloned = 0;

            try
            {
                // Limpe a tabela local
                _localContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS organization_53257_teams_users_v2");
                _localContext.Database.ExecuteSqlRaw("/* SQL para criar a tabela organization_53257_teams_users_v2 novamente, com a chave primária composta */");

                var usersFromOrigin = _originContext.Users.ToList();

                // Adicione registros à base local
                _localContext.Users.AddRange(usersFromOrigin);
                await _localContext.SaveChangesAsync();

                recordsCloned = usersFromOrigin.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                Console.WriteLine(ex.Message); // Você pode ajustar isso para logar o erro como desejar
                return Json(new { success = false });
            }

            return Json(new { success = true, records = recordsCloned });
        }

        [HttpGet]
        public IActionResult DisplayLocalData()
        {
            // Obtenha os dados do usuário do banco de dados local
            var users = _localContext.Users.ToList();

            // Retorna os dados como JSON.
            return Json(users);
        }

    }
}
