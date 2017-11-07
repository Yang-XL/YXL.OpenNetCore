using System;
using System.Linq;
using Core;
using Core.FileManager;
using IdentityServer4;
using IdentityServer4.Validation;
using IdentitySite.Services;
using IdentitySite.Services.IdentityService;
using IdentitySite.Services.IdentityService.Validation;
using IService;
using IService.PermissionSystem;
using LoggerExtensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mongo;
using PermissionSystem;
using Service.PermissionSystem;
using ViewModels.IdentitySite.Options;
using ViewModels.Mapper;

namespace IdentitySite
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

            #region XL.Core

            services.UserCore();
            services.AddSingleton(AutoMapperConfiguration.Init());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.UserMongoLog(Configuration.GetSection("Mongo.Log"));


            #endregion

            #region DB

            services.AddDbContext<PermissionSystemContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("PermissionSystem"),
                    o => o.UseRowNumberForPaging()),
                ServiceLifetime.Singleton);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientApiService, ClientApiService>();
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserRoleJurisdictionService, UserRoleJurisdictionService>();
            services.AddScoped<ILogService, LogService>();

            #endregion

            #region IdentityServer4

            services.Configure<AccountOption>(Configuration.GetSection("Identity.Service"));
            services.AddScoped<AccountService>();
            services.AddScoped<ConsentService>();
            services.AddScoped<IRedirectUriValidator, HostRedirectUriValidator>();

         
            var builder = services.AddIdentityServer(options=>
            {
                //options.Authentication.AuthenticationScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
                options.Authentication.CookieLifetime = TimeSpan.FromHours(24);
                
            });
            builder.AddClientStore<ClientStore>();
            builder.AddResourceStore<ResourceStore>();
            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            builder.AddDeveloperSigningCredential();
         
            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            ILogService logService)
        {
            loggerFactory.AddLog(Configuration.GetSection("Logging"), logService);
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
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}