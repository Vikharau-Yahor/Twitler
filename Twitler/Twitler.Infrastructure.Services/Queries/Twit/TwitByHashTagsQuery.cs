using System.Data.Entity;
using System.Linq;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Services.Queries
{
    public class TwitByHashTagsQuery: IQuery<Twit>
    {
        private readonly int[] _hashValues;

        public TwitByHashTagsQuery(int[] hashValues)
        {
            _hashValues = hashValues;
        }

        public IQueryable<Twit> Execute(IQueryable<Twit> data)
        {
            return data.Where(t => t.HashTags.Any(ht => _hashValues.Contains(ht.HashValue)))
                    .Include(t => t.HashTags);
        }
    }
}
