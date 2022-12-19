using MijnCV_User_Service.Models;
using MijnCV_User_Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MijnCV_User_Service.test.Services
{
    public class UserServiceFake : IUserService
    {
        private readonly List<User> _user;

        public UserServiceFake()
        {
            _user = new List<User>()
            {
                new User { Id = 1, CV = "TestCV1", SubID = 12345},
                new User { Id = 2, CV = "TestCV2", SubID = 54321}
            };
        }

        public Task<bool> DeleteUser(int id)
        {
            var user = _user.First(p => p.Id == id);
            if (user == null)
            {
                return Task.FromResult(true);
            }

            _user.Remove(user);

            return Task.FromResult(false);
        }

        public Task<User?> GetUser(int id)
        {
            var user = _user.Where(p => p.Id == id).FirstOrDefault();
            return Task.FromResult(user);
        }

        public Task<List<User>> GetUsers()
        {
            return Task.FromResult(_user);
        }

        public bool UserExists(int id)
        {
            return _user.Any(p => p.Id == id);
        }

        public Task PostUser(User user)
        {
            user.Id = _user.Last().Id + 1;
            _user.Add(user);
            return Task.FromResult(user);
        }

        public Task<bool> PutUser(int id, User user)
        {
            if (!UserExists(id))
            {
                return Task.FromResult(true);
            }

            _user.First(p => p.Id == id).CV = user.CV;
            _user.First(p => p.Id == id).SubID = user.SubID;

            return Task.FromResult(false);
        }
    }
}
