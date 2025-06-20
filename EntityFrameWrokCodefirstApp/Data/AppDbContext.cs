using EntityFrameWrokCodefirstApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWrokCodefirstApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }



    }
}
