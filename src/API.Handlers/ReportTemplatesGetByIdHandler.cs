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
                .Select(rt => mapper.Map<ReportTemplateDto>(rt))
                .SingleOrDefaultAsync(rt => rt.Id == request.Id);

            return reportTemplate;
        }
    }
}
