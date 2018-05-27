using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNet.Security.OAuth.Validation;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using API.Core;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options => {
                if (Environment.IsEnvironment(Constants.Testing))
                {
                    options.UseInMemoryDatabase(Constants.Testing);
                }
                else
                {
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection"));
                }
                options.UseOpenIddict();
                options.EnableSensitiveDataLogging(true);
            });

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register the OpenIddict services.
            services.AddOpenIddict(options =>
                {
                    // Register the Entity Framework stores.
                    options.AddEntityFrameworkCoreStores<ApplicationDbContext>();

                    // Register the ASP.NET Core MVC binder used by OpenIddict.
                    // Note: if you don't call this method, you won't be able to
                    // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                    options.AddMvcBinders();

                    // Enable the token endpoint (required to use the password flow).
                    options.EnableTokenEndpoint("/connect/token");

                    // Allow client applications to use the grant_type=password flow.
                    options.AllowPasswordFlow();

                    // During development, you can disable the HTTPS requirement.
                    options.DisableHttpsRequirement();
                });

            // Register the validation handler, that is used to decrypt the tokens.
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OAuthValidationDefaults.AuthenticationScheme;
                // options.DefaultChallengeScheme = OAuthValidationDefaults.AuthenticationScheme;
            }).AddOAuthValidation();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Aqua Reports API", Version = "v1" });
            });

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Data.Models.ReportTemplateReportTemplateTag, string>().ConstructUsing(rtrti => rtrti.ReportTemplateTag.Name);
                config.CreateMap<Data.Models.ReportTemplate, Models.ReportTemplateDto>();
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }

        public void ConfigureTesting(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            this.Configure(app, env);
            SeedData.PopulateTestData(app.ApplicationServices.GetService<ApplicationDbContext>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aqua Reports API V1");
            });


            app.UseMvc();
        }
    }
}
