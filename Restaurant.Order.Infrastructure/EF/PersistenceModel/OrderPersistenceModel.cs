using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Infrastructure.EF.PersistenceModel;

[Table("order")]
internal class OrderPersistenceModel
{
    [Key]
    [Column("orderId")]
    public Guid Id { get; set; }

    [Column("clientName", TypeName = "nvarchar(250)")]
    [Required]
    public string ClientName { get; set; } = "";

    [Column("status", TypeName = "nvarchar(50)")]
    [Required]
    public string Status { get; set; } = "";

    [Column("total", TypeName = "decimal(12,2)")]
    [Required]
    public decimal Total { get; set; }

    [Column("orderDate", TypeName = "datetime")]
    [Required]
    public DateTime OrderDate { get; set; }
    


    [Column("startedDate", TypeName = "datetime")]
    [AllowNull]
    public DateTime? StartedDate { get; set; }
    

    [Column("acceptedDate", TypeName = "datetime")]
    [AllowNull]
    public DateTime? AcceptedDate { get; set; }
    

    [Column("confirmedDate", TypeName = "datetime")]
    [AllowNull]
    public DateTime? ConfirmedDate { get; set; }


    public List<OrderLinePersistenceModel> OrderLines { get; set; }



}
