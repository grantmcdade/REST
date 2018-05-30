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

        public ReportTemplateUpdateHandler(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Handle(ReportTemplateUpdate request, CancellationToken cancellationToken)
        {
            var reportTemplate = mapper.Map<ReportTemplate>(request);

            context.ReportTemplates.Add(reportTemplate);

            // Delete linked tags that are not in the DTO
            var tagsToDelete = await context.ReportTemplateReportTemplateTags
                .Include(rtrtt1 => rtrtt1.ReportTemplateTag)
                .Where(rtrtt => rtrtt.ReportTemplateId == request.Id && !request.Tags.Any(t => t == rtrtt.ReportTemplateTag.Name))
                .ToListAsync();
            context.ReportTemplateReportTemplateTags.RemoveRange(tagsToDelete);

            await context.SaveChangesAsync();
        }
    }
}
