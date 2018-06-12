using API.Core.Dtos;
using API.Core.Models;
using API.Handlers.Commands;
using API.Valdators;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class ReportTemplatesValidatorShould
    {
        [Fact]
        public async Task EndureThatNameIsNotEmpty()
        {
            var item = new ReportTemplate
            {
                Id = 1,
                Description = "Description"
            };
            var validator = new ReportTemplateValidator();

            var result = await validator.ValidateAsync(item);

            Assert.Contains(result.Errors, s => s.ErrorMessage.StartsWith("'Name'"));

        }
    }
}
