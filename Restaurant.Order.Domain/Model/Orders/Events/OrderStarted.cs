using MediatR;
using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Domain.Model.Orders.Events
{
    public record OrderStarted : DomainEvent, INotification
    {
        public Guid OrderId { get; init; }
        public string ClientName { get; set; }

        public decimal Total { get; set; }

        public DateTime CreationDate { get; set; }

        public List<OrderLineStarted> OrderLines { get; set; }

        public OrderStarted(Guid orderId, string clientName, decimal total, DateTime creationDate, List<OrderLineStarted> orderLines) : base(occuredOn: DateTime.Now)
        {
            OrderId = orderId;
            ClientName = clientName;
            Total = total;
            CreationDate = creationDate;
            OrderLines = orderLines;
        }

        public record OrderLineStarted(Guid ItemId, string name, int Quantity, decimal UnitaryPrice);
    }
}
