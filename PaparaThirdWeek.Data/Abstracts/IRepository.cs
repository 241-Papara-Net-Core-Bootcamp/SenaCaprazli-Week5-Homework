using PaparaThirdWeek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Text;

namespace PaparaThirdWeek.Data.Abstracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get();
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Remove(int id);
        void HardRemove(T entity);

    }
}
