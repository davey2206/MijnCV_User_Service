using Microsoft.EntityFrameworkCore;
using MijnCV_User_Service.Models;

namespace MijnCV_User_Service.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly MijnCV_User_ServiceContext _context;
        public StatisticsService(MijnCV_User_ServiceContext context)
        {
            _context = context;
        }

        public async Task<Statistics> GetStatisticsByCv(string cv)
        {
            var stat = await _context.Statistics.Where(s => s.cv == cv).FirstOrDefaultAsync();
            if (stat == null)
            {
                return new Statistics { cv = cv, Views = 0 };
            }
            else
            {
                return stat;
            }
        }

        public async Task UpdateViewCountByCv(string cv)
        {
            var stat = await _context.Statistics.Where(s => s.cv == cv).FirstOrDefaultAsync();

            if (stat == null)
            {
                var newStat = new Statistics { cv = cv, Views = 1 };
                _context.Statistics.Add(newStat);
                _context.SaveChanges();
                return;
            }
            else
            {
                stat.Views = stat.Views + 1;
                _context.Entry(stat).State = EntityState.Modified;
                _context.SaveChanges();

                return;
            }
        }
    }
}
