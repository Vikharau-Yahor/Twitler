using System.Linq;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Services.Queries
{
    public class TwitAllQuery:IQuery<Twit>
    {
        public IQueryable<Twit> Execute(IQueryable<Twit> data)
        {
            return data;
        }
    }
}
