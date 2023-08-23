using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bangazon.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    isSeller = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "OrderId", "ProductId" },
                values: new object[,]
                {
                    { 1, 123, 1 },
                    { 2, 123, 3 },
                    { 3, 123, 4 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderId", "OrderStatus", "UserId" },
                values: new object[] { 123, 0, 0, 1 });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Debit Card" },
                    { 2, "Credit Card" },
                    { 3, "Gift Card" },
                    { 4, "AfterPay" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Twenty-year-old Violet Sorrengail was supposed to enter the Scribe Quadrant, living a quiet life among books and history.", "https://m.media-amazon.com/images/I/51AC2+BVowL._SX307_BO1,204,203,200_.jpg", "Fourth Wing", 18.49m },
                    { 2, 1, "When 19-year-old huntress Feyre kills a wolf in the woods, a beast-like creature arrives to demand retribution for it. Dragged to a treacherous magical land she only knows about from legends, Feyre discovers that her captor is not an animal, but Tamlin - one of the lethal, immortal faeries who once ruled their world.", "https://m.media-amazon.com/images/I/417IU0f5jwL._SX327_BO1,204,203,200_.jpg", "A Court of Thorns and Roses", 17.59m },
                    { 3, 2, "Set in the fictional world of Azeroth, WoW allows players to create avatar-style characters and explore a sprawling universe while interacting with nonreal players", "https://upload.wikimedia.org/wikipedia/en/thumb/6/65/World_of_Warcraft.png/220px-World_of_Warcraft.png", "World of Warcraft", 59.99m },
                    { 4, 2, "a first-person shooter game that incorporates role-playing and massively multiplayer online game (MMO) elements", "https://upload.wikimedia.org/wikipedia/en/0/05/Destiny_2_%28artwork%29.jpg", "Destiny 2", 59.99m }
                });

            migrationBuilder.InsertData(
                table: "UserOrders",
                columns: new[] { "Id", "OrderId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "UserPaymentTypes",
                columns: new[] { "Id", "PaymentTypeId", "UserId" },
                values: new object[] { 1, 3, 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CustomerId", "UserName", "isSeller", "userId" },
                values: new object[] { 1, 1, "Test", false, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserOrders");

            migrationBuilder.DropTable(
                name: "UserPaymentTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
