using API.Controllerrs;
using API.Core;
using API.Infrastructure;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

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

            Controller = new ReportTemplatesController(Context, Mapper, MockMediator.Object);
        }

        public IMapper Mapper { get; }
        public Mock<IMediator> MockMediator { get; } = new Mock<IMediator>();
        public ReportTemplatesController Controller { get; }
        public ApplicationDbContext Context { get; }
    }
}