using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LimisInsight.Models;

namespace LimisInsight.Controllers
{
    [Route("Parametros")]
    public class ParametrosConfigsController : Controller
    {
        private readonly LocalDbContext _localDbContext;

        public ParametrosConfigsController(LocalDbContext context)
        {
            _localDbContext = context;
        }

        // GET: ParametrosConfigs
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("GetParametrosData")]
        public IActionResult GetParametros()
        {
            var parametros = _localDbContext.ParametrosConfigs.ToList();
            return Ok(parametros);
        }

        [HttpPost("UpdateParametros")]
        public IActionResult UpdateParametros(List<ParametroUpdateModel> parametros)
        {
            foreach (var parametro in parametros)
            {
                var dbParametro = _localDbContext.ParametrosConfigs.Find(parametro.Id);
                if (dbParametro != null)
                {
                    dbParametro.ParametroValor = parametro.Valor;
                    dbParametro.DataAtualizacao = DateTime.Now;
                    _localDbContext.ParametrosConfigs.Update(dbParametro);
                }
            }
            _localDbContext.SaveChanges();
            return Ok(new { message = "Parâmetros atualizados com sucesso!" });
        }

        public class ParametroUpdateModel
        {
            public int Id { get; set; }
            public string Valor { get; set; }
        }

        [HttpGet("AddParametro")]
        public IActionResult AddParametro()
        {
            return View();  // Isso exibirá a view acima.
        }

        [HttpPost("AddParametro")]
        public async Task<IActionResult> AddParametro(ParametrosConfig parametro)
        {
            if (ModelState.IsValid)
            {
                _localDbContext.ParametrosConfigs.Add(parametro);
                await _localDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(parametro);
        }



    }
}
