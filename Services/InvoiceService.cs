using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Domain.Entities;

namespace Services
{
    public class InvoiceService : IInvoiceService
    {
        public Task AddAsync(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataCollection<Invoice>> GetAllAsync(int page, int take, IEnumerable<int> entities = null)
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, Invoice entity)
        {
            throw new NotImplementedException();
        }
    }
}