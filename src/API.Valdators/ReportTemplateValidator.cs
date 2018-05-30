using API.Core.Models;
using FluentValidation;

namespace API.Valdators
{
    public class ReportTemplateValidator : AbstractValidator<ReportTemplate>
    {
        public ReportTemplateValidator()
        {
            RuleFor(rt => rt.Name).NotEmpty();
            RuleFor(rt => rt.Description).NotEmpty();
        }
    }
}
