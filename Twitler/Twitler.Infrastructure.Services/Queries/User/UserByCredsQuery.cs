using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Services.Queries
{
    public class UserByCredsQuery : UserByEmailQuery
    {
        private readonly Guid _password;

        public UserByCredsQuery(Guid password, string email) : base(email)
        {
            _password = password;
        }

        public override IQueryable<User> Execute(IQueryable<User> data)
        {
            return base.Execute(data.Where(u => u.Password == _password));
        }
    }
}
