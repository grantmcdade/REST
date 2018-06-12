using API.Controllerrs;
using API.Core;
using API.Core.Dtos;
using API.Handlers.Queries;
using API.Infrastructure;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using AutoMapper.QueryableExtensions;
using System.Linq;
using StructureMap;
using API.Valdators;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.Tests
{

    class MyClass
    {

    }

    public class ReportTemplateControllerFixture : IDisposable
    {
        public ReportTemplateControllerFixture()
        {
            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseInMemoryDatabase("TestingDb")
            //    .Options;
            //Context = new ApplicationDbContext(options);
            var host = WebHost.CreateDefaultBuilder()
                .UseEnvironment(Constants.Testing)
                .UseStartup<Startup>()
                .Build();

            Scope = host.Services.CreateScope();
            Context = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //SeedData.PopulateTestData(Context);

            Controller = Scope.ServiceProvider.GetRequiredService<ReportTemplatesController>();
            //Controller = new ReportTemplatesController(Context, MockMapper.Object, MockMediator.Object);
        }

        public ReportTemplatesController Controller { get; }
        public ApplicationDbContext Context { get; }
        public IServiceScope Scope { get; private set; }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Scope.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ReportTemplateControllerFixture() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
        //public Mock<IMapper> MockMapper { get; private set; } = new Mock<IMapper>();
        //public Mock<IMediator> MockMediator { get; private set; } = new Mock<IMediator>();
    }
}