using API.Core.Dtos;
using API.Core.Models;
using API.Handlers.Commands;
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
            CreateMap<ReportTemplateReportTemplateTag, string>().ProjectUsing(rtrti => rtrti.ReportTemplateTag.Name);
            CreateMap<string, ReportTemplateReportTemplateTag>().ConvertUsing<ReportTemplateTagConverter>();
            CreateMap<ReportTemplate, ReportTemplateDto>();
            CreateMap<ReportTemplateCreate, ReportTemplate>();
            CreateMap<ReportTemplateUpdate, ReportTemplate>();
        }
    }
}
