using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MijnCV_User_Service.Models;
using MijnCV_User_Service.Services;

namespace MijnCV_User_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _StatisticsService;

        public StatisticsController(IStatisticsService service)
        {
            _StatisticsService = service;
        }

        // GET: api/Users/5
        [HttpGet("{cv}")]
        public async Task<ActionResult<Statistics>> GetStatistics(string cv)
        {
            var statistic = await _StatisticsService.GetStatisticsByCv(cv);

            if (statistic == null)
            {
                return NotFound();
            }

            return statistic;
        }

        [HttpPost("ViewCount/{cv}")]
        public async Task UpdateViewCountByCv(string cv)
        {
            await _StatisticsService.UpdateViewCountByCv(cv);
        }

    }
}
