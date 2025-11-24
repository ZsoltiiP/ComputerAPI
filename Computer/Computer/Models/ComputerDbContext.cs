using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace Computer.Models
{
    public class ComputerDbContext : DbContext
    {
        public ComputerDbContext() {}

        public ComputerDbContext(DbContextOptions options) :base(options) {}
        public DbSet<Computer> computers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=computer;user=root;password=");
        }

    }
}
