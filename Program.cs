using Microsoft.EntityFrameworkCore;
using OnlineBankAPI.Models;

namespace OnlineBankAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContextPool<AppDBContext>(

options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", builder =>
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader());
                });
            }



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("AllowAll");
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
