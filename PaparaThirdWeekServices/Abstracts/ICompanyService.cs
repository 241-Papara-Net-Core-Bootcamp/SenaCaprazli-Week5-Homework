using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.Abstracts
{
    public interface ICompanyService
    {
        Task<IReadOnlyList<Company>> GetAll();
        Task<Company> Get(int id);
        Task<int> Add(Company company);
        Task<int> Update(Company company, int id);
        Task<int> Delete(int id);


    }
}
