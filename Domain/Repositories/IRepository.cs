using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(string id);
        void Delete(T entity);
        IEnumerable<T> Find(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        T GetById(string id);
    }
}
