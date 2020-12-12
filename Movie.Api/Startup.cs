using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Api.Controllers;
using Movie.Api.MovieMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NLog;
using Movie.Services;
using Movie.Interfaces;
using Movie.Repository;
using Movie.Repository.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Movie.Types;
using Microsoft.OpenApi.Models;

namespace Movie.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public byte[]? key;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            RegisterGenericServices(services);
            RegisterApiServices(services);
            RegisterSwagger(services);
            RegisterAuthentication(services);
            RegisterControllers(services);
        }

        private void RegisterAuthentication(IServiceCollection services)
        {

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private void RegisterGenericServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDbContext>
              (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddSingleton<ILogger, Logger>();
            services.AddCors();
            services.AddAutoMapper(typeof(MovieMappings));


            //AppSettings 
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            key = Encoding.ASCII.GetBytes(appSettings.Secret);
        }

        private void RegisterApiServices(IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepositoryService, MovieModelRepository>();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IActorRepositoryService, ActorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("movieapispec",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Movie API",
                        Version = "1",
                        Description = "Movie API"
                    });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                { 
                Description= "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] " +
                "and then your token in the text input below. \r\n\r\nExample: \"Bearer 12345678f \" ",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                     new  OpenApiSecurityScheme
                     { 
                     Reference = new OpenApiReference
                     { 
                     Type = ReferenceType.SecurityScheme,
                     Id= "Bearer"
                     
                     },
                     Scheme = "oath2",
                     Name = "Bearer",
                     In = ParameterLocation.Header,
                     },
                     new List<string>()
                    }
                });
            });
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
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;



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
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
