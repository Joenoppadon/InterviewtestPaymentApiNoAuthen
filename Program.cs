using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PaymentsApi.Data;
using FluentValidation;
using PaymentsApi.Data.Model;
using PaymentsApi.Data.Model.Validation;
using PaymentsApi.Data.Repository;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using PaymentsApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.Converters.Add(new StringEnumConverter
    {
        CamelCaseText = true
    });
});

//add db context
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("condb")
    )
);
//dependency injection
builder.Services.AddScoped<IValidator<CreateUser>,UserValidation>();
builder.Services.AddScoped<IUserRepositoryAsync,UserRepositoryAsync>();

builder.Services.AddEndpointsApiExplorer();
// swagger work debug on chrome only 
builder.Services.AddSwaggerGen(config =>
  {
      config.SwaggerDoc("v1", new OpenApiInfo() { Title = "Payment API", Version = "v1" });
      config.EnableAnnotations();
  });
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
        {
            config.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment API");
            config.RoutePrefix = string.Empty;

            //config.DefaultModelExpandDepth(-1); //hide schema
        });
}

app.UseHttpsRedirection();
//app.UseCors();

app.UseAuthorization();
//app.UseAuthentication(); 

app.MapControllers();

app.Run();
