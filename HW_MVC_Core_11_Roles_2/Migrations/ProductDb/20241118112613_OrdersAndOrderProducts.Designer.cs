﻿// <auto-generated />
using System;
using HW_MVC_Core_11_Roles_2.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HW_MVC_Core_11_Roles_2.Migrations.ProductDb
{
    [DbContext(typeof(ProductDbContext))]
    [Migration("20241118112613_OrdersAndOrderProducts")]
    partial class OrdersAndOrderProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("products")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HW_MVC_Core_11_Roles_2.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Orders", "products");
                });

            modelBuilder.Entity("HW_MVC_Core_11_Roles_2.Models.OrderProducts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts", "products");
                });

            modelBuilder.Entity("Shop_app.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products", "products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6d455bf2-ae4d-46b4-af45-fcf88a245616"),
                            Description = "High performance laptop for gaming and work",
                            Name = "Laptop",
                            Price = 999.99m
                        },
                        new
                        {
                            Id = new Guid("eff1ae67-5250-4469-8a30-4fa61b1f7dd4"),
                            Description = "Latest smartphone with 5G connectivity",
                            Name = "Smartphone",
                            Price = 599.99m
                        },
                        new
                        {
                            Id = new Guid("ef12476f-05ed-44c4-a2dc-19103cb70286"),
                            Description = "Noise-cancelling over-ear headphones",
                            Name = "Headphones",
                            Price = 79.99m
                        },
                        new
                        {
                            Id = new Guid("4ef890e5-0710-469c-8fea-694aae0a008b"),
                            Description = "27-inch 4K UHD monitor for professional use",
                            Name = "Monitor",
                            Price = 199.99m
                        },
                        new
                        {
                            Id = new Guid("89ae365f-99b7-4c75-b0b2-c14fa205a6c3"),
                            Description = "Mechanical keyboard with RGB backlight",
                            Name = "Keyboard",
                            Price = 49.99m
                        },
                        new
                        {
                            Id = new Guid("8aaf2aa1-67e0-44dd-a12b-431f000d8fa3"),
                            Description = "Wireless optical mouse with ergonomic design",
                            Name = "Mouse",
                            Price = 29.99m
                        },
                        new
                        {
                            Id = new Guid("8532e000-4c71-48b6-8bf9-fe20b9a9f2b4"),
                            Description = "10-inch tablet with high-resolution display",
                            Name = "Tablet",
                            Price = 299.99m
                        },
                        new
                        {
                            Id = new Guid("1554154c-9657-4d78-af8f-bbe89afad9af"),
                            Description = "All-in-one color printer with Wi-Fi support",
                            Name = "Printer",
                            Price = 149.99m
                        },
                        new
                        {
                            Id = new Guid("05590f91-0afd-407e-aa3e-bd1da21c7f24"),
                            Description = "1TB external hard drive for data backup",
                            Name = "External Hard Drive",
                            Price = 89.99m
                        },
                        new
                        {
                            Id = new Guid("09cbfa50-76bb-4f67-8624-829b443da2ed"),
                            Description = "1080p HD webcam with built-in microphone",
                            Name = "Webcam",
                            Price = 59.99m
                        });
                });

            modelBuilder.Entity("HW_MVC_Core_11_Roles_2.Models.OrderProducts", b =>
                {
                    b.HasOne("HW_MVC_Core_11_Roles_2.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop_app.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("HW_MVC_Core_11_Roles_2.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
