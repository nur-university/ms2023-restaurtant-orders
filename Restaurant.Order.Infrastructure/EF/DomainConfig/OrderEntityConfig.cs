using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restaurant.Order.Domain.Model.Orders;
using Restaurant.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Infrastructure.EF.DomainConfig;

internal class OrderEntityConfig : IEntityTypeConfiguration<Domain.Model.Orders.Order>,
    IEntityTypeConfiguration<Domain.Model.Orders.OrderLine>
{
    public void Configure(EntityTypeBuilder<Domain.Model.Orders.Order> builder)
    {
        builder.ToTable("order");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("orderId");


        builder.Property(x => x.ClientName)
            .HasColumnName("clientName");

        var priceConverter = new ValueConverter<PriceValue, decimal>(
            priceValue => priceValue.Value,
            price => new PriceValue(price)
        );

        builder.Property(x => x.Total)
            .HasConversion(priceConverter)
            .HasColumnName("total");

        builder.Property(x => x.OrderDate)
            .HasColumnName("orderDate");

        builder.Property(x => x.StartedDate)
            .HasColumnName("startedDate");

        builder.Property(x => x.AcceptedDate)
            .HasColumnName("acceptedDate");

        builder.Property(x => x.ConfirmedDate)
            .HasColumnName("confirmedDate");

        builder.HasMany(typeof(OrderLine), "_orderLines");

        builder.Ignore("_domainEvents");
        builder.Ignore(x => x.DomainEvents);
        builder.Ignore(x => x.OrderLines);
    }

    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable("orderLine");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("orderLineId");

        builder.Property(x => x.ItemId)
            .HasColumnName("itemId");

        builder.Property(x => x.ItemName)
            .HasColumnName("itemName");

        var priceConverter = new ValueConverter<PriceValue, decimal>(
            priceValue => priceValue.Value,
            price => new PriceValue(price)
        );

        builder.Property(x => x.UnitaryPrice)
            .HasConversion(priceConverter)
            .HasColumnName("unitaryPrice");

        builder.Property(x => x.SubTotal)
            .HasConversion(priceConverter)
            .HasColumnName("subTotal");

        var quantityConverter = new ValueConverter<QuantityValue, int>(
            quantityValue => quantityValue.Value,
            quantity => new QuantityValue(quantity)
        );

        builder.Property(x => x.Quantity)
            .HasConversion(quantityConverter)
            .HasColumnName("quantity");

        builder.Ignore("_domainEvents");
        builder.Ignore(x => x.DomainEvents);
    }
}
