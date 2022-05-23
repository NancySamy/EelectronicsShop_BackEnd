using LinkDevelopmentWorkshop.Domain.Entities;
using LinkDevelopmentWorkshop.Repo.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, GetOrdersQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<GetOrdersQueryResponse> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var response = new GetOrdersQueryResponse() { Orders = new List<OrderResponse>() };
            var orders = _unitOfWork.Order.FindAll().ToList();
            if (orders.Any())
            {
                response.Orders = Map(orders);
            }
            return response;
        }

        private List<OrderResponse> Map(List<Order> orders)
        {
            return orders.ConvertAll(order => new OrderResponse
            {
                UserID = order.UserID,
                ProductID = order.ProductID,
                Quantity = order.Quantity,
                FinalPrice = order.FinalPrice
            });
        }
    }
}
