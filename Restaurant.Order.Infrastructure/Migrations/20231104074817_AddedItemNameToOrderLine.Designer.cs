﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restaurant.Order.Infrastructure.EF.Contexts;

#nullable disable

namespace Restaurant.Order.Infrastructure.Migrations
{
    [DbContext(typeof(PersistenceDbContext))]
    [Migration("20231104074817_AddedItemNameToOrderLine")]
    partial class AddedItemNameToOrderLine
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Restaurant.Order.Infrastructure.EF.PersistenceModel.OrderLinePersistenceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("orderLineId");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("itemId");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("itemName");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("orderId");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(12,2)")
                        .HasColumnName("subTotal");

                    b.Property<decimal>("UnitaryPrice")
                        .HasColumnType("decimal(12,2)")
                        .HasColumnName("unitaryPrice");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("orderLine");
                });

            modelBuilder.Entity("Restaurant.Order.Infrastructure.EF.PersistenceModel.OrderPersistenceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("orderId");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("clientName");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime")
                        .HasColumnName("orderDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("status");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(12,2)")
                        .HasColumnName("total");

                    b.HasKey("Id");

                    b.ToTable("order");
                });

            modelBuilder.Entity("Restaurant.Order.Infrastructure.EF.PersistenceModel.OutboxPersistenceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("outboxId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("created");

                    b.Property<bool>("Processed")
                        .HasColumnType("bit")
                        .HasColumnName("processed");

                    b.Property<DateTime?>("ProcessedOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("processedOn");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("outboxMessage");
                });

            modelBuilder.Entity("Restaurant.Order.Infrastructure.EF.PersistenceModel.OrderLinePersistenceModel", b =>
                {
                    b.HasOne("Restaurant.Order.Infrastructure.EF.PersistenceModel.OrderPersistenceModel", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Restaurant.Order.Infrastructure.EF.PersistenceModel.OrderPersistenceModel", b =>
                {
                    b.Navigation("OrderLines");
                });
#pragma warning restore 612, 618
        }
    }
}
