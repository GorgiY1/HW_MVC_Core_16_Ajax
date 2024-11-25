using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HW_MVC_Core_11_Roles_2.Migrations.ProductDb
{
    /// <inheritdoc />
    public partial class OrdersAndOrderProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "products");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                schema: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "products",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "products",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "products",
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("05590f91-0afd-407e-aa3e-bd1da21c7f24"), "1TB external hard drive for data backup", "External Hard Drive", 89.99m },
                    { new Guid("09cbfa50-76bb-4f67-8624-829b443da2ed"), "1080p HD webcam with built-in microphone", "Webcam", 59.99m },
                    { new Guid("1554154c-9657-4d78-af8f-bbe89afad9af"), "All-in-one color printer with Wi-Fi support", "Printer", 149.99m },
                    { new Guid("4ef890e5-0710-469c-8fea-694aae0a008b"), "27-inch 4K UHD monitor for professional use", "Monitor", 199.99m },
                    { new Guid("6d455bf2-ae4d-46b4-af45-fcf88a245616"), "High performance laptop for gaming and work", "Laptop", 999.99m },
                    { new Guid("8532e000-4c71-48b6-8bf9-fe20b9a9f2b4"), "10-inch tablet with high-resolution display", "Tablet", 299.99m },
                    { new Guid("89ae365f-99b7-4c75-b0b2-c14fa205a6c3"), "Mechanical keyboard with RGB backlight", "Keyboard", 49.99m },
                    { new Guid("8aaf2aa1-67e0-44dd-a12b-431f000d8fa3"), "Wireless optical mouse with ergonomic design", "Mouse", 29.99m },
                    { new Guid("ef12476f-05ed-44c4-a2dc-19103cb70286"), "Noise-cancelling over-ear headphones", "Headphones", 79.99m },
                    { new Guid("eff1ae67-5250-4469-8a30-4fa61b1f7dd4"), "Latest smartphone with 5G connectivity", "Smartphone", 599.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                schema: "products",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                schema: "products",
                table: "OrderProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts",
                schema: "products");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "products");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "products");
        }
    }
}
