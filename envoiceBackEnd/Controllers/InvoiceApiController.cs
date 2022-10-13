using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTOs.Invoice;

namespace envoiceBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceApiController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;

        public InvoiceApiController(IInvoiceService invoiceService,
                                    IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }


        [HttpGet()]
        public async Task<DataCollection<InvoiceDto>> GetAll(int page = 1, int take = 10, string? ids = null)
        {
            IEnumerable<int> invoices = null;
            if (!string.IsNullOrEmpty(ids))
            {
                invoices = ids.Split(',').Select(x => Convert.ToInt32(x));
            }
            var collection = await _invoiceService.GetAllAsync(page, take,invoices);

            return _mapper.Map<DataCollection<InvoiceDto>>(collection);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDto>> GetCompanyById(int id)
        {
            var company = await _invoiceService.GetAsync(id);
            if (company != null)
            {
                return _mapper.Map<InvoiceDto>(company);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> PostCompany([FromBody] InvoiceCreateDTo model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var invoice = _mapper.Map<Invoice>(model);

                await _invoiceService.AddAsync(invoice);
                return Ok(model);
            }
            catch (System.Exception)
            {

                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, InvoiceUpdateDTo model)
        {

            if (id != model.Id)
            {
                return BadRequest();
            }
            var invoice = _mapper.Map<Invoice>(model);
          
            await _invoiceService.UpdateAsync(id, invoice);

            return Ok();

        }



    }

}
