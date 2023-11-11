using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Model.Orders.Order, Guid>
    {
        
    }
}
