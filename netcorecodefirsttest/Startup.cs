using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using netcorecodefirsttest.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using netcorecodefirsttest.Filter;

namespace netcorecodefirsttest
{
    // 在 Startup中注册中间件、定义管道的逻辑
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
            //跨域 设置了允许所有来源
            services.AddCors(options =>
             options.AddPolicy("any",
             builder => builder.AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowAnyOrigin()
                             .AllowCredentials()));

            services.AddMvc();
            // 缓存
            services.AddMemoryCache();
            services.AddDbContext<DTContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //注册Cookie认证服务
           services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            //注意app.UseAuthentication方法一定要放在下面的app.UseMvc方法前面，否者后面就算调用HttpContext.SignInAsync进行用户登录后，使用
            //HttpContext.User还是会显示用户没有登录，并且HttpContext.User.Claims读取不到登录用户的任何信息。
            //这说明Asp.Net OWIN框架中MiddleWare的调用顺序会对系统功能产生很大的影响，各个MiddleWare的调用顺序一定不能反
             app.UseAuthentication();

            #region 跨越代码
            //app.UseCors(builder =>
            //       builder.WithOrigins("http://localhost")
            //           .AllowAnyOrigin()
            //           .AllowAnyHeader()
            //           .AllowAnyMethod());

            //app.UseCors("AllowSpecificOrigin");

            app.UseStaticFiles();

            #endregion
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

           
        }
    }
}
