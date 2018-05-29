using API.Core.Dtos;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Valdators
{
    public class ReportTemplateCreateValidator : AbstractValidator<ReportTemplateDto>
    {
        public ReportTemplateCreateValidator()
        {
            RuleFor(rt => rt.Name).NotEmpty();
            RuleFor(rt => rt.Description).NotEmpty();
        }
    }
}
