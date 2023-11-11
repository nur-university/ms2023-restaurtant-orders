using Restaurant.SharedKernel.Core;
using Restaurant.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Domain.Model.Orders
{
    public record PriceValue : ValueObject
    {
        public decimal Value { get; }
        public PriceValue(decimal value)
        {
            if (value < 0)
            {
                throw new BussinessRuleValidationException("Price value cannot be negative");
            }
            Value = value;
        }

        public static implicit operator decimal(PriceValue value)
        {
            return value.Value;
        }

        public static implicit operator PriceValue(decimal value)
        {
            return new PriceValue(value);
        }
    }
}
