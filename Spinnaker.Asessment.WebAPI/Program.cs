using Spinnaker.Asessment.SQLIntegration.Base;
using Spinnaker.Assessment.DomainModels;
using Spinnaker.Assessment.Interfaces.In;
using Spinnaker.Assessment.Domain;
using Spinnaker.Assessment.Interfaces.Out;
using Spinnaker.Asessment.SQLIntegration.Repos;
using Spinnaker.Asessment.WebAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICountryRepo, CountryRepo>();


builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.MapControllers();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) 
                .AllowCredentials());

app.Run();
