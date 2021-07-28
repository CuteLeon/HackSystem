using HackSystem.Web.Authentication.Extensions;
using HackSystem.WebAPI.Authentication.Configurations;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.Extensions;
using HackSystem.WebAPI.MockServers.Configurations;
using HackSystem.WebAPI.MockServers.Extensions;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebAPI.Services.Extensions;
using HackSystem.WebAPI.TaskServers.Configurations;
using HackSystem.WebAPI.TaskServers.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HackSystem.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Config CORS
            services.AddCors(options => options.AddPolicy("AllowAny", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            // Config Database context
            services.AddDbContext<HackSystemDBContext>(options =>
                    options
                        .UseSqlite(this.Configuration.GetConnectionString("HSDB"))
                        .UseLazyLoadingProxies());

            // Register Identity service and options
            services
                .AddIdentity<HackSystemUser, HackSystemRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 4;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;

                    options.Lockout.AllowedForNewUsers = true;

                    options.SignIn.RequireConfirmedAccount = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<HackSystemDBContext>();

            // Config Jwt
            var jwtConfiguration = this.Configuration.GetSection("JwtConfiguration").Get<JwtAuthenticationOptions>();
            var taskServerConfiguration = this.Configuration.GetSection("TaskServerConfiguration").Get<TaskServerOptions>();
            var mockServerConfiguration = this.Configuration.GetSection("MockServerConfiguration").Get<MockServerOptions>();
            services
                .AddAutoMapper(typeof(Startup).Assembly)
                .AttachTaskServer(taskServerConfiguration)
                .AttachMockServer(mockServerConfiguration)
                .AddHttpClient()
                .AddMemoryCache()
                .AddHackSystemWebAPIExtensions()
                .AddAPIAuthentication(jwtConfiguration)
                .AddAPIServices();

            services
                .AddResponseCompression()
                .AddControllersWithViews()
                .AddNewtonsoftJson();
        }

        /// <summary>
        /// Config Request channel
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/HackSystemError");
                app.UseHsts();
            }

            app.UseCors("AllowAny");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication
            app.UseAuthentication();
            // Authorization
            app.UseAuthorization();

            app.UseWebAPILogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMockServer();
        }
    }
}
