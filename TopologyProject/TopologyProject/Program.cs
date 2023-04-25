using System.Text.Json;
using TopologyProject;

var builder = WebApplication.CreateBuilder(args);

var jsonSerializerOptions = new JsonSerializerOptions();
//jsonSerializerOptions.Converters.Add(new GeomentryJsonConverter());

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(jsonSerializerOptions);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=GetJson}");

app.Run();
