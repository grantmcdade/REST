using API.Controllerrs;
using API.Core;
using API.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Tests
{
    public class ReportTemplateControllerFixture
    {
        public ReportTemplateControllerFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("TestingDB");
            Context = new ApplicationDbContext(optionsBuilder.Options);
            SeedData.PopulateTestData(Context);

            var mapperConfig = new MapperConfiguration(options => 
            {
                options.AddProfile<MapperProfile>();
            });
            Mapper = mapperConfig.CreateMapper();

            Controller = new ReportTemplatesController(Context, Mapper);
        }

        public IMapper Mapper { get; }
        public ReportTemplatesController Controller { get; }
        public ApplicationDbContext Context { get; }
    }
}