using bART.Model.Configuration;
using bART.Model.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using bART.Model;
using bART.Model.Repositories.Interfaces;
using bART.Model.Repositories;

namespace bART.Web
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<DbConfiguration>(Configuration.GetSection("ConnectionStrings"));

            var connectionString =
                Configuration.GetConnectionString("DbConnection");
            services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure());
            });


            services.AddSingleton<IRepository<incidents, string>, incidentsRepository>();
            services.AddSingleton<IRepository<accounts, string>, accountsRepository>();
            services.AddSingleton<IRepository<contacts, string>, contactsRepository>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "bART", Version = "v1" });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "bART v1"));

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
