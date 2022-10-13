using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTOs;
using Services.DTOs.Company;

namespace envoiceBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompaniesController(
            ICompanyService companyService,
            IMapper mapper
        )
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        
        [HttpGet()]
        public async Task<DataCollection<CompanyDTo>> GetAll(int page = 1, int take = 10, string? ids = null)
        {
            IEnumerable<int> companies = null;
            var collection =  await _companyService.GetAllAsync(page, take);
            if (!string.IsNullOrEmpty(ids))
            {
                companies = ids.Split(',').Select(x => Convert.ToInt32(x));
            }

            return _mapper.Map<DataCollection<CompanyDTo>>(collection);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDTo>> GetCompanyById(int id)
        {
            var company = await _companyService.GetAsync(id);
            if (company != null)
            {
                return _mapper.Map<CompanyDTo>(company);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> PostCompany([FromBody] CompanyCreateDto model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
           var company= _mapper.Map<Company>(model);

                await _companyService.AddAsync(company);
                return Ok(model);
            }
            catch (System.Exception)
            {

                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CompanyDTo model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
        var company= _mapper.Map<Company>(model);
            model.Id = id;
            await _companyService.UpdateAsync(id,company);

            return Ok();

        }



    }
}
