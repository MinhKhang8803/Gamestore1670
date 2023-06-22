﻿// <auto-generated />
using System;
using Gamestore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gamestore.Migrations
{
    [DbContext(typeof(GamestoreContext))]
    [Migration("20230622015806_testdb")]
    partial class testdb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gamestore.Areas.Admin.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Gamestore.Areas.Admin.Models.Game", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameID"));

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GameDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("GameID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("Gamestore.Areas.Admin.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<int?>("CartID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("CartID");

                    b.HasIndex("UserID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Gamestore.Areas.Admin.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailID"));

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailID");

                    b.HasIndex("GameID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Gamestore.Areas.Admin.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("UserAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Gamestore.Models.Cart", b =>
                {
                    b.Property<int>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartID"));

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CartID");

                    b.HasIndex("UserID");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Gamestore.Models.CartDetail", b =>
                {
                    b.Property<int>("CartDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartDetailID"));

                    b.Property<int>("CartID")
                        .HasColumnType("int");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartDetailID");

                    b.HasIndex("CartID");

                    b.HasIndex("GameID");

                    b.ToTable("CartDetail");
                });

            modelBuilder.Entity("Gamestore.Areas.Admin.Models.Game", b =>
                {
                    b.HasOne("Gamestore.Areas.Admin.Models.Category", "Category")
                        .WithMany("Games")
                        .HasForeignKey("CategoryID");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Gamestore.Areas.Admin.Models.Order", b =>
                {
                    b.HasOne("Gamestore.Models.Cart", "Carts")
                        .WithMany()
                        .HasForeignKey("CartID");

                    b.HasOne("Gamestore.Areas.Admin.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carts");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Gamestore.Areas.Admin.Models.OrderDetails", b =>
                {
                    b.HasOne("Gamestore.Areas.Admin.Models.Game", "Games")
                        .WithMany("OrderDetails")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gamestore.Areas.Admin.Models.Order", "Orders")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Games");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Gamestore.Models.Cart", b =>
                {
                    b.HasOne("Gamestore.Areas.Admin.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Gamestore.Models.CartDetail", b =>
                {
                    b.HasOne("Gamestore.Models.Cart", "Carts")
                        .WithMany("CartDetails")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gamestore.Areas.Admin.Models.Game", "Games")
                        .WithMany("CartDetails")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carts");

                    b.Navigation("Games");
                });

            modelBuilder.Entity("Gamestore.Areas.Admin.Models.Category", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("Gamestore.Areas.Admin.Models.Game", b =>
                {
                    b.Navigation("CartDetails");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Gamestore.Models.Cart", b =>
                {
                    b.Navigation("CartDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
