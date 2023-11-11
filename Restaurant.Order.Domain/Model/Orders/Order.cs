using Restaurant.Order.Domain.Model.Orders.Events;
using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Domain.Model.Orders
{
    public class Order : AggregateRoot
    {
        public OrderStatus Status { get; set; }
        public string ClientName { get; set; }
        public PriceValue Total { get; set; }
        public DateTime OrderDate { get; set; }

        public DateTime? StartedDate { get; set; }

        public DateTime? AcceptedDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }

        private readonly List<OrderLine> _orderLines;

        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines.ToImmutableList();

        internal Order(string clientName)
        {
            Id = Guid.NewGuid();
            ClientName = clientName;
            OrderDate = DateTime.Now;
            _orderLines = new List<OrderLine>();
            Total = 0;
        }
        
        public void AddItem(Guid itemId, string itemName, int quantity, decimal unitaryPrice)
        {
            var item = _orderLines.Find(x => x.ItemId == itemId);
            if (item is null)
            {
                item = new OrderLine(itemId, itemName, quantity, unitaryPrice);
                _orderLines.Add(item);
            }
            else
            {
                Total -= item.SubTotal;
                item.AddQuantity(quantity);
            }
            Total += item.SubTotal;
        }

        public static Order Create(string clientName)
        {
            return new Order(clientName);
        }

        public void Start()
        {
            if (_orderLines.Count == 0)
            {
                throw new InvalidOperationException("Order must have at least one item");
            }

            StartedDate = DateTime.Now;

            Status = OrderStatus.Started;
            

            var orderLinesStared = _orderLines
                .Select(x =>
                new OrderStarted.OrderLineStarted(x.ItemId, x.ItemName, x.Quantity, x.UnitaryPrice))
                .ToList();
            AddDomainEvent(new OrderStarted(Id, ClientName, Total, OrderDate, orderLinesStared));
        }

    }
}
