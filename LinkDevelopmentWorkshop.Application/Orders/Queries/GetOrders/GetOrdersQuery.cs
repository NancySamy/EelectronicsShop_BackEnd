using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQuery:IRequest<GetOrdersQueryResponse>
    {
    }
}
