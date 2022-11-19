using AutoMapper;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.Concretes
{
    public class CompanyServices : ICompanyService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IMapper _mapper;
        private readonly IDapperRepository<Company> _dapperRepository;

        public CompanyServices(IRepository<Company> companyRepository, IMapper mapper, IDapperRepository<Company> dapperRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _dapperRepository = dapperRepository;

        }

        public async Task<IReadOnlyList<Company>> GetAll()
        {
            return await _dapperRepository.GetAll();
        }
        public async Task<Company> Get(int id)
        {
            return await _dapperRepository.GetById(id);
        }

        public async Task<int> Add(Company company)
        {
            return await _dapperRepository.Add(company);
        }

        public async Task<int> Update(Company company, int id)
        {
            return await _dapperRepository.Update(company, id);
        }

        public async Task<int> Delete(int id)
        {
            return await _dapperRepository.DeleteById(id);
        }
    }
}
