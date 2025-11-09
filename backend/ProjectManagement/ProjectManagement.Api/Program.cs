using Microsoft.OpenApi.Models;
using ProjectManagement.Infrastructure.Configuration;
using Serilog;

namespace ProjectManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logSettings = builder.Configuration.GetSection(LoggingSettings.Section).Get<LoggingSettings>();

            builder.Services.AddOptions<DatabaseSettings>() .Bind(builder.Configuration.GetSection(DatabaseSettings.Section)).ValidateOnStart();
                                                                                                                              
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(logSettings.LogFilePath, rollingInterval: RollingInterval.Day)
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();


            // Add services to the container.
            builder.Services.AddPresentationDI(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ProjectManagementAPI",
                    Version = "v1",

                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
