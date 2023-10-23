using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LimisInsight.Models;

namespace LimisInsight.Controllers
{
    [Route("DataFeriado")]
    public class DataFeriadoController : Controller
    {
        private readonly LocalDbContext _localDbContext;

        public DataFeriadoController(LocalDbContext localDbContext)
        {
            _localDbContext = localDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("LoadLocalFeriados")]
        public IActionResult LoadLocalFeriados()
        {
            var localFeriados = _localDbContext.DatasFeriados.ToList();
            return Json(localFeriados);
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("Save")]
        public IActionResult Save(DataFeriado dataFeriado)
        {
            if (ModelState.IsValid)
            {
                _localDbContext.DatasFeriados.Add(dataFeriado);
                _localDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Add", dataFeriado);
        }
    }
}
