using HW_MVC_Core_11_Roles_2.Models;
using Microsoft.EntityFrameworkCore;
using Shop_app.Models;

namespace HW_MVC_Core_11_Roles_2.DbContexts
{
    public class ProductDbContext : DbContext // Изменено на DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
           
        }

        // Ваші DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("products");
            // Конфігурація моделі
            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId);

            modelBuilder.Entity<Product>().HasData(SeedProducts);
            
            //modelBuilder.Entity<OrderProducts>().HasData(SeedOrderProducts);
            //modelBuilder.Entity<Order>().HasData(SeedOrders);
        }
        //private static readonly OrderProducts[] SeedOrderProducts =
        //{
        //    // Перше замовлення
        //    new OrderProducts
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderId = SeedOrders[0].Id, // Використовуємо Id першого замовлення
        //        ProductId = SeedProducts[0].Id, // Використовуємо Id першого продукту
        //        Quantity = 1
        //    },
        //    new OrderProducts
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderId = SeedOrders[0].Id,
        //        ProductId = SeedProducts[1].Id,
        //        Quantity = 2
        //    },

        //    // Друге замовлення
        //    new OrderProducts
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderId = SeedOrders[1].Id,
        //        ProductId = SeedProducts[2].Id,
        //        Quantity = 3
        //    },

        //    // Третє замовлення
        //    new OrderProducts
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderId = SeedOrders[2].Id,
        //        ProductId = SeedProducts[3].Id,
        //        Quantity = 1
        //    },
        //    new OrderProducts
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderId = SeedOrders[2].Id,
        //        ProductId = SeedProducts[4].Id,
        //        Quantity = 4
        //    }
        //};
        //// Замовлення за замовчуванням
        //private static readonly Order[] SeedOrders =
        //{
        //    new Order
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderDate = DateTime.UtcNow.AddDays(-5)
        //    },
        //    new Order
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderDate = DateTime.UtcNow.AddDays(-4)
        //    },
        //    new Order
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderDate = DateTime.UtcNow.AddDays(-3)
        //    },
        //    new Order
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderDate = DateTime.UtcNow.AddDays(-2)
        //    },
        //    new Order
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderDate = DateTime.UtcNow.AddDays(-1)
        //    }
        //};
        private static readonly Product[] SeedProducts =
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Laptop",
                Price = 999.99m,
                Description = "High performance laptop for gaming and work"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smartphone",
                Price = 599.99m,
                Description = "Latest smartphone with 5G connectivity"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Headphones",
                Price = 79.99m,
                Description = "Noise-cancelling over-ear headphones"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Monitor",
                Price = 199.99m,
                Description = "27-inch 4K UHD monitor for professional use"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Keyboard",
                Price = 49.99m,
                Description = "Mechanical keyboard with RGB backlight"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Mouse",
                Price = 29.99m,
                Description = "Wireless optical mouse with ergonomic design"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Tablet",
                Price = 299.99m,
                Description = "10-inch tablet with high-resolution display"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Printer",
                Price = 149.99m,
                Description = "All-in-one color printer with Wi-Fi support"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "External Hard Drive",
                Price = 89.99m,
                Description = "1TB external hard drive for data backup"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Webcam",
                Price = 59.99m,
                Description = "1080p HD webcam with built-in microphone"
            }
        };

    }
}