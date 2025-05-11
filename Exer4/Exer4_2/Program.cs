using Exer4_2.Models;
using Exer4_2.service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connString = builder.Configuration.GetConnectionString("QT2DbConn");

builder.Services.AddDbContext<Ex4DbContext>(opts => opts.UseSqlServer(connString));
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SortInterface, SortService>();
builder.Services.AddScoped<ItemStockInterface, StockService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
