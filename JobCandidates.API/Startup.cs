using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobCandidates.Domain.PersistenceInterfaces;
using JobCandidates.Domain.Services.Implementation;
using JobCandidates.Domain.Services.Interface;
using JobCandidates.Persistence.EfStructures;
using JobCandidates.Persistence.Migrations;
using JobCandidates.Persistence.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace JobCandidates.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobCandidates.API", Version = "v1" });
            });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowAnyOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("connectionString"));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IJobCandidateReadRepository, JobCandidateReadRepository>();
            services.AddScoped<IJobCandidateWriteRepository, JobCandidateWriteRepository>();
            services.AddScoped<ISkillReadRepository, SkillReadRepository>();
            services.AddScoped<ISkillWriteRepository, SkillWriteRepository>();

            services.AddScoped<IJobCandidateService, JobCandidateService>();
            services.AddScoped<ISkillService, SkillService>();

            using (var context = new AppDbContextFactory().CreateDbContext(
                       new[] { Configuration.GetConnectionString("connectionString") }))
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FindByName.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowAnyOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
