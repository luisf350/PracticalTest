using PracticalTest.Api.Extensions;
using PracticalTest.Api.Utils;
using PracticalTest.Entities.Context;
using Microsoft.EntityFrameworkCore;

namespace PracticalTest.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddCors();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Swagger Documentation
        builder.Services.AddSwaggerDocumentation();

        // Add JwtAuthentication
        builder.Services.AddJwtAuthentication(JwtCreationUtil.GetJwtToken(builder.Configuration));

        // Set configuration for Entity Framework
        builder.Services.AddDbContext<AppDbContext>
            (options => options.UseInMemoryDatabase("InMemory"));

        // Dependency Injection
        builder.Services.AddDiExtensions();

        // Add AutoMapper
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.AddSwagger();
        }

        // Global Error Handling
        app.ConfigureExceptionHandler();

        app.UseHttpsRedirection();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
