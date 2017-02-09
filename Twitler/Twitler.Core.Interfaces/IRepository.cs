using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitler.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get(IQuery<T> query);

        void Add(T entity);
        void Delete(T entity);
    }
}
