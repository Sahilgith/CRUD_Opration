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


        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderItem> OrderItems { get; set; } 

        //cascade delete --> for deleting category
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                . WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<OrderItem>()    
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)    
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);





        }



    }
}
