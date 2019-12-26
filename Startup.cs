using ElectronNET.API;
using ElectronNET.API.Entities;
using Inventory.Data;
using Inventory.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;
using VueCliMiddleware;

namespace Inventory
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.AddDbContext<Data.ApplicationDbContext>();

            services.AddIdentity<Models.DataModels.UserModels.User, Models.DataModels.UserModels.UserRole>()
            .AddEntityFrameworkStores<Data.ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
                        {
                            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                            options.CheckConsentNeeded = context => true;
#if DEBUG
                            options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
#else
                            options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
#endif
                        });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";

                // SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                // options.Cookie.HttpOnly = true;
                // options.Cookie.Expiration = System.TimeSpan.FromMinutes(30);
#if DEBUG
                options.Cookie.SameSite = SameSiteMode.None;
#endif
                options.ExpireTimeSpan = System.TimeSpan.FromMinutes(30);

                // If the LoginPath isn't set, ASP.NET Core defaults
                // the path to /User/Login.
                options.LoginPath = "/User/Login";

                // If the AccessDeniedPath isn't set, ASP.NET Core defaults
                // the path to /User/AccessDenied.
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.Cookie.Name = "XSRF-TOKEN";
                options.Cookie.HttpOnly = false;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.Path = "/";
            });

#if DEBUG
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080", "http://localhost:5000", "https://localhost:5001")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseExceptionMiddleware();
            }
            else
            {
                app.UseExceptionMiddleware();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

#if DEBUG
            app.UseCors("CorsPolicy");
#endif
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                   "default", "{controller=Home}/{action=Index}/{id?}");

                if (env.IsDevelopment())
                {
                    endpoints.MapToVueCliProxy("{*path}", new SpaOptions { SourcePath = "src" }, "serve", regex: "Compiled successfully");
                }
                else
                {
                    endpoints.MapFallbackToFile("dist\\index.html");
                }
            });

            // Open the Electron-Window here
            Task.Run(() => this.BootstrapElectron(lifetime));
        }

        private async Task BootstrapElectron(IHostApplicationLifetime lifetime)
        {
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(
                new BrowserWindowOptions()
                {
                    Title = "انبار داری",
                    AutoHideMenuBar = true,
                    Show = false
                });

            browserWindow.SetMenuBarVisibility(false);
            browserWindow.OnReadyToShow += () =>
            {
                browserWindow.Show();
            };

            browserWindow.OnClosed += lifetime.StopApplication;
        }
    }
}
