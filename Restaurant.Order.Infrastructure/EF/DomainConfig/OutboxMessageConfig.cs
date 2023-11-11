using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Restaurant.Order.Application.OutBox;
using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Infrastructure.EF.DomainConfig;

internal class OutboxMessageConfig : IEntityTypeConfiguration<OutboxMessage<DomainEvent>>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Application.OutBox.OutboxMessage<DomainEvent>> builder)
    {
        builder.ToTable("outboxMessage");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("outboxId");

        builder.Property(x => x.Created)
            .HasColumnName("created");

        builder.Property(x => x.Type)
            .HasColumnName("type");

        builder.Property(x => x.Processed)
            .HasColumnName("processed");

        builder.Property(x => x.ProcessedOn)
            .HasColumnName("processedOn");

        var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        var contentConverter = new ValueConverter<DomainEvent, string>(
           obj => JsonConvert.SerializeObject(obj, jsonSettings),
           stringValue =>  JsonConvert.DeserializeObject<DomainEvent>(stringValue, jsonSettings)
       );

        builder.Property(x => x.Content)
            .HasConversion(contentConverter)
            .HasColumnName("content");

    }
}
