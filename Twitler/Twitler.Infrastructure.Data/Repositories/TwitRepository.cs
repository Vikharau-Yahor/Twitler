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
    }
}
