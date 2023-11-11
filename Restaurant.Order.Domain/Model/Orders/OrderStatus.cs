using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Domain.Model.Orders
{
    public enum OrderStatus
    {
        Started,
        Accepted,
        Confirmed,
        Cancelled
    }
}
