using MassTransit;
using MassTransit.Mediator;
using MediatR;
using Quartz;
using Restaurant.Order.Application.OutBox;
using Restaurant.Order.Application.Services;
using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class OutboxProcessor : IJob
{
    private readonly IOutboxService _outboxService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly MediatR.IMediator _mediator;

    public OutboxProcessor(IOutboxService outboxService, IUnitOfWork unitOfWork, MediatR.IMediator mediator)
    {
        _outboxService = outboxService;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _outboxService.GetUnprocessedMessages();

        foreach (var item in messages)
        {
            Type type = typeof(OutboxMessage<>)
                    .MakeGenericType(item.Content.GetType());

            var confirmedEvent = (INotification)Activator
                    .CreateInstance(type, item.Content);

            await _mediator.Publish(confirmedEvent);

            item.MarkAsProcessed();

            await _unitOfWork.Commit();
        }

    }
}
