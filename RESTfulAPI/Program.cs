using Microsoft.EntityFrameworkCore;
using RESTfulAPI.DataBase;
using Microsoft.Extensions.Configuration;
using RESTfulAPI.Services;

namespace RESTfulAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //ע�����������
            builder.Services.AddControllers();
            //ע�����ݿ������ķ���

            builder.Services.AddDbContext<AppDbContext>(
                option => option.UseSqlServer(builder.Configuration["DBContext:ConnectionString"]));

            builder.Services.AddTransient<ITouristRouteRepository,TouristRouteRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //���Կ��������쳣ҳ��UseDeveloperExceptionPage()
            //app.MapGet("/test", async context => throw new Exception("error"));

            //app.MapGet("/", () => "Hello World!");
            //ӳ�������·��
            app.MapControllers();

            app.Run();
        }
    }
}
