using Microsoft.EntityFrameworkCore;
using RESTfulAPI.DataBase;
using Microsoft.Extensions.Configuration;
using RESTfulAPI.Services;
using AutoMapper;

namespace RESTfulAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //注册控制器服务
            builder.Services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;//设置返回406状态码,当请求的格式不被支持时
            }).AddXmlDataContractSerializerFormatters();//添加XML格式支持
            //注册数据库上下文服务

            builder.Services.AddDbContext<AppDbContext>(
                option => option.UseSqlServer(builder.Configuration["DBContext:ConnectionString"]));
            //扫描Profiles文件夹下的所有映射配置
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddTransient<ITouristRouteRepository,TouristRouteRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //测试开发环境异常页面UseDeveloperExceptionPage()
            //app.MapGet("/test", async context => throw new Exception("error"));

            //app.MapGet("/", () => "Hello World!");
            //映射控制器路由
            app.MapControllers();

            app.Run();
        }
    }
}
