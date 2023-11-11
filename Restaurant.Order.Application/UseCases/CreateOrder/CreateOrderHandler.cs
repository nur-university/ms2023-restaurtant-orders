using MediatR;
using Restaurant.Order.Domain.Repositories;
using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Application.UseCases.CreateOrder
{
    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Domain.Model.Orders.Order.Create(request.ClientName);

            foreach (var item in request.OrderLines)
            {
                order.AddItem(item.ItemId, item.ItemName, item.Quantity, item.UnitaryPrice);
            }

            order.Start();

            await _orderRepository.CreateAsync(order);

            await _unitOfWork.Commit();

            return order.Id;
        }
    }
}
