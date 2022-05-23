using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Orders.Queries.GetOrders
{
  
    public class GetOrdersQueryResponse
    {
        public ICollection<OrderResponse> Orders { get; set; }
    }
    public class OrderResponse
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
