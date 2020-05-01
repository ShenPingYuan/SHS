using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SHS.Data;
using SHS.Entities;
using SHS.IRepository;
using SHS.Repository;

namespace SHS_API
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
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
                //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).AddXmlDataContractSerializerFormatters();
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnectionString"),
                    b => b.MigrationsAssembly("SHS.Data"));
                options.EnableSensitiveDataLogging(true);//打开敏感数据记录
            });
            services.AddIdentity<ApplicationIdentityUser, ApplicationIdentityRole>(options => 
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredLength = 1;
            //    options.Password.RequiredUniqueChars = 1;

            //});
            services.AddCors(options =>
            {
                options.AddPolicy("any", o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            services.AddScoped<ITeacherRepository, TeacherRepository>();
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

            app.UseCors("any");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
