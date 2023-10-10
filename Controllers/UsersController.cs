using LimisInsight.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LimisInsight.Controllers
{
    public class UsersController : Controller
    {
        private readonly LimisInsightContext _context;

        public UsersController(LimisInsightContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListArtiaData()
        {
            // Implementar a lógica para listar dados do Artia aqui.
            return View();
        }

        public IActionResult CloneData()
        {
            // Implementar a lógica para clonar dados aqui.
            return View();
        }

        public IActionResult DisplayLocalData()
        {
            // Implementar a lógica para exibir dados locais aqui.
            return View();
        }
    }
}
