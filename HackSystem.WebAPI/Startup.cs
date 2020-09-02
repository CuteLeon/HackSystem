using System;
using System.Text;
using HackSystem.WebAPI.Configurations;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.Model.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 配置 CORS
            services.AddCors(options => options.AddPolicy("AllowAny", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            // 配置数据库交互
            services.AddDbContext<HackSystemDBContext>(options =>
                    options.UseSqlite(this.Configuration.GetConnectionString("HSDB")));

            // 注册 Identity 服务及配置
            services.AddIdentity<HackSystemUser, HackSystemRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 4;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;

                    options.Lockout.AllowedForNewUsers = true;

                    options.SignIn.RequireConfirmedAccount = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<HackSystemDBContext>();

            // 启用身份认证
            services.Configure<JwtConfiguration>(this.Configuration.GetSection("JwtConfiguration"));
            var jwtConfiguration = this.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // 修正过期时间偏移
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfiguration.JwtIssuer,
                        ValidAudience = jwtConfiguration.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.JwtSecurityKey))
                    };
                });

            services.AddResponseCompression();

            services
                .AddControllersWithViews()
                .AddNewtonsoftJson();
        }

        /// <summary>
        /// 配置请求处理管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors("AllowAny");

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            // 用户认证
            app.UseAuthentication();

            app.UseRouting();

            // 用户授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
