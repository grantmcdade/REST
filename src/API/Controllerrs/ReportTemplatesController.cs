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
            return Json(await mediator.Send(new GetReportTemplates()));
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
        public async Task<IActionResult> PutReportTemplate([FromRoute] int id, [FromBody] ReportTemplateDto reportTemplateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reportTemplateDto.Id)
            {
                return BadRequest();
            }

            var reportTemplate = mapper.Map<ReportTemplate>(reportTemplateDto, options =>
            {
                options.Items.Add("Id", id);
            });
            _context.Entry(reportTemplate).State = EntityState.Modified;

            // Delete linked tags that are not in the DTO
            var tagsToDelete = await _context.ReportTemplateReportTemplateTags
                .Include(rtrtt1 => rtrtt1.ReportTemplateTag)
                .Where(rtrtt => rtrtt.ReportTemplateId == id && !reportTemplateDto.Tags.Any(t => t == rtrtt.ReportTemplateTag.Name))
                .ToListAsync();

            _context.ReportTemplateReportTemplateTags.RemoveRange(tagsToDelete);
            try
            {
                await _context.SaveChangesAsync();
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