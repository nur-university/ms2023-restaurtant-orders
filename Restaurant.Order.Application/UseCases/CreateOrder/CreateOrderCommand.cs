using MediatR;
using Restaurant.Order.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Application.UseCases.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        private CreateOrderCommand() { }

        public CreateOrderCommand(List<OrderLineDto> orderLines)
        {
            OrderLines = orderLines;
        }

        public List<OrderLineDto> OrderLines { get; set; }
        public string ClientName { get; set; }

    }
}
