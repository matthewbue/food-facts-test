using FitnessFoods.Application.Services;
using FitnessFoods.Infrastructure;
using FitnessFoods.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpClient<ProductImportService>();

builder.Services.AddHostedService<CronJobService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitnessFoods API V1");
        c.RoutePrefix = string.Empty; // Define a raiz do aplicativo como a rota do Swagger
    });
}


app.UseAuthorization();

app.MapControllers();

app.Run();