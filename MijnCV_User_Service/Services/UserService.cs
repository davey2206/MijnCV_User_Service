using Microsoft.EntityFrameworkCore;
using MijnCV_User_Service.Models;

namespace MijnCV_User_Service.Services
{
    public class UserService : IUserService
    {
        private readonly MijnCV_User_ServiceContext _context;
        public UserService(MijnCV_User_ServiceContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<bool> PutUser(int id, User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return Task.FromResult(true);
                }
                else
                {
                    throw;
                }
            }

            return Task.FromResult(false);
        }

        public async Task PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task<bool> DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return Task.FromResult(true);
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Task.FromResult(false);
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
