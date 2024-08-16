using System;
using System.Collections.Generic;
using System.Linq;

namespace refactoring.bl
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get(); // or

        IReadOnlyCollection<T> Get(Func<T, bool> where);

        IReadOnlyCollection<T> GetAll();

        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        T FindById(int Id);
    }
}