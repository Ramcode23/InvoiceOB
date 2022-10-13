using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain.Entities;
using Services.DTOs;
using Services.DTOs.Company;
using Services.DTOs.Invoice;

namespace envoiceBackEnd.utilities
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataCollection<Company>,DataCollection<CompanyDTo>>().ReverseMap();
            CreateMap<DataCollection<Company>, DataCollection<CompanyDTo>>();
            CreateMap<Company, CompanyCreateDto>().ReverseMap();
            CreateMap<Company, CompanyCreateDto>();

            CreateMap<DataCollection<Invoice>,DataCollection<InvoiceDto>>().ReverseMap();
            CreateMap<DataCollection<Invoice>, DataCollection<InvoiceDto>>();
            CreateMap<Invoice, InvoiceCreateDTo>().ReverseMap();
            CreateMap<Invoice, InvoiceCreateDTo>();
        }
    }
}