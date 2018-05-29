using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class ReportTemplatesControllerPostShould : IClassFixture<ReportTemplateControllerFixture>
    {
        public ReportTemplatesControllerPostShould(ReportTemplateControllerFixture fixture)
        {
            Fixture = fixture;
        }

        private ReportTemplateControllerFixture Fixture { get; }
    }
}
