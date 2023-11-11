using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Infrastructure.EF.PersistenceModel;

[Table("orderLine")]
internal class OrderLinePersistenceModel
{
    [Key]
    [Column("orderLineId")]
    public Guid Id { get; set; }

    [Column("itemId")]
    [Required]
    public Guid ItemId { get; set; }

    [Column("itemName", TypeName ="nvarchar(150)")]
    [Required]
    public string ItemName { get; set; }

    [Column("quantity", TypeName = "int")]
    public int Quantity { get; set; }
    
    [Column("unitaryPrice", TypeName = "decimal(12,2)")]
    [Required]
    public decimal UnitaryPrice { get; set; }

    [Column("subTotal", TypeName = "decimal(12,2)")]
    [Required]
    public decimal SubTotal { get; set; }

    [Column("orderId")]
    [Required]
    public Guid OrderId { get; set; }

    public OrderPersistenceModel Order { get; set; }
}
