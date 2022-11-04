using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTOs.Invoice;

namespace envoiceBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public InvoiceController(IInvoiceService invoiceService,
                                 IUserService userService,
                                    IMapper mapper)
        {
            _invoiceService = invoiceService;
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet()]
     //   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<DataCollection<InvoiceDto>> GetAll(int page = 1, int take = 10, string? ids = null)
        {
            IEnumerable<int> invoices = null;
            if (!string.IsNullOrEmpty(ids))
            {
                invoices = ids.Split(',').Select(x => Convert.ToInt32(x));
            }
            var collection = await _invoiceService.GetAllAsync(page, take, invoices);

            return _mapper.Map<DataCollection<InvoiceDto>>(collection);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<InvoiceDto>> GetInvoiceById(int id)
        {
            var invoice = await _invoiceService.GetAsync(id);
            if (invoice != null)
            {
                return _mapper.Map<InvoiceDto>(invoice);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostInvoice([FromBody] InvoiceCreateDTo model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var invoice = _mapper.Map<Invoice>(model);
                invoice.CreatedBy = await _userService.GetUserByEmailAsync(User.Identity.Name);
                await _invoiceService.AddAsync(invoice);
                return Ok(model);
            }
            catch (System.Exception)
            {

                return NoContent();
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutInvoice(int id, InvoiceUpdateDTo model)
        {

            if (id != model.Id)
            {
                return BadRequest();
            }
            var invoice = _mapper.Map<Invoice>(model);
            invoice.UpdatedBy = await _userService.GetUserByEmailAsync(User.Identity.Name);
            await _invoiceService.UpdateAsync(id, invoice);

            return Ok();

        }



    }

}
