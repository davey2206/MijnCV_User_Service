using Microsoft.EntityFrameworkCore;
using MijnCV_User_Service.Models;
using MijnCV_User_Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MijnCV_User_Service.test.Services
{
    public class StatisticsServiceFake : IStatisticsService
    {
        private readonly List<Statistics> _statistics;

        public StatisticsServiceFake()
        {
            _statistics = new List<Statistics>()
            {
                new Statistics() { Id = 1, cv="test", Views = 5}
            };
        }
        public Task<Statistics> GetStatisticsByCv(string cv)
        {
            var stat = _statistics.Where(s => s.cv == cv).FirstOrDefault();
            if (stat == null)
            {
                return Task.FromResult(new Statistics { cv = cv, Views = 0 });
            }
            else
            {
                return Task.FromResult(stat);
            }
        }

        public Task UpdateViewCountByCv(string cv)
        {
            var stat = _statistics.Where(s => s.cv == cv).FirstOrDefault();

            if (stat == null)
            {
                var id = _statistics.Count() + 1;
                var newStat = new Statistics {Id = id, cv = cv, Views = 1 };
                _statistics.Add(newStat);
                return Task.CompletedTask;
            }
            else
            {
                _statistics.Where(s => s.cv == cv).First().Views = stat.Views + 1;

                return Task.CompletedTask;
            }
        }
    }
}
