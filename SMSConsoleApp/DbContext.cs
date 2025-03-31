using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HTTPClassLib;

namespace SMSConsoleApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<HTTPClassLib.Models.Dish> Dishes { get; set; }

        private static string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return config.GetConnectionString("Postgres");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(GetConnectionString());
    }
}
