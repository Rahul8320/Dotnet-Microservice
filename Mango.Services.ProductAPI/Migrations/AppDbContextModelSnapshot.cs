﻿// <auto-generated />
using System;
using Mango.Services.ProductAPI.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mango.Services.ProductAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-rc.1.23419.6");

            modelBuilder.Entity("Mango.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = new Guid("2fde16f1-1eaf-44f7-af35-7e7a49eea0b9"),
                            CategoryName = "Appetizer",
                            CreatedAt = new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(991),
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://www.zedamagazine.com/wp-content/uploads/2018/06/Indian-Food-Samosa-Dish-HD-Wallpapers.jpg",
                            Name = "Samosa",
                            Price = 15.0,
                            UpdatedAt = new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1002)
                        },
                        new
                        {
                            ProductId = new Guid("13c57be2-7174-4cf1-8cc6-2fe660baed91"),
                            CategoryName = "Appetizer",
                            CreatedAt = new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1078),
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "http://www.cookforindia.com/wp-content/uploads/2016/08/Paneer-Tikka-_LR.jpg",
                            Name = "Paneer Tikka",
                            Price = 13.99,
                            UpdatedAt = new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1078)
                        },
                        new
                        {
                            ProductId = new Guid("ff69d67e-1152-4ed0-b206-8360f89cd5f4"),
                            CategoryName = "Dessert",
                            CreatedAt = new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1094),
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://img.sndimg.com/food/image/upload/v1/img/recipes/30/76/6/pic0L9jII.jpg",
                            Name = "Sweet Pie",
                            Price = 10.99,
                            UpdatedAt = new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1095)
                        },
                        new
                        {
                            ProductId = new Guid("e4a9e1fb-af06-4f42-a0ea-b4d0fea4dae6"),
                            CategoryName = "Entree",
                            CreatedAt = new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1109),
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://i.pinimg.com/originals/2d/37/a6/2d37a6a69d254a42ca1a345304120796.jpg",
                            Name = "Pav Bhaji",
                            Price = 15.0,
                            UpdatedAt = new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1110)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}