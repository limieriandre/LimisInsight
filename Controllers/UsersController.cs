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

        public async Task<IActionResult> CloneData()
        {
            var originUsers = _originContext.Users.ToList();

            // Aqui você pode verificar se os dados precisam ser atualizados ou simplesmente adicionar todos. 
            // Para simplicidade, estou adicionando todos os dados:

            _localContext.Users.AddRange(originUsers);
            await _localContext.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
