
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SumCalculator.Data;
using SumCalculator.Repositories;
using SumCalculator.Validators;
using Morris.Blazor.Validation;

var builder = WebApplication.CreateBuilder(args);

//add mssql
var DefaultConnection = builder.Configuration.GetConnectionString("DefaultConnection")?? 
    throw new ArgumentNullException("$Connection string is null");
builder.Services.AddDbContextFactory<ApplicatonDbContext>(options => options.UseSqlServer(DefaultConnection));

//add validation
builder.Services.AddFormValidation(
    config => config.AddFluentValidation(typeof(CalculatorEntryValidator).Assembly)
);
//add repos
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ICalculatorRepository, CalculatorRepository>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

//other middlewares...
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
