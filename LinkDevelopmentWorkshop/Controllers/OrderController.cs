using LinkDevelopmentWorkshop.Application.Constants;
using LinkDevelopmentWorkshop.Application.Orders.Commands;
using LinkDevelopmentWorkshop.Application.Orders.Commands.CreateOrder;
using LinkDevelopmentWorkshop.Application.Orders.Queries.GetOrders;
using LinkDevelopmentWorkshop.ModelDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkDevelopmentWorkshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPost]
        [Route("AddOrder")]
        [Authorize(Policy = AuthorizationPolicies.CustomerUser)]
        public async Task<List<int>> AddOrder([FromBody] AddOrderRequestDto order)
        {

            var command = new CreateOrderCommand
            {
                UserID = order.UserID,
                OrderProducts = order.OrderProducts.ConvertAll(orderOrderProduct => new OrderRequest
                {
                    ProductID = orderOrderProduct.ProductID,
                    Quantity = orderOrderProduct.Quantity
                })
            };
            var response = _mediator.Send(command).Result;
            return response.OrderIDs;

        }

        [HttpGet]
        [Route("GetAllOrders")]
        [Authorize(Policy = AuthorizationPolicies.CustomerUser)]
        public async Task<GetOrdersResponseDto> GetAllOrders()
        {

            var query = new GetOrdersQuery();

            var response = _mediator.Send(query).Result;

            var ResponseOrders = Map(response.Orders);
            return new GetOrdersResponseDto { Orders = ResponseOrders };
        }

        private List<OrderDto> Map(ICollection<OrderResponse> orders)
        {
            return orders.Select(order => new OrderDto
            {
                UserID = order.UserID,
                ProductID = order.ProductID,
                Quantity = order.Quantity,
                FinalPrice = order.FinalPrice
            }).ToList();
        }
    }
}
