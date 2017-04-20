using Domain.Exceptions;
using Domain.Models.Products;
using Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FileRepository<T> : IRepository<T> where T : class
    {
        readonly string filePath;

        public FileRepository(string filePath)
        {
            this.filePath = filePath;  
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            var list = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath));
            if (list == null)
                return new List<T>();
            else
                return list;
        }

        public T GetById(string id)
        {
            T obj = GetAll().SingleOrDefault(x => (string)x.GetType().GetProperty("Id").GetValue(x) == id);

            if (obj == null)
                throw new ProductException("No product is found.");
            else
                return obj;
        }
    }
}
