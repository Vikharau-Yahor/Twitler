using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using NLog;
using Twitler.Data.Context;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Data.Repositories
{
    public class TwitRepository : IRepository<Twit>
    {
        private readonly ITwitlerContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
            try
            {
                AttachHashTags(twit.HashTags);
            }
            catch (Exception exception)
            {
                logger.Error($"Add method: error occurred when new hash-tags saving to database. Exception: {exception.Message}. StackTrace: {exception.StackTrace}");
                return;
            }

            try
            {
                //insert twit
                _context.Twits.Add(twit);
                _context.SaveChanges();
            }
            catch(Exception exception)
            { 
                logger.Error($"Add method: error occurred when new twit-message saving to database. Exception: {exception.Message}. StackTrace: {exception.StackTrace}");
            }
        }

        public void Delete(Twit twit)
        {
            if (twit == null)
            {
                logger.Warn($"Delete method: twit-message object is null. This method will execute nothing");
                return;
            }

            try
            {
                _context.Entry(twit).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                logger.Error($"Delete method: error occurred when twit-message removing from database. Exception: {exception.Message}. StackTrace: {exception.StackTrace}");
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
