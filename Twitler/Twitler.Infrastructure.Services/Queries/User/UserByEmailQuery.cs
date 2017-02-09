using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;

namespace Twitler.Services.Queries
{
    public class UserByEmailQuery:IQuery<User>
    {
        private readonly string _email;

        public UserByEmailQuery(string email)
        {
            _email = email;
        }

        public virtual IQueryable<User> Execute(IQueryable<User> data)
        {
            return data.Where(u => u.Email == _email);
        }
    }
}
