using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LimisInsight.Models;

namespace LimisInsight.Controllers
{
    [Route("DataJustificada")]
    public class DataJustificadaController : Controller
    {
        private readonly LocalDbContext _localDbContext;

        public DataJustificadaController(LocalDbContext localDbContext)
        {
            _localDbContext = localDbContext;
        }

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona
        public async Task<IActionResult> Index()
#pragma warning restore CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona
        {
            return View();
        }

        [HttpGet("LoadLocalJustificativas")]
        public IActionResult LoadLocalJustificativas()
        {
            var localJustificativas = _localDbContext.DatasJustificadas.ToList();
            return Json(localJustificativas);
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("Save")]
        public IActionResult Save(DataJustificada dataJustificada)
        {
            if (ModelState.IsValid)
            {
                _localDbContext.DatasJustificadas.Add(dataJustificada);
                _localDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Add", dataJustificada);
        }
    }
}
