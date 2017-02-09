using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Services.Queries
{
    public class TwitByUserQuery : IQuery<Twit>
    {
        private readonly string _userEmail;
        private readonly int _twitId;
        public TwitByUserQuery(string userEmail, int twitId)
        {
            _userEmail = userEmail;
            _twitId = twitId;
        }
        public IQueryable<Domain.Model.Twit> Execute(IQueryable<Domain.Model.Twit> data)
        {
            return data.Where(t => t.Id == _twitId && t.User.Email == _userEmail);
        }
    }
}
