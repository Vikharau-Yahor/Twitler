using System;
using System.Linq;
using Twitler.Data.Context;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ITwitlerContext _context;

        public UserRepository(ITwitlerContext context)
        {
            _context = context;
        }

        public User Find(string email, Guid password)
        {
            User result = null;

            if (email != null)
            {
                result = _context.Users.SingleOrDefault(u => u.Email == email &&
                                                             u.Password == password);
            }

            return result;
        }
    }
}
