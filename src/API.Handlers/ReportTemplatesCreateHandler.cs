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
using API.Valdators;
using FluentValidation;

namespace API.Handlers
{
    public class ReportTemplatesCreateHandler : IRequestHandler<ReportTemplateCreate, int>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ReportTemplateValidator validator;

        public ReportTemplatesCreateHandler(ApplicationDbContext context, IMapper mapper, ReportTemplateValidator validator)
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<int> Handle(ReportTemplateCreate request, CancellationToken cancellationToken)
        {
            var reportTemplate = mapper.Map<ReportTemplate>(request);

            var vr = validator.Validate(reportTemplate);
            if (!vr.IsValid)
            {
                throw new ValidationException(vr.Errors);
            }

            context.ReportTemplates.Add(reportTemplate);

            await context.SaveChangesAsync();

            return reportTemplate.Id;
        }
    }
}
