using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Data.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

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
        public IEnumerable<API.Models.ReportTemplateDto> GetReportTemplates()
        {
            return _context.ReportTemplates
                .Include(rt => rt.Tags)
                .ThenInclude(rtt => rtt.ReportTemplateTag)
                .Select(rt => mapper.Map<API.Models.ReportTemplateDto>(rt));
        }

        // GET: api/ReportTemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<API.Models.ReportTemplateDto>> GetReportTemplate([FromRoute] int id)
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

            return Ok(mapper.Map<API.Models.ReportTemplateDto>(reportTemplate));
        }

        // PUT: api/ReportTemplates/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutReportTemplate([FromRoute] int id, [FromBody] ReportTemplate reportTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reportTemplate.Id)
            {
                return BadRequest();
            }

            _context.Entry(reportTemplate).State = EntityState.Modified;

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
        public async Task<ActionResult<API.Models.ReportTemplateDto>> PostReportTemplate([FromBody] ReportTemplate reportTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ReportTemplates.Add(reportTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportTemplate", new { id = reportTemplate.Id }, mapper.Map<API.Models.ReportTemplateDto>(reportTemplate));
        }

        // DELETE: api/ReportTemplates/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<API.Models.ReportTemplateDto>> DeleteReportTemplate([FromRoute] int id)
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

            return Ok(mapper.Map<API.Models.ReportTemplateDto>(reportTemplate));
        }

        private bool ReportTemplateExists(int id)
        {
            return _context.ReportTemplates.Any(e => e.Id == id);
        }
    }
}