using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Twitler.Data.Context;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Data.Repositories
{
    public class TwitRepository : IRepository<Twit>
    {
        private ITwitlerContext _context;

        public TwitRepository(ITwitlerContext context)
        {
            _context = context;
        }

        public IQueryable<Twit> Get(IQuery<Twit> query)
        {
            return query.Execute(_context.Twits);
        }

        public void Add(Twit twit)
        {

            AttachHashTags(twit.HashTags);

            //insert twit
            _context.Twits.Add(twit);
            _context.SaveChanges();
        }

        public void Delete(Twit twit)
        {
            if (twit == null) return;

            _context.Entry(twit).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        private void AttachHashTags(ICollection<HashTag> hashTags)
        {
            var hashArray = hashTags.Select(ht => ht.HashValue).ToArray();
            if (hashArray.Length != 0)
            {
                var existTags = _context.HashTags.Where(ht => hashArray.Contains(ht.HashValue))
                                             .Select(ht => ht.HashValue).ToArray();
                foreach (var hashTag in hashTags)
                {
                    if (existTags.Contains(hashTag.HashValue))
                    {
                        _context.Entry(hashTag).State = EntityState.Unchanged;
                    }
                    else
                    {
                        _context.Entry(hashTag).State = EntityState.Added;
                    }
                }
            }
        }

        
    }
}
