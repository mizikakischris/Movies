using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Api.Controllers;
using Movie.Api.Data;
using Movie.Api.MovieMapper;
using Movie.Api.Repository;
using Movie.Api.Repository.IRepository;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Movie.Api
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
            services.AddDbContext<AppDbContext>
              (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMovieModelRepository, MovieModelRepository>();
            services.AddAutoMapper(typeof(MovieMappings));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("movieapispec",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Movie API",
                        Version = "1",
                        Description = "Movie API"
                    });
            });


                RegisterControllers(services);
        }

        private void RegisterControllers(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Converters = new List<JsonConverter>
                {
                    new StringEnumConverter
                    {
                        AllowIntegerValues = false
                    }
                };

                if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                    resolver.NamingStrategy = null;
            })
            .AddApplicationPart(typeof(MoviesController).Assembly);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        
          
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options => { 

               options.SwaggerEndpoint("/swagger/movieapispec/swagger.json", "Movie API");
                     
                options.RoutePrefix = "";
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
