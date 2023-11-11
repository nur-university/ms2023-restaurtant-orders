using MediatR;
using Restaurant.Order.Application.Services;
using Restaurant.Order.Domain.Model.Orders.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Application.EventHandlers
{
    internal class AddToOutboxWhenOrderStarted : INotificationHandler<OrderStarted>
    {
        private readonly IOutboxService _outboxService;

        public AddToOutboxWhenOrderStarted(IOutboxService outboxService)
        {
            _outboxService = outboxService;
        }
        public async Task Handle(OrderStarted notification, CancellationToken cancellationToken)
        {
            await _outboxService.Add(notification);
        }
    }
}
