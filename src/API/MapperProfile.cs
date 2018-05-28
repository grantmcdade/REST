using API.Data.Models;
using API.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ReportTemplateReportTemplateTag, string>().ConstructUsing(rtrti => rtrti.ReportTemplateTag.Name);
            CreateMap<string, ReportTemplateReportTemplateTag>().ConvertUsing<ReportTemplateTagConverter>();
            CreateMap<ReportTemplate, ReportTemplateDto>();
        }
    }
}
