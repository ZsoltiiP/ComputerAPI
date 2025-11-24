using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext() { }

        public BlogDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Blogger> bloggers { get; set; }
        public DbSet<Posts> posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbulder)
        {
            optionbulder.UseMySQL("server=localhost;database=Blog;user=root;password=");
        }
    }
}
