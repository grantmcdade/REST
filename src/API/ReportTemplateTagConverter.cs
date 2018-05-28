using API.Data;
using API.Data.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class ReportTemplateTagConverter : ITypeConverter<string, ReportTemplateReportTemplateTag>
    {
        private readonly ApplicationDbContext context;

        public ReportTemplateTagConverter(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ReportTemplateReportTemplateTag Convert(string source, ReportTemplateReportTemplateTag destination, ResolutionContext autoMapperContext)
        {
            var rt = context.ReportTemplateTags
                .SingleOrDefault(rt1 => rt1.Name == source);

            if (rt == null)
            {
                rt = new ReportTemplateTag { Name = source };
                context.ReportTemplateTags.Add(rt);
            }

            ReportTemplateReportTemplateTag rtrtt = null;
            if (autoMapperContext.Items.TryGetValue("Id", out object value))
            {
                var reportId = System.Convert.ToInt32(value);
                rtrtt = context.ReportTemplateReportTemplateTags
                    .SingleOrDefault(rtrtt1 => rtrtt1.ReportTemplateTagId == rt.Id && rtrtt1.ReportTemplateId == reportId);
            }

            if (rtrtt == null)
            {
                rtrtt = new ReportTemplateReportTemplateTag
                {
                    ReportTemplateTag = rt
                };
                context.ReportTemplateReportTemplateTags.Add(rtrtt);
            }

            return rtrtt;
        }
    }
}
