using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Dtos;
using API.Handlers.Queries;
using API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;

namespace API.Handlers
{
    public class ReportTemplatesGetHandler : IRequestHandler<ReportTemplatesGet, IEnumerable<ReportTemplateDto>>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ReportTemplatesGetHandler(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReportTemplateDto>> Handle(ReportTemplatesGet request, CancellationToken cancellationToken)
        {
            return await context.ReportTemplates
                .Include(rt => rt.Tags)
                .ThenInclude(rtrtt => rtrtt.ReportTemplateTag)
                .Select(rt => mapper.Map<ReportTemplateDto>(rt))
                .ToListAsync();
        }
    }
}
