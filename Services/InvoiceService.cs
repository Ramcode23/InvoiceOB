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
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;
        public async Task AddAsync(Invoice entity)
        {
            entity.InvoiceNumber= Guid.NewGuid().ToString();
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, Invoice entity)
        {
             var invoice = await GetAsync(id);
            if (invoice != null)
            {
                invoice.DeleteteAt = DateTime.Now;
                invoice.UpdatedBy = entity.UpdatedBy;
               
            }

            await _context.SaveChangesAsync();
        }

        public async Task<DataCollection<Invoice>> GetAllAsync(int page, int take, IEnumerable<int> entities = null)
        {
            return await _context.Invoices
                 .Where(x => entities == null || entities.Contains(x.Id))
                 .OrderBy(x => x.InvoiceDate)
                 .GetPagedAsync(page, take);
        }

        public async Task<Invoice> GetAsync(int id)
        {
            return await _context.Invoices.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(int id, Invoice entity)
        {
            var invoice = await GetAsync(id);
            if (invoice != null)
            {
                invoice.Decription = entity.Decription;
                invoice.UpdatedAt = DateTime.Now;
                invoice.UpdatedBy = entity.UpdatedBy;
                invoice.InvoiceLines = new List<InvoiceLine>();
                invoice.InvoiceLines = entity.InvoiceLines;
            }

            await _context.SaveChangesAsync();
        }
    }
}