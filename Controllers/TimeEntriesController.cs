using LimisInsight.Data;
using LimisInsight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LimisInsight.Controllers.UsersController;

namespace LimisInsight.Controllers
{
    public class TimeEntriesController : Controller
    {
        private readonly LimisInsightContext _localContext;
        private readonly LimisInsightContext _originContext;

        public TimeEntriesController(IOptions<ConnectionStrings> connectionStrings)
        {
            var localOptions = new DbContextOptionsBuilder<LimisInsightContext>()
                .UseMySql(connectionStrings.Value.ArtiaLocalConnection,
                          new MySqlServerVersion(new System.Version(8, 0, 21)))
                .Options;
            _localContext = new LimisInsightContext(localOptions);

            var originOptions = new DbContextOptionsBuilder<LimisInsightContext>()
                .UseMySql(connectionStrings.Value.ArtiaOrigemConnection,
                          new MySqlServerVersion(new System.Version(8, 0, 21)))
                .Options;
            _originContext = new LimisInsightContext(originOptions);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetTimeEntries()
        {
            try
            {
                var timeEntries = _originContext.Set<TimeEntries>().AsNoTracking().ToList();
                return Json(timeEntries);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> CloneData()
        {
            const int batchSize = 1000;
            int skipAmount = 0;

            try
            {
                // Recria a tabela para mudar a estrutura de chaves primárias
                await _localContext.Database.ExecuteSqlRawAsync("DROP TABLE IF EXISTS TimeEntries;");
                await _localContext.Database.MigrateAsync();

                DateTime startDate = new DateTime(2023, 9, 1);

                while (true)
                {
                    var timeEntriesBatch = await _originContext.TimeEntries
                        .Where(te => te.DateAt >= startDate)
                        .OrderBy(te => te.Id)
                        .Skip(skipAmount)
                        .Take(batchSize)
                        .AsNoTracking()
                        .ToListAsync();

                    if (!timeEntriesBatch.Any())
                        break;

                    _localContext.TimeEntries.AddRange(timeEntriesBatch);
                    await _localContext.SaveChangesAsync();

                    skipAmount += batchSize;
                }

                return Json(new { success = true, totalCloned = skipAmount });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult DisplayLocalData()
        {
            try
            {
                var timeEntries = _localContext.Set<TimeEntries>().AsNoTracking().ToList();
                return Json(timeEntries);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
