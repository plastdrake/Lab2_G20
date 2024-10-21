﻿// <auto-generated />
using System;
using Lab2_G20.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lab2_G20.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241021080735_Labb2")]
    partial class Labb2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Lab2_G20.Models.Crop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("HarvestDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PlantingDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Crops");
                });
#pragma warning restore 612, 618
        }
    }
}
