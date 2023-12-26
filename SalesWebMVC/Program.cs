using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMVC.Data;
using SalesWebMVC.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;

var builder = WebApplication.CreateBuilder(args);
var connectionStr = "server=localhost;userid=root;password=895smigol;database=bancoteste";

builder.Services.AddDbContext<SalesWebMVCContext>(options =>
    options.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr)));

builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<SalesRecordService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var enUS = new CultureInfo("en-US");
var localizationOption = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS }

};

app.UseRequestLocalization(localizationOption);

app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
