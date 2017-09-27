﻿using System;
using System.IO;
using System.Security.Claims;
using AdminSite.Core;
using AdminSite.Middleware;
using AdminSite.SiteAttributes;
using AdminSite.SiteAttributes.Policys.UserPolicy;
using AutoMapper;
using Core;
using Core.FileManager;
using IService;
using IService.PermissionSystem;
using LoggerExtensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mongo;
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.UserMongoLog(Configuration.GetSection("Mongo.Log"));


            #region DB

            services.AddDbContext<PermissionSystemContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("PermissionSystem"),o=>o.UseRowNumberForPaging()),
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

            #region Other
            services.Configure<PagerOptions>(Configuration.GetSection("Pager"));
            services.AddBootstrapPagerGenerator(options => options.ConfigureDefault());
            services.Configure<AdminSiteOption>(Configuration.GetSection("SiteOption"));
            services.AddScoped<IRouter, AdminRouter>();
            services.AddScoped<IRouteProvider, RouteProvider>();

            //services.AddLogging(builder => builder.AddFileLog(Configuration.GetSection("Logging")));

            #endregion

            #region 授权

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LogoutPath = "/Account/LogOut";
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1440);
                options.Cookie.Name = $"{CookieAuthenticationDefaults.CookiePrefix}XL.Y.COOKIES.ADMIN.WEBSITE";
            });
            //基于策略
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", configurePolicy =>
                {
                    configurePolicy.RequireClaim(ClaimTypes.NameIdentifier);
                    configurePolicy.AddRequirements(new UserRequirement());
                });
                options.AddPolicy("Role", configurePolicy =>
                {
                    //configurePolicy.RequireClaim(ClaimTypes.Role);
                    configurePolicy.AddRequirements(new RoleRequirement(services.BuildServiceProvider()));
                });
            });
            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();


            ////基于过滤器
            //services.AddMvc(options => options.Filters.Add<AdminSiteAuthorizationFilter>());

            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            ILogService logService,
            IRouteProvider routeProvider)
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
            app.UseMiddleware<CustomErrorPagesMiddleware>();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routeProvider.RegisterRoutes);
            
        }
    }
}