using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Data.Abstracts
{
 public interface IDapperRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<int> Add(T entity);
        Task<int> Update(T entity, int id);
        Task<int> DeleteById(int id);
    }
}
