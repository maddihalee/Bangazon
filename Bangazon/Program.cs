using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<BangazonDbContext>(builder.Configuration["BangazonDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Get all Products
app.MapGet("api/products", (BangazonDbContext db) =>
{
    return db.Products.ToList();
});

// Get product details
app.MapGet("api/products/{id}", (BangazonDbContext db, int id) =>
{
    Product product = db.Products.SingleOrDefault(pr => pr.Id == id);
    return product;
});

// delete product
app.MapDelete("api/products/{id}", (BangazonDbContext db, int id) =>
{
    Product product = db.Products.SingleOrDefault(pr => pr.Id == id);
    if (product == null)
    {
        return Results.NotFound();
    }
    db.Products.Remove(product);
    db.SaveChanges();
    return Results.NoContent();
});

// get user by id
app.MapGet("api/users/{id}", (BangazonDbContext db, int id) =>
{
    User user = db.Users.SingleOrDefault(user => user.Id == id);
    return user;
});

// get product categories
// create a category
// create product
// delete order
// create order
// GET order with products by OrderId
// GET Product with related order (get product, see what order it's associated with)
// get all products that have orders
// Get a 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
