using Microsoft.EntityFrameworkCore;

namespace Bangazon.Models;

    public class BangazonDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }
        public DbSet<UserPaymentType> UserPaymentTypes { get; set; }

    
        public BangazonDbContext(DbContextOptions<BangazonDbContext> context) : base(context) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User { UserId = 1, UserName = "Test", CustomerId = 1, isSeller = false},
        });

        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product { ProductId = 1, Name = "Fourth Wing", Description = "Twenty-year-old Violet Sorrengail was supposed to enter the Scribe Quadrant, living a quiet life among books and history.", Price = 18.49M, Image = "https://m.media-amazon.com/images/I/51AC2+BVowL._SX307_BO1,204,203,200_.jpg", CategoryId = 1, },
            new Product { ProductId = 2, Name = "A Court of Thorns and Roses", Description = "When 19-year-old huntress Feyre kills a wolf in the woods, a beast-like creature arrives to demand retribution for it. Dragged to a treacherous magical land she only knows about from legends, Feyre discovers that her captor is not an animal, but Tamlin - one of the lethal, immortal faeries who once ruled their world.", Price = 17.59M, Image = "https://m.media-amazon.com/images/I/417IU0f5jwL._SX327_BO1,204,203,200_.jpg", CategoryId = 1},
            new Product { ProductId = 3, Name = "World of Warcraft", Description = "Set in the fictional world of Azeroth, WoW allows players to create avatar-style characters and explore a sprawling universe while interacting with nonreal players", Price = 59.99M, Image = "https://upload.wikimedia.org/wikipedia/en/thumb/6/65/World_of_Warcraft.png/220px-World_of_Warcraft.png", CategoryId = 2},
            new Product { ProductId = 4, Name = "Destiny 2", Description = "a first-person shooter game that incorporates role-playing and massively multiplayer online game (MMO) elements", Price = 59.99M, Image = "https://upload.wikimedia.org/wikipedia/en/0/05/Destiny_2_%28artwork%29.jpg", CategoryId = 2 }
        });

        modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
        {
            new PaymentType { PaymentTypeId = 1, Type = "Debit Card"},
            new PaymentType { PaymentTypeId = 2, Type = "Credit Card"},
            new PaymentType { PaymentTypeId = 3, Type = "Gift Card"},
            new PaymentType { PaymentTypeId = 4, Type = "AfterPay"}
        });

        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { OrderId = 123, UserId = 1}
        });

        modelBuilder.Entity<OrderProduct>().HasData(new OrderProduct[]
        {
            new OrderProduct { OrderProductId = 1, ProductId = 1, OrderId = 123 },
            new OrderProduct { OrderProductId = 2, ProductId = 3, OrderId = 123 },
            new OrderProduct { OrderProductId = 3, ProductId = 4, OrderId = 123 },
        });

        modelBuilder.Entity<UserOrder>().HasData(new UserOrder[]
        {
            new UserOrder { UserOrderId = 1, UserId = 1, OrderId = 1},
        });

        modelBuilder.Entity<UserPaymentType>().HasData(new UserPaymentType[]
        {
            new UserPaymentType { UserPaymentTypeId = 1, UserId = 1, PaymentTypeId = 3}
        });
    }
}

