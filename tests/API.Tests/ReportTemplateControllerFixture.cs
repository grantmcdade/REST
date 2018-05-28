using API.Controllerrs;
using API.Data;
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

            Mapper = Startup.CreateMapper();

            Controller = new ReportTemplatesController(Context, Mapper);
        }

        public IMapper Mapper { get; }
        public ReportTemplatesController Controller { get; }
        public ApplicationDbContext Context { get; }
    }
}