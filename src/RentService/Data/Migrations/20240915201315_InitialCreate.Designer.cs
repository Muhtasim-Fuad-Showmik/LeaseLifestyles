﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RentService.Data;

#nullable disable

namespace RentService.Data.Migrations
{
    [DbContext(typeof(RentDbContext))]
    [Migration("20240915201315_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RentService.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("AvailableFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Balconies")
                        .HasColumnType("integer");

                    b.Property<int>("Baths")
                        .HasColumnType("integer");

                    b.Property<int>("Beds")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("HouseSize")
                        .HasColumnType("integer");

                    b.Property<int>("HouseSizeUnit")
                        .HasColumnType("integer");

                    b.Property<int?>("LandSize")
                        .HasColumnType("integer");

                    b.Property<int?>("LandSizeUnit")
                        .HasColumnType("integer");

                    b.Property<Guid>("RentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RentId")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("RentService.Entities.Rent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ConfirmedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CurrentHighBid")
                        .HasColumnType("integer");

                    b.Property<string>("Landlord")
                        .HasColumnType("text");

                    b.Property<string>("LandlordContactNo")
                        .HasColumnType("text");

                    b.Property<int?>("RentAmount")
                        .HasColumnType("integer");

                    b.Property<int>("ReservedPrice")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Tennant")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("RentService.Entities.Item", b =>
                {
                    b.HasOne("RentService.Entities.Rent", "Rent")
                        .WithOne("Item")
                        .HasForeignKey("RentService.Entities.Item", "RentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rent");
                });

            modelBuilder.Entity("RentService.Entities.Rent", b =>
                {
                    b.Navigation("Item");
                });
#pragma warning restore 612, 618
        }
    }
}
