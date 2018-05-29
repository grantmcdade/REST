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
    public class ReportTemplatesGetByIdHandler : IRequestHandler<ReportTemplatesGetById, ReportTemplateDto>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ReportTemplatesGetByIdHandler(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public Task<ReportTemplateDto> Handle(ReportTemplatesGetById request, CancellationToken cancellationToken)
        {
            var reportTemplate = context.ReportTemplates
                .Include(rt => rt.Tags)
                .ThenInclude(rtt => rtt.ReportTemplateTag)
                .ProjectTo<ReportTemplateDto>()
                .SingleOrDefaultAsync(rt => rt.Id == request.Id);

            return reportTemplate;
        }
    }
}
