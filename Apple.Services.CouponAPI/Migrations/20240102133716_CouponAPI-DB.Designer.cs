﻿// <auto-generated />
using System;
using Apple.Services.CouponAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Apple.Services.CouponAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240102133716_CouponAPI-DB")]
    partial class CouponAPIDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Apple.Services.CouponAPI.Models.Coupon", b =>
                {
                    b.Property<int>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CouponId"));

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("DiscountAmount")
                        .HasColumnType("float");

                    b.Property<int>("MinAmount")
                        .HasColumnType("int");

                    b.HasKey("CouponId");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            CouponId = 1,
                            CouponCode = "10OFF",
                            CreatedDateTime = new DateTime(2024, 1, 2, 13, 37, 15, 421, DateTimeKind.Utc).AddTicks(8290),
                            DiscountAmount = 10.0,
                            MinAmount = 10
                        },
                        new
                        {
                            CouponId = 2,
                            CouponCode = "20OFF",
                            CreatedDateTime = new DateTime(2024, 1, 2, 13, 37, 15, 421, DateTimeKind.Utc).AddTicks(8325),
                            DiscountAmount = 20.0,
                            MinAmount = 20
                        });
                });
#pragma warning restore 612, 618
        }
    }
}