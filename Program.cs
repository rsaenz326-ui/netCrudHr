using Microsoft.EntityFrameworkCore;
using WebAppHr4.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conString = builder.Configuration.GetConnectionString("conexion") ??
     throw new InvalidOperationException("Error cadena de conexion'" +
    " not found.");

builder.Services.AddDbContext<HrContext>(options =>
  options.UseMySql(conString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb")));



var app = builder.Build();

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
