using Microsoft.EntityFrameworkCore;
using Movie.Repository.Data;
using Microsoft.Extensions.Configuration;

namespace Movie.UnitTests
{
    public class Depedencies
    {

        string _connectionString;
        public DbContextOptions<AppDbContext> Options { get; set; }
        public void Setup()
        {

            var config = InitConfiguration();
            _connectionString = config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
           
            Options = GetOptions(_connectionString);

        }
   
        private  DbContextOptions<AppDbContext> GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<AppDbContext>(), connectionString).Options;
        }
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }

}
