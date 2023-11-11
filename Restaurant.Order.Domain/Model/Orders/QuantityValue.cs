using Restaurant.SharedKernel.Core;
using Restaurant.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Domain.Model.Orders
{
    public record QuantityValue : ValueObject
    {
        public int Value { get; }
        public QuantityValue(int value)
        {
            if (value < 0)
            {
                throw new BussinessRuleValidationException("Quantity cannot be equals or less than zero");
            }
            Value = value;
        }

        public static implicit operator int(QuantityValue value)
        {
            return value.Value;
        }

        public static implicit operator QuantityValue(int value)
        {
            return new QuantityValue(value);
        }
    }
}
