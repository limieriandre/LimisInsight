using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LimisInsight.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LimisInsight.Controllers
{
    [Route("TimeEntries")]
    public class TimeEntriesController : Controller
    {
        private readonly OrigemDbContext _origemDbContext;
        private readonly LocalDbContext _localDbContext;

        public TimeEntriesController(OrigemDbContext origemDbContext, LocalDbContext localDbContext)
        {
            _origemDbContext = origemDbContext;
            _localDbContext = localDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("GetTimeEntries")]
        public async Task<IActionResult> GetTimeEntries(DateTime startDate, DateTime endDate)
        {
            var dataNull = DateTime.MinValue;
            if (startDate == dataNull)
            {
                Console.WriteLine("teste1");
                startDate = DateTime.MinValue;
            }
            if (endDate == dataNull)
            {
                Console.WriteLine("teste2");
                endDate = DateTime.MaxValue;
            }
            var entries = await _origemDbContext.TimeEntries
                                   .Where(te => te.DateAt >= startDate && te.DateAt <= endDate)
                                   .ToListAsync();
            return Json(entries);
        }


        [HttpGet("LoadLocalData")]
        public async Task<IActionResult> LoadLocalData(DateTime startDate, DateTime endDate)
        {
            var dataNull = DateTime.MinValue;
            if (startDate == dataNull)
            {
                Console.WriteLine("teste1");
                startDate = DateTime.MinValue; 
            }
            if (endDate == dataNull)
            {
                Console.WriteLine("teste2");
                endDate = DateTime.MaxValue;
            }
            var localData = await _localDbContext.TimeEntries
                                   .Where(te => te.DateAt >= startDate && te.DateAt <= endDate)
                                   .ToListAsync();

            return Json(localData);
        }

        [HttpPost("CopyDataToLocalStorage")]
        public async Task<IActionResult> CopyDataToLocalStorage(DateTime startDate, DateTime endDate)
        {
            var dataNull = DateTime.MinValue;
            if (startDate == dataNull)
            {
                Console.WriteLine("teste1");
                startDate = DateTime.MinValue;
            }
            if (endDate == dataNull)
            {
                Console.WriteLine("teste2");
                endDate = DateTime.MaxValue;
            }
            // Recuperando os dados da origem filtrados pelas datas
            var dataFromSource = _origemDbContext.TimeEntries
                                    .Where(te => te.DateAt >= startDate && te.DateAt <= endDate)
                                    .ToList();

            // Verificando e excluindo/recriando a tabela no banco de dados local.
            _localDbContext.Database.ExecuteSqlRaw("DELETE FROM organization_53257_time_entries_v2;");

            // Aplica a migração específica para criar a tabela.
            _localDbContext.Database.Migrate();

            // Inserindo os dados recuperados na tabela local.
            _localDbContext.TimeEntries.AddRange(dataFromSource);
            await _localDbContext.SaveChangesAsync();

            // Retornando o número de registros copiados.
            return Ok(dataFromSource.Count);
        }
    }
}
