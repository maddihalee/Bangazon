using Microsoft.EntityFrameworkCore;

namespace Bangazon.Models;

    public class BangazonDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }

    
        public BangazonDbContext(DbContextOptions<BangazonDbContext> context) : base(context) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User { Id = 1, UserName = "Test", Password = "Test1234", CustomerId = 1, isSeller = false},
        });

        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product { Id = 1, Name = "Fourth Wing", Description = "Twenty-year-old Violet Sorrengail was supposed to enter the Scribe Quadrant, living a quiet life among books and history.", Price = 18.49M, Image = "https://m.media-amazon.com/images/I/51AC2+BVowL._SX307_BO1,204,203,200_.jpg"},
            new Product { Id = 2, Name = "A Court of Thorns and Roses", Description = "When 19-year-old huntress Feyre kills a wolf in the woods, a beast-like creature arrives to demand retribution for it. Dragged to a treacherous magical land she only knows about from legends, Feyre discovers that her captor is not an animal, but Tamlin - one of the lethal, immortal faeries who once ruled their world.", Price = 17.59M, Image = "https://m.media-amazon.com/images/I/417IU0f5jwL._SX327_BO1,204,203,200_.jpg"},
            new Product { Id = 3, Name = "World of Warcraft", Description = "Set in the fictional world of Azeroth, WoW allows players to create avatar-style characters and explore a sprawling universe while interacting with nonreal players", Price = 59.99M, Image = "https://upload.wikimedia.org/wikipedia/en/thumb/6/65/World_of_Warcraft.png/220px-World_of_Warcraft.png"},
            new Product { Id = 4, Name = "Destiny 2", Description = "a first-person shooter game that incorporates role-playing and massively multiplayer online game (MMO) elements", Price = 59.99M, Image = "https://upload.wikimedia.org/wikipedia/en/0/05/Destiny_2_%28artwork%29.jpg"},
            new Product { Id = 5, Name = "Basketball", Description = "basketball", Price = 1000M, Image = ""},
            new Product { Id = 6, Name = "Football", Description = "football", Price = 59.99M, Image = ""}
        });

        modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
        {
            new PaymentType { Id = 1, Type = "Debit Card"},
            new PaymentType { Id = 2, Type = "Credit Card"},
            new PaymentType { Id = 3, Type = "Gift Card"},
            new PaymentType { Id = 4, Type = "AfterPay"}
        });

        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { Id = 123, UserId = 1}
        });

        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category { Id = 543, Name = "Books"},
            new Category { Id = 544, Name = "Video Games"},
            new Category { Id = 545, Name = "Sports Equipment"}
        });

        //var Fortnite = new Product { ProductId = 7, Name = "Fortnite", Description = "Fortnite", Price = 29.99M, Image = "https://m.media-amazon.com/images/M/MV5BNzU2YTY2OTgtZGZjZi00MTAyLThlYjUtMWM5ZmYzOGEyOWJhXkEyXkFqcGdeQXVyNTgyNTA4MjM@._V1_FMjpg_UX1000_.jpg" };
        //var Order1 = new Order { OrderId = 124, UserId = 1 };

        var orderProduct = modelBuilder.Entity("OrderProduct");
        orderProduct.HasData(
            new { ProductsId = 1, OrdersId = 123 },
            new { ProductsId = 3, OrdersId = 123 }
           );

        var categoryProduct = modelBuilder.Entity("CategoryProduct");
        categoryProduct.HasData(
            new { ProductsId = 1, CategoriesId = 543 },
            new { ProductsId = 3, CategoriesId = 544 });
    }
}

