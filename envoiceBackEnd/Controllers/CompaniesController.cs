using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserService _userService;

        public CompaniesController(
            ICompanyService companyService,
            IUserService userService,
            IMapper mapper
        )
        {
            _companyService = companyService;
            _userService = userService;
            _mapper = mapper;

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<DataCollection<CompanyDTo>> GetAll(int page = 1, int take = 10, string? ids = null)
        {
            IEnumerable<int> companies = null;
            if (!string.IsNullOrEmpty(ids))
                companies = ids.Split(',').Select(x => Convert.ToInt32(x));
            
            var collection = await _companyService.GetAllAsync(page, take,companies);

            return _mapper.Map<DataCollection<CompanyDTo>>(collection);
        }


        [HttpGet("list")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<List<CompanyDTo>> GetList([FromQuery] string name)
        {        
            if (string.IsNullOrEmpty(name))
                return new List<CompanyDTo>();
            

            var collection = await _companyService.GetListAsync(name);

            return _mapper.Map<List<CompanyDTo>>(collection);
        } 

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CompanyDTo>> GetCompanyById(int id)
        {
            var company = await _companyService.GetAsync(id);
            if (company != null)
                return _mapper.Map<CompanyDTo>(company);
            return NotFound();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostCompany([FromBody] CompanyCreateDto model)
        {

            try
            {
                if (model == null)
                    return BadRequest();
                
                var company = _mapper.Map<Company>(model);
                company.CreatedBy = await _userService.GetUserByEmailAsync(User.Identity.Name);
                await _companyService.AddAsync(company);
                return Ok(model);
            }
            catch (System.Exception)
            {

                return NoContent();
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutCompany(int id, CompanyDTo model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            var company = _mapper.Map<Company>(model);
            model.Id = id;
            company.UpdatedBy = await _userService.GetUserByEmailAsync(User.Identity.Name);
            company.UpdatedAt = DateTime.UtcNow;
            await _companyService.UpdateAsync(id, company);

            return Ok();

        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company= await _companyService.GetAsync(id);
            if (company==null)
            {
                return BadRequest();
            }
            
            company.DeletedBy= await _userService.GetUserByEmailAsync(User.Identity.Name);
            company.DeleteteAt = DateTime.UtcNow;
            await _companyService.DeleteAsync(id, company);

            return Ok();

        }


    }
}
