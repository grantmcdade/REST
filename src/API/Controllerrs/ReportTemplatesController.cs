using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Core.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using API.Infrastructure;
using API.Core.Dtos;
using MediatR;
using API.Handlers.Queries;
using API.Handlers.Commands;
using API.Valdators;
using FluentValidation;

namespace API.Controllerrs
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReportTemplatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ReportTemplatesController(ApplicationDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        // GET: api/ReportTemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportTemplateDto>>> GetReportTemplates()
        {
            return Json(await mediator.Send(new ReportTemplatesGet()));
        }

        // GET: api/ReportTemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportTemplateDto>> GetReportTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportTemplate = await mediator.Send(new ReportTemplatesGetById { Id = id });

            if (reportTemplate == null)
            {
                return NotFound();
            }

            return Ok(reportTemplate);
        }

        // PUT: api/ReportTemplates/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutReportTemplate([FromRoute] int id, [FromBody] ReportTemplateUpdate command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != command.Id)
            {
                return BadRequest();
            }

            try
            {
                await mediator.Send(command);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportTemplateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (ValidationException ex)
            {
                ex.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        // POST: api/ReportTemplates
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReportTemplateDto>> PostReportTemplate([FromBody] ReportTemplateCreate command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await mediator.Send(command);

            return CreatedAtAction("GetReportTemplate", new { id }, await mediator.Send(new ReportTemplatesGetById { Id = id }));
        }

        // DELETE: api/ReportTemplates/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ReportTemplateDto>> DeleteReportTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportTemplate = await _context.ReportTemplates.FindAsync(id);
            if (reportTemplate == null)
            {
                return NotFound();
            }

            _context.ReportTemplates.Remove(reportTemplate);
            await _context.SaveChangesAsync();

            return Ok(mapper.Map<ReportTemplateDto>(reportTemplate));
        }

        private bool ReportTemplateExists(int id)
        {
            return _context.ReportTemplates.Any(e => e.Id == id);
        }
    }
}