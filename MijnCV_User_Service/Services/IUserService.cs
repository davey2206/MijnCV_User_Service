using MijnCV_User_Service.Models;

namespace MijnCV_User_Service.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();
        public Task<User?> GetUser(int id);
        public Task<bool> PutUser(int id, User user);
        public Task PostUser(User user);
        public Task<bool> DeleteUser(int id);
        public bool UserExists(int id);
    }
}
