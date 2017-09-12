using AdminSite.Core;
using AutoMapper;
using Core;
using Core.FileManager;
using Core.Log.FileLog;
using IService;
using IService.PermissionSystem;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PermissionSystem;
using Sakura.AspNetCore.Mvc;
using Service.PermissionSystem;
using ViewModels.Mapper;
using ViewModels.Options;

namespace AdminSite
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
            services.UserCore();
            services.AddSingleton(AutoMapperConfiguration.Init());

            #region DB

            services.AddDbContext<PermissionSystemContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("PermissionSystem")),
                ServiceLifetime.Singleton);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientApiService, ClientApiService>();
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IMenuService, MenuService>();

            #endregion

            #region Other

            services.Configure<PagerOptions>(Configuration.GetSection("Pager"));
            services.AddBootstrapPagerGenerator(options => options.ConfigureDefault());


            services.Configure<AdminSiteOption>(Configuration.GetSection("SiteOption"));

            services.AddScoped<IRouter, AdminRouter>();
            services.AddScoped<IRouteProvider, RouteProvider>();

            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IFileManager fileManager, IHttpContextAccessor httpContextAccessor, IRouteProvider routeProvider)
        {
            loggerFactory.AddFileLogger(fileManager, httpContextAccessor, Configuration.GetSection("Logging"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseMvc(routeProvider.RegisterRoutes);
        }
    }
}