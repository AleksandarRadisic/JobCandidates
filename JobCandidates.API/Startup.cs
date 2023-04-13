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
using JobCandidates.Persistence.Migrations;
using JobCandidates.Persistence.Repositories.Implementation;
using JobCandidates.Persistence.Repositories.Interface;
using Persistence.EfStructures;
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

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("connectionString"));
            });

            services.AddScoped<IJobCandidateReadRepository, JobCandidateReadRepository>();
            services.AddScoped<IJobCandidateWriteRepository, JobCandidateWriteRepository>();
            services.AddScoped<ISkillReadRepository, SkillReadRepository>();
            services.AddScoped<ISkillWriteRepository, SkillWriteRepository>();

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobCandidates.API v1"));
            }

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