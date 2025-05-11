using Exer3.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connString = builder.Configuration.GetConnectionString("QT2DbConn");
builder.Services.AddDbContext<Ex3DbContext>(options => options.UseSqlServer(connString));

builder.Services.AddScoped<IDataService, DALservice>();

builder.Services.AddSession();
builder.Services.AddMemoryCache();  

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
