using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<Twit> GetByHashTags(int[] hashValues)
        {
            return _context.Twits.Where(t => t.HashTags.Any(ht => hashValues.Contains(ht.HashValue)))
                .Include(t => t.HashTags).ToList();
        }

        public void Add(Twit twit)
        {
            if (twit != null)
            {
                AttachHashTags(twit.HashTags);
               
                //insert twit
                _context.Twits.Add(twit);
                _context.SaveChanges();
            }
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
