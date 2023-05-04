using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi_DAL;
using WebApi_DAL.Models;
using WebApi_DAL.Repository;
using WebApi_DAL.Services;
using WebApi_BAL.Validator;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApi_DAL.Pagination;
using Microsoft.VisualStudio.Services.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product API",
        Description = "Product"
    });
     
});
 
builder.Services.AddDbContext<ProductContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
optionsBuilder => optionsBuilder.MigrationsAssembly("WebApi_DAL")));

 
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();

 

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
