using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public class ReportTemplateTag : BaseEntity
    {
        public string Name { get; set; }
        public virtual HashSet<ReportTemplateReportTemplateTag> Reports { get; set; }
    }
}
