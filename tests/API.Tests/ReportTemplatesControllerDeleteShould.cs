using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class ReportTemplatesControllerDeleteShould : IClassFixture<ReportTemplateControllerFixture>
    {
        public ReportTemplatesControllerDeleteShould(ReportTemplateControllerFixture fixture)
        {
            Fixture = fixture;
        }

        private ReportTemplateControllerFixture
            Fixture { get; }

    }
}
