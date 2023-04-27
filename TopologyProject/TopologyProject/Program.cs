using Microsoft.EntityFrameworkCore;
using TopologyProject;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("FeatureCollection");

builder.Services.AddDbContext<FeaturesDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller=Features}/{action=GetJson}");

app.Run();
