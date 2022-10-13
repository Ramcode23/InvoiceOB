using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Services
{
    public class CompanyService : ICompanyService
    {

        private readonly ApplicationDbContext _context;
        public CompanyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Company entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id, Company entity)
        {
            throw new NotImplementedException();
        }

        public async Task<DataCollection<Company>> GetAllAsync(int page, int take, IEnumerable<int> entities = null)
        {
            return await _context.Companies
                    .Where(x => entities == null || entities.Contains(x.Id))
                    .OrderBy(x => x.Name)
                    .GetPagedAsync(page, take);

        }

        public async Task<Company> GetAsync(int id)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(int id, Company entity)
        {
            var company = await GetAsync(id);
            if (company != null)
            {
                company.Name = entity.Name;
                company.UpdatedAt = DateTime.Now;
                company.UpdatedBy = entity.UpdatedBy;
            }
            
            await _context.SaveChangesAsync();
        }
    }
}