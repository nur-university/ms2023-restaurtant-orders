using MediatR;
using Restaurant.Order.Application.Services;
using Restaurant.Order.Domain.Model.Orders.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Application.OutBox.Handlers
{
    internal class PublishToServiesWhenOutboxOrderStarted : INotificationHandler<OutboxMessage<OrderStarted>>
    {
        private readonly IBusService _busService;

        public PublishToServiesWhenOutboxOrderStarted(IBusService busService)
        {
            _busService = busService;
        }

        public async Task Handle(OutboxMessage<OrderStarted> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.Content;

            IntegrationEvents.OrderStarted integrationEvent = new IntegrationEvents.OrderStarted(
                domainEvent.OrderId,
                domainEvent.ClientName,
                domainEvent.Total,
                domainEvent.CreationDate,
                domainEvent.OrderLines.Select(x => new IntegrationEvents.OrderStarted.OrderLineStarted(
                    x.ItemId,
                    x.name,
                    x.Quantity,
                    x.UnitaryPrice
                )).ToList());


            await _busService.PublishAsync(integrationEvent);
        }
    }
}
