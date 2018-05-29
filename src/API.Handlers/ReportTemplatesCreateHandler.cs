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
using API.Handlers.Commands;
using AutoMapper;

namespace API.Handlers
{
    public class ReportTemplatesCreateHandler : IRequestHandler<ReportTemplateCreate, int>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ReportTemplatesCreateHandler(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<int> Handle(ReportTemplateCreate request, CancellationToken cancellationToken)
        {
            var reportTemplate = mapper.Map<ReportTemplate>(request);
            context.ReportTemplates.Add(reportTemplate);

            await context.SaveChangesAsync();

            return reportTemplate.Id;
        }
    }
}
