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

namespace API.Controllerrs
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReportTemplatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public ReportTemplatesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/ReportTemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportTemplateDto>>> GetReportTemplates()
        {
            var model = await _context.ReportTemplates
                .Include(rt => rt.Tags)
                .ThenInclude(rtt => rtt.ReportTemplateTag)
                .ToListAsync();

            return Json(model.Select(rt => mapper.Map<ReportTemplateDto>(rt)));
        }

        // GET: api/ReportTemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportTemplateDto>> GetReportTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportTemplate = await _context.ReportTemplates
                .Include(rt => rt.Tags)
                .ThenInclude(rtt => rtt.ReportTemplateTag)
                .SingleOrDefaultAsync(rt => rt.Id == id);

            if (reportTemplate == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ReportTemplateDto>(reportTemplate));
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
        public async Task<ActionResult<ReportTemplateDto>> PostReportTemplate([FromBody] ReportTemplateDto reportTemplateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportTemplate = mapper.Map<ReportTemplate>(reportTemplateDto);
            _context.ReportTemplates.Add(reportTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportTemplate", new { id = reportTemplate.Id }, mapper.Map<ReportTemplateDto>(reportTemplate));
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