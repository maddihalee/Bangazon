using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
//using Microsoft.AspNetCore.Mvc;

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
app.MapGet("api/categories", (BangazonDbContext db) =>
{
    return db.Categories.ToList();
});

// create a category
app.MapPost("api/categories", (BangazonDbContext db, Category category) =>
{
    db.Categories.Add(category);
    db.SaveChanges();
    return Results.Created($"/api/categories/{category.Id}", category);
});

// Delete a category
app.MapDelete("api/categories", (BangazonDbContext db, int id) =>
{
    Category category = db.Categories.SingleOrDefault(category => category.Id == id);
    if (category == null)
    {
        return Results.NotFound();
    }
    db.Categories.Remove(category);
    db.SaveChanges();
    return Results.NoContent();
});

// create product
app.MapPost("api/products", (BangazonDbContext db, Product product) =>
{
    db.Products.Add(product);
    db.SaveChanges();
    return Results.Created($"/api/products/{product.Id}", product);
});

// delete order
app.MapDelete("api/orders/{id}", (BangazonDbContext db, int id) =>
{
    Order order = db.Orders.SingleOrDefault(o => o.Id == id);
    if (order == null)
    {
        return Results.NotFound();
    }
    db.Orders.Remove(order);
    db.SaveChanges();
    return Results.NoContent();
});

// create order
app.MapPost("api/orders", (BangazonDbContext db, Order order) =>
{
    db.Orders.Add(order);
    db.SaveChanges();
    return Results.Created($"/api/orders/{order.Id}", order);
});

// get all orders
app.MapGet("/api/orders", (BangazonDbContext db) => {
    return db.Orders.ToList();
});

// GET order with products by OrderId
app.MapGet("api/orders/{id}/products", (BangazonDbContext db, int id) =>
{
    Order order = db.Orders
    .Include(o => o.Products)
    .SingleOrDefault(o => o.Id == id);

    return Results.Ok(order);
});

// GET Product with related order (get product, see what order it's associated with)
app.MapGet("api/products/{id}/orders", (BangazonDbContext db, int id) =>
{
    Product product = db.Products
    .Include(p => p.Orders)
    .FirstOrDefault(p => p.Id == id);

    return Results.Ok(product);
});

// Update Order
app.MapPut("/api/orders/{id}", (BangazonDbContext db, int id, Order order) =>
{
    Order orderToUpdate = db.Orders.SingleOrDefault(order =>  order.Id == id);  
    if (orderToUpdate == null)
    {
        return Results.NotFound();
    }
    orderToUpdate.UserId = order.UserId;

    db.SaveChanges();
    return Results.NoContent();
});

// Update a Product
app.MapPut("api/products/{id}", (BangazonDbContext db, int id, Product product) =>
{
    Product productToUpdate = db.Products.SingleOrDefault(product => product.Id == id);
    if (productToUpdate == null)
    {
        return Results.NotFound();
    }
    productToUpdate.Name = product.Name;
    productToUpdate.Description = product.Description;
    productToUpdate.Price = product.Price;
    productToUpdate.Image = product.Image;

    db.SaveChanges();
    return Results.NoContent();
});

// Update User
app.MapPut("/api/user/{id}", (BangazonDbContext db, User user, int id) =>
{
    User userToUpdate = db.Users.SingleOrDefault(o => o.Id == id);
    if (userToUpdate == null)
    {
        return Results.NotFound();
    }
    userToUpdate.UserName = user.UserName;
    userToUpdate.CustomerId = user.CustomerId;
    userToUpdate.isSeller = user.isSeller;

    db.SaveChanges();
    return Results.Created($"/api/user/user.Id", user);
});

// Get Payment Types
app.MapGet("/api/paymentTypes", (BangazonDbContext db) =>
{
    return db.PaymentTypes.ToList();
});

// create payment types
app.MapPost("/api/paymentTypes", (BangazonDbContext db, PaymentType paymentType) =>
{
    db.PaymentTypes.Add(paymentType);
    db.SaveChanges();
    return Results.Created($"/api/paymentTypes/{paymentType.Id}", paymentType);
});

// Delete a product from an order 2.0
app.MapDelete("api/orders/{orderId}/products/{productId}", (BangazonDbContext db, int orderId, int productId) =>
{
    var order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == orderId);
    if (order == null)
    {
        return Results.NotFound();
    }
    var productToRemove = order.Products.FirstOrDefault(p => p.Id == productId);
    if (productToRemove == null)
    {
        return Results.NotFound();
    }
    order.Products.Remove(productToRemove);
    db.SaveChanges();
    return Results.NoContent();
});

// Add a product to an order
app.MapPost("api/orders/{orderId}/products", (BangazonDbContext db, int orderId, int productId) =>
{
    Order order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == orderId);
    if (order == null)
    {
        return Results.NotFound();
    }
    Product productToAdd = db.Products.FirstOrDefault(p => p.Id == productId);
    order.Products.Add(productToAdd);
    db.SaveChanges();
    return Results.Created($"/api/orders/{orderId}/products/{productToAdd.Id}", productToAdd);
});

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
