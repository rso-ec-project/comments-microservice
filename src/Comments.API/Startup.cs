using AutoMapper;
using Comments.API.Extensions;
using Comments.Application.Comments;
using Comments.Domain.CommentAggregate;
using Comments.Domain.Shared;
using Comments.Infrastructure;
using Comments.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Comments.API
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
            services.AddDbContext<ApplicationDbContext>((_, options) =>
            {
                options.UseNpgsql(GetConnectionString());
            });

            var mapperConfig = CreateMapperConfiguration();
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddControllers();

            services.AddSwagger();

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        private static string GetConnectionString()
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var username = Environment.GetEnvironmentVariable("DB_USERNAME");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            return $"Host={host};Database={database};Username={username};Password={password}";
        }

        private static MapperConfiguration CreateMapperConfiguration()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CommentMapperProfile());
            });

            return mapperConfig;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChargingStations.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
