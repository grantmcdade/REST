using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Dtos;
using API.Core.Models;
using API.Handlers.Queries;
using API.Infrastructure;
using MediatR;

namespace API.Handlers
{
    public class GetReportTemplatesHandler : IRequestHandler<GetReportTemplates, IEnumerable<ReportTemplateDto>>
    {
        public GetReportTemplatesHandler(ApplicationDbContext context)
        {

        }

        public Task<IEnumerable<ReportTemplateDto>> Handle(GetReportTemplates request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
