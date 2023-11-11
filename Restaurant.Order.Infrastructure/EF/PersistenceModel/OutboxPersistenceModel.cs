using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Infrastructure.EF.PersistenceModel;

[Table("outboxMessage")]
internal class OutboxPersistenceModel
{
    [Column("outboxId")]
    public Guid Id { get; set; }

    [Column("content")]
    public string Content { get; set; }

    [Column("type")]
    public string Type { get; set; }

    [Column("created")]
    public DateTime Created { get; set; }

    [Column("processed")]
    public bool Processed { get; set; }

    [Column("processedOn")]
    public DateTime? ProcessedOn { get; set; }

    public OutboxPersistenceModel(string content, string type)
    {
        Id = Guid.NewGuid();
        Created = DateTime.Now;
        Processed = false;
        Content = content;
        Type = type;
    }

    public void MarkAsProcessed()
    {
        ProcessedOn = DateTime.Now;
        Processed = true;            
    }

    public OutboxPersistenceModel() { }
}
