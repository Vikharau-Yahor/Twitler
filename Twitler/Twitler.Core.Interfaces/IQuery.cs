using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitler.Domain.Interfaces
{
    public interface IQuery<T> where T : class
    {
        IQueryable<T> Execute(IQueryable<T> data);
    }
}
