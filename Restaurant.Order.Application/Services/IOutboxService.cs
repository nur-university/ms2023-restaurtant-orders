using Restaurant.Order.Application.OutBox;
using Restaurant.SharedKernel.Core;

namespace Restaurant.Order.Application.Services;

public interface IOutboxService
{
    Task Add(DomainEvent content);

    //Task Update(OutboxMessage<DomainEvent> message);

    Task<ICollection<OutboxMessage<DomainEvent>>> GetUnprocessedMessages(int pageSize = 20);
}
