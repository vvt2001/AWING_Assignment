﻿// <auto-generated />
using System;
using AWING_Assignment_Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AWING_Assignment_Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241219133729_InputHistories")]
    partial class InputHistories
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AWING_Assignment_Data.Model.InputHistory", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("m")
                        .HasColumnType("int");

                    b.Property<string>("matrix")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("n")
                        .HasColumnType("int");

                    b.Property<int>("p")
                        .HasColumnType("int");

                    b.Property<DateTime>("time")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("inputHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
