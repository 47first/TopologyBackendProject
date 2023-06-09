using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

namespace TopologyProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.MapControllerRoute(
                name: "api",
                pattern: "api/{controller=Features}/{action=Import}");

            app.Run();

        }

        private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            string? connectionString = configuration.GetConnectionString("FeatureCollection");
            services.AddDbContext<FeaturesDbContext>(options => options.UseSqlServer(connectionString));

            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            services.AddControllersWithViews().AddJsonOptions(options =>
               options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
        }
    }
}
