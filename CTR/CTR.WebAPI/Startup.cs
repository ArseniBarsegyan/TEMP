using CTR.BLL.Interfaces;
using CTR.BLL.Services;
using CTR.DAL.EF;
using CTR.DAL.Entities;
using CTR.DAL.Identity;
using CTR.DAL.Interfaces;
using CTR.DAL.Repositories;
using CTR.WebAPI.Helpers;
using CTR.WebAPI.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CTR.WebAPI
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
            MapperHelper.InitializeMapper();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(ConstantStorage.ConnectionString)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddScoped<DbContext, ApplicationContext>();
            services.AddScoped<IGenericRepository<ActivityEntity>, GenericRepository<ActivityEntity>>();
            services.AddScoped<IActivityService, ActivityService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
