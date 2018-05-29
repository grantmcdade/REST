using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public class ReportTemplate : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<ReportTemplateReportTemplateTag> Tags { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ThumbnailImage { get; set; }
        public string FullSizeImage { get; set; }
        public string PdfFile { get; set; }
        public string ZipFile { get; set; }
    }
}
