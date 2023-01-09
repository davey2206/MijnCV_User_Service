using MijnCV_User_Service.Models;

namespace MijnCV_User_Service.Services
{
    public interface IStatisticsService
    {
        public Task<Statistics> GetStatisticsByCv(string cv);
        public Task UpdateViewCountByCv(string cv);
    }
}
