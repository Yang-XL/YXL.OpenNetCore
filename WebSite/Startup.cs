using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.FileManager;
using Core.Log.FileLog;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace WebSite
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
            services.AddMvc();
           
            
           var builder =  services.AddAuthentication();
            builder.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, "Cookie", options =>
            {
                options.Cookie.Name = CookieAuthenticationDefaults.CookiePrefix+ "MNDJK";
                options.Cookie.Expiration = TimeSpan.FromHours(24);
            });

            builder.AddOpenIdConnect("meinian","美年大健康产业集团有限公司", options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Authority = "http://www.sts.com:5000/";
                options.RequireHttpsMetadata = false;
                options.SaveTokens = true;
                options.ClientId = "admin";
                options.ClientSecret = "admin";

                options.ResponseType = "id_token";
                options.Scope.Clear();
                options.Scope.Add("openid");
                //options.CallbackPath = new PathString("/signin-idsrv");
                //options.SignedOutCallbackPath = new PathString("/signout-callback-idsrv");
                //options.RemoteSignOutPath = new PathString("/signout-idsrv");
                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    NameClaimType = "name",
                //    RoleClaimType = "role"
                //};
            });
    
            services.UserCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IFileManager fileManager, IHttpContextAccessor httpContextAccessor)
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
            
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}