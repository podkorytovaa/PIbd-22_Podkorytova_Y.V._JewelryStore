﻿// <auto-generated />
using System;
using JewelryStoreDatabaseImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JewelryStoreDatabaseImplement.Migrations
{
    [DbContext(typeof(JewelryStoreDatabase))]
    [Migration("20220302171411_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("JewelryStoreDatabaseImplement.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("JewelryStoreDatabaseImplement.Models.Jewel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("JewelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Jewels");
                });

            modelBuilder.Entity("JewelryStoreDatabaseImplement.Models.JewelComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ComponentId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("JewelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("JewelId");

                    b.ToTable("JewelComponents");
                });

            modelBuilder.Entity("JewelryStoreDatabaseImplement.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateImplement")
                        .HasColumnType("datetime2");

                    b.Property<int>("JewelId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("JewelId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("JewelryStoreDatabaseImplement.Models.JewelComponent", b =>
                {
                    b.HasOne("JewelryStoreDatabaseImplement.Models.Component", "Component")
                        .WithMany("JewelComponents")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JewelryStoreDatabaseImplement.Models.Jewel", "Jewel")
                        .WithMany("JewelComponents")
                        .HasForeignKey("JewelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");

                    b.Navigation("Jewel");
                });

            modelBuilder.Entity("JewelryStoreDatabaseImplement.Models.Order", b =>
                {
                    b.HasOne("JewelryStoreDatabaseImplement.Models.Jewel", "Jewel")
                        .WithMany("Orders")
                        .HasForeignKey("JewelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jewel");
                });

            modelBuilder.Entity("JewelryStoreDatabaseImplement.Models.Component", b =>
                {
                    b.Navigation("JewelComponents");
                });

            modelBuilder.Entity("JewelryStoreDatabaseImplement.Models.Jewel", b =>
                {
                    b.Navigation("JewelComponents");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
