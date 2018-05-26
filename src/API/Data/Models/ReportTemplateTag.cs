using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Models
{
    public class ReportTemplateTag : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<ReportTemplateReportTemplateTag> Reports { get; set; }
    }
}
