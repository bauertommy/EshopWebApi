using EshopWebApi.BusinessLayer.Contexts;
using EshopWebApi.BusinessLayer.Interfaces.Contexts;
using EshopWebApi.BusinessLayer.Interfaces.Services;
using EshopWebApi.BusinessLayer.Services;
using EshopWebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;

namespace EshopWebApi
{
    /// <summary>
    /// Start web api point
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Provides information about the web hosting environment an application is running in
        /// </summary>
        public IHostingEnvironment Environment { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvcCore(i => i.Filters.Add(typeof(EshopWebApiExceptionFilter)))
                .AddVersionedApiExplorer(
                    options =>
                    {
                        options.SubstituteApiVersionInUrl = true;
                        options.GroupNameFormat = "'v'VVV";
                    });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Version = "v1", Title = $"{this.GetType().Assembly.GetName().Name}", Description = "This version contains methods to get products from database." });
                options.SwaggerDoc("v2", new Info { Version = "v2", Title = $"{this.GetType().Assembly.GetName().Name}", Description = "This version contains methods to get products from database including ability to update product." });
                options.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{this.GetType().Assembly.GetName().Name}.xml"));
                options.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"EshopWebApi.BusinessLayer.xml"));
            });

            services.AddSingleton(opt => new MapperFactory().CreateAutoMapper());
            services.AddSingleton(opt => new DbContextFactory().CreateDbContext());
            services.AddScoped<IProductServiceContext, ProductServiceContext>();
            services.AddScoped<IProductService, ProductService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                }
            });
        }
    }
}
