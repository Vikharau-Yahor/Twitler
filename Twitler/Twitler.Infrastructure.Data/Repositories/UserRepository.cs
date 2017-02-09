using System;
using System.Data.Entity;
using System.Linq;
using NLog;
using Twitler.Data.Context;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ITwitlerContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public UserRepository(ITwitlerContext context)
        {
            _context = context;
        }

        public IQueryable<User> Get(IQuery<User> query)
        {
            return query.Execute(_context.Users);
        }

        public void Add(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                logger.Error(
                    $"Add method: error occurred when new user saving to database. Exception: {exception.Message}. StackTrace: {exception.StackTrace}");
            }
        }

        public void Delete(User user)
        {
            if (user == null)
            {
                logger.Warn($"Delete method: user object is null. This method will execute nothing");
                return;
            }

            try
            {
                _context.Entry(user).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                logger.Error($"Delete method: error occurred when user removing from database. Exception: {exception.Message}. StackTrace: {exception.StackTrace}");
            }
        }
    }
}
