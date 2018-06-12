using API.Core.Models;
using API.Handlers.Commands;
using API.Infrastructure;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Handlers
{
    public class ReportTemplateUpdateHandler : IRequestHandler<ReportTemplateUpdate>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IServiceProvider sp;

        public ReportTemplateUpdateHandler(ApplicationDbContext context, IMapper mapper, IServiceProvider sp)
        {
            this.context = context;
            this.mapper = mapper;
            this.sp = sp;
        }

        public async Task<Unit> Handle(ReportTemplateUpdate request, CancellationToken cancellationToken)
        {
            var reportTemplate = await context.ReportTemplates.Include(rt => rt.Tags).ThenInclude(rtrtt => rtrtt.ReportTemplateTag).SingleOrDefaultAsync(rt => rt.Id == request.Id);
            if (reportTemplate == null)
            {
                reportTemplate = new ReportTemplate();
            }

            mapper.Map(request, reportTemplate, opts =>
            {
                opts.ConstructServicesUsing(t => sp.GetService(t));
            });

            // Delete linked tags that are not in the DTO
            var tagsToDelete = await context.ReportTemplateReportTemplateTags
                .Include(rtrtt1 => rtrtt1.ReportTemplateTag)
                .Where(rtrtt => rtrtt.ReportTemplateId == request.Id && !request.Tags.Any(t => t == rtrtt.ReportTemplateTag.Name))
                .ToListAsync();
            context.ReportTemplateReportTemplateTags.RemoveRange(tagsToDelete);

            await context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
