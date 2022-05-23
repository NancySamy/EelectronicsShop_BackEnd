using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand:IRequest<CreateOrderCommandResponse>
    {
        public int UserID { get; set; }
        public List<OrderRequest> OrderProducts { get; set; }
    }
    public class OrderRequest
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
