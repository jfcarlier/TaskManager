using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskManager.Data.Provider.Sql;
using AutoMapper;
using TaskManager.Data.Interface;
using TaskManager.Data.Provider.Sql.Repository;
using TaskManager.Business;
using TaskManager.Business.Interface;

namespace TaskManager.API
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
            services.AddControllers();

            //ID interface => Repository
            services.AddScoped<IBoardRepository, BoardRepository>();            
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            //ID Interface => Domain
            services.AddScoped<ITaskManagerDomain, TaskManagerDomain>();

            //ID DbContext
            //En changeant la valeur de "UseSqlServer" dans le fichier appsettings.json, on choisis quel Db utiliser
            //Si on utilise SqlServer, ne pas oublier de changer la connectionString dans le même fichier
            if (Configuration.GetSection("UseSqlServer").Value == "true")
            {                
                services.AddDbContext<TaskManagerContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("TaskManagerDbContext")));
            }
            else
            {
                services.AddDbContext<TaskManagerContext>(opt => opt.UseInMemoryDatabase("TaskManagerDb"));
            }

            //ID AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //ID Swagger via NSwag
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "TaskManager API";
                    document.Info.Description = "A Task Management ASP.NET Core web API ";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Carlier Jean-François",
                        Email = "carlier.jeanfrancois.info@gmail.com",
                        Url = "https://github.com/jfcarlier/TaskManager"
                    };                    
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Ajout pour utiliser l'interface de Swagger
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
