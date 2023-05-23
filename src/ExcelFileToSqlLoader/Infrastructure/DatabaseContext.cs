using ExcelFileToSqlLoader.Models;
using ExcelFileToSqlLoader.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExcelFileToSqlLoader.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Artist> Artist { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = ConfigurationHelper.GetConfiguration();
            string connectionString = configuration.GetConnectionString("SQL_ConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
