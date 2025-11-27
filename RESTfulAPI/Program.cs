using Microsoft.EntityFrameworkCore;
using RESTfulAPI.DataBase;
using Microsoft.Extensions.Configuration;
using RESTfulAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

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
            })
            //注册JsonPatch
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            }) 
            .AddXmlDataContractSerializerFormatters()//添加XML格式支持
            //接管 ASP.NET Core 的模型验证失败响应
            //不再返回框架默认的 400 + JSON
            //而是统一返回 422 + 符合 RFC 7807 的 application/ problem + json
            //并且带上自定义扩展字段
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "错误类型",
                        Title = "数据验证失败",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "详细信息",
                        Instance = context.HttpContext.Request.Path
                    };
                    problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(problemDetail)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });
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
