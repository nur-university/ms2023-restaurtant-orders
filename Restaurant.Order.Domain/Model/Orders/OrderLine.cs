using Restaurant.SharedKernel.Core;
using Restaurant.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Domain.Model.Orders
{
    public class OrderLine : Entity
    {
        public Guid ItemId { get; private set; }

        public string ItemName { get; private set; }
        public QuantityValue Quantity { get; private set; }
        public PriceValue UnitaryPrice { get; private set; }
        public PriceValue SubTotal { get; private set; }

        internal OrderLine(Guid itemId, string itemName,
            int quantity, decimal unitaryPrice)
        {
            Id = Guid.NewGuid();
            ItemId = itemId;
            ItemName = itemName;
            Quantity = quantity;
            UnitaryPrice = unitaryPrice;
            SubTotal = quantity * unitaryPrice;
        }

        private OrderLine() { }

        internal void AddQuantity(int quantity)
        {
            Quantity += quantity;
            SubTotal = Quantity * UnitaryPrice;
        }
    }
}
