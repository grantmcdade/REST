using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Models
{
    public class ReportTemplateReportTemplateTag
    {
        public int ReportTemplateId { get; set; }
        public ReportTemplate ReportTemplate { get; set; }
        public int ReportTemplateTagId { get; set; }
        public ReportTemplateTag ReportTemplateTag { get; set; }
    }
}
