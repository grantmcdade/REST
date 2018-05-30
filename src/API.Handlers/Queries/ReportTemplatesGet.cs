using API.Core.Dtos;
using API.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Handlers.Queries
{
    public class ReportTemplatesGet : IRequest<IEnumerable<ReportTemplateDto>>
    {
        
    }
}
