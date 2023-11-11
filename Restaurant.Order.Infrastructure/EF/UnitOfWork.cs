using MediatR;
using Restaurant.Order.Infrastructure.EF.Contexts;
using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Infrastructure.EF;

internal class UnitOfWork : IUnitOfWork
{
    private readonly IMediator _mediator;
    private readonly DomainDbContext _context;

    private int _transactionCounter;

    public UnitOfWork(DomainDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
        _transactionCounter = 0;
    }

    public async Task Commit()
    {
        _transactionCounter++;

        var domainEvents = _context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x =>
            {
                var domainEvents = x.Entity
                                .DomainEvents
                                .ToImmutableArray();
                x.Entity.ClearDomainEvents();

                return domainEvents;
            })
            .SelectMany(domainEvents => domainEvents)
            .ToList();

        foreach (var evento in domainEvents)
        {
            await _mediator.Publish(evento);
        }

        if (_transactionCounter == 1)
        {
            await _context.SaveChangesAsync();
        }
        else
        {
            _transactionCounter--;
        }
    }
}
