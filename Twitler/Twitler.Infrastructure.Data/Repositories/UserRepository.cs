using System;
using System.Data.Entity;
using System.Linq;
using Twitler.Data.Context;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private ITwitlerContext _context;

        public UserRepository(ITwitlerContext context)
        {
            _context = context;
        }

        public IQueryable<User> Get(IQuery<User> query)
        {
            return query.Execute(_context.Users);
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
