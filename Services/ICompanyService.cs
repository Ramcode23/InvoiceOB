using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Domain.Entities;
using Services.DTOs;
using Services.DTOs.Company;

namespace Services
{
    public interface ICompanyService : IQueryExtension<Company>
    {
    Task<List<Company>> GetListAsync(string field);
    }
}