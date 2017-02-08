using System;
using System.Collections.Generic;
using System.Linq;
using Twitler.Data.Context;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Data.Repositories
{
    public class TwitRepository : ITwitRepository
    {
        private ITwitlerContext _context;

        public TwitRepository(ITwitlerContext context)
        {
            _context = context;
        }

        public List<Twit> GetAll()
        {
            return _context.Twits.ToList();
        }

        public void Add(string userEmail, Twit twit)
        {
            if (userEmail == null || twit == null)
                return;

            var user = _context.Users.SingleOrDefault(u => u.Email == userEmail);
            if (user != null)
            {
                user.Twits.Add(twit);
                _context.SaveChanges();
            }
        }
    }
}
