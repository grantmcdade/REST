using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Dtos;
using API.Core.Models;
using API.Handlers.Queries;
using API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace API.Handlers
{
    public class ReportTemplatesGetHandler : IRequestHandler<ReportTemplatesGet, IEnumerable<ReportTemplateDto>>
    {
        private readonly ApplicationDbContext context;

        public ReportTemplatesGetHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ReportTemplateDto>> Handle(ReportTemplatesGet request, CancellationToken cancellationToken)
        {
            return await context.ReportTemplates
                .Include(rt => rt.Tags)
                .ThenInclude(rtrtt => rtrtt.ReportTemplateTag)
                .ProjectTo<ReportTemplateDto>()
                .ToListAsync();
        }
    }
}
