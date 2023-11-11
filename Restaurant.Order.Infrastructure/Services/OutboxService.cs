using Microsoft.EntityFrameworkCore;
using Restaurant.Order.Application.OutBox;
using Restaurant.Order.Application.Services;
using Restaurant.Order.Infrastructure.EF.Contexts;
using Restaurant.SharedKernel.Core;

namespace Restaurant.Order.Infrastructure.Services
{
    internal class OutboxService : IOutboxService
    {
        private readonly DomainDbContext _dbContext;

        public OutboxService(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(DomainEvent content)
        {
            var outboxMessage = new OutboxMessage<DomainEvent>(content);

            await _dbContext.OutboxMessage.AddAsync(outboxMessage);
        }

        public async Task<ICollection<OutboxMessage<DomainEvent>>> GetUnprocessedMessages(int pageSize = 20)
        {
            return await _dbContext.OutboxMessage.Where(x => !x.Processed).Take(pageSize).ToListAsync();
        }

    }
}
