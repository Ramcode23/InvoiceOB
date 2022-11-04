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
            CreateMap<Company, CompanyCreateDto>().PreserveReferences().ReverseMap();
            CreateMap<Company, CompanyCreateDto>().PreserveReferences();
            CreateMap<Company, CompanyDTo>().PreserveReferences().ReverseMap();
            CreateMap<Company, CompanyDTo>().PreserveReferences();
            CreateMap<DataCollection<Company>,DataCollection<CompanyDTo>>().PreserveReferences();


            CreateMap<Invoice, InvoiceDto>().PreserveReferences().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().PreserveReferences();

            CreateMap<DataCollection<Invoice>, DataCollection<InvoiceDto>>().PreserveReferences();  

            CreateMap<Invoice, InvoiceCreateDTo>().PreserveReferences().ReverseMap();
            CreateMap<Invoice, InvoiceCreateDTo>().PreserveReferences();

            CreateMap<InvoiceLine, InvoiceLineDTo>().PreserveReferences().ReverseMap();
            CreateMap<InvoiceLine, InvoiceLineDTo>().PreserveReferences();
        }
    }
}