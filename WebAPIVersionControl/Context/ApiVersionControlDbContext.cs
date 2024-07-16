using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics.Metrics;
using WebAPIVersionControl.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace WebAPIVersionControl.Context
{
    public class ApiVersionControlDbContext : DbContext
    {
        public ApiVersionControlDbContext(DbContextOptions<ApiVersionControlDbContext> options): base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<City> city { get; set; }
        public DbSet<Country> country { get; set; }
        public DbSet<CountryLanguage> countrylanguage { get; set; }

        // Add DbSet properties for other entities as needed
    }
}
