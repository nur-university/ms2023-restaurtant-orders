using Microsoft.EntityFrameworkCore;
using Restaurant.Order.Domain.Repositories;
using Restaurant.Order.Infrastructure.EF.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Infrastructure.EF.Repository
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly DomainDbContext _dbContext;

        public OrderRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Domain.Model.Orders.Order obj)
        {
            await _dbContext.Orders.AddAsync(obj);
        }

        public async Task<Domain.Model.Orders.Order?> FindByIdAsync(Guid id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
