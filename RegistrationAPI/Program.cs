using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;
using RegistrationAPI;
using OpenApiSecurityScheme = Microsoft.OpenApi.Models.OpenApiSecurityScheme;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(new DbRegisterStore("Server=34.159.94.190;Database=ClientBase;Port=5432;User Id= postgres;Password=jtcboPaN7cHJiprd;Ssl Mode=Require;Trust Server Certificate=true;"));
builder.Services.AddSingleton(new DbAccountStore("Server=34.159.94.190;Database=ClientBase;Port=5432;User Id= postgres;Password=jtcboPaN7cHJiprd;Ssl Mode=Require;Trust Server Certificate=true;"));
builder.Services.AddSingleton(new DbTransactionStore("Server=34.159.94.190;Database=ClientBase;Port=5432;User Id= postgres;Password=jtcboPaN7cHJiprd;Ssl Mode=Require;Trust Server Certificate=true;"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument(document => {
    document.Title = "Project API";
    document.Version = "v1";
    document.DocumentProcessors.Add(
        new SecurityDefinitionAppender("JWT Token", new  NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Description = "JWT Token",
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header
        })
    );
    document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JWTMiddleware>();

app.MapControllers();

app.Run();