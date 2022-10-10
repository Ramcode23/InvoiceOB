using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Domain.Entities;

namespace Services
{
    public class CompanyService : ICompanyService
    {
        public Task AddAsync(Company entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataCollection<Company>> GetAllAsync(int page, int take, IEnumerable<int> entities = null)
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, Company entity)
        {
            throw new NotImplementedException();
        }
    }
}