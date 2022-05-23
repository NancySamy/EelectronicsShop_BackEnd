using LinkDevelopmentWorkshop.Domain.Entities;
using LinkDevelopmentWorkshop.Repo.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                
                var Response = new CreateOrderCommandResponse();
                var finalOrdersAfterDiscount=new List<Order>();
                //0)Get Order Products IdList
               var idList= request.OrderProducts.Select(p => p.ProductID).ToList();
                //1)Get Order Products 
                var orderProducts=_unitOfWork.Product.FindByCondition(p=> idList.Contains(p.ProductId)).ToList();
                 
                //2)Check if there is two products or more in order under the Same Category
                var productsHaveConfigDiscount = new List<int>();
                foreach (var line in orderProducts.GroupBy(info => info.CategoryId)
                        .Select(group => new {
                            ProductID = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.ProductID))
                {
                    if (line.Count >= 2)
                    {
                        productsHaveConfigDiscount.Add(line.ProductID);
                    }
                }
                //3)Apply configDisCount
                var confiDiscountProducts=orderProducts.Where(x => productsHaveConfigDiscount.Contains(x.ProductId)).ToList();
                if (confiDiscountProducts.Any())
                {
                    var discountPercentage = _configuration.GetSection("DiscountPercentage").Get<int>();
                    foreach (var product in confiDiscountProducts)
                    {
                        var order = new Order();
                        order.UserID = request.UserID;
                        order.Quantity = request.OrderProducts.Find(x=>x.ProductID==product.ProductId).Quantity;
                        order.ProductID = product.ProductId;
                        order.FinalPrice = product.Price - ((product.Price * discountPercentage) / 100);
                        finalOrdersAfterDiscount.Add(order);
                    }
                }
                var otherProducts = orderProducts.Where(x => !productsHaveConfigDiscount.Contains(x.ProductId)).ToList();

                if (otherProducts.Any())
                {
                    foreach (var product in otherProducts)
                    {
                        var order = new Order();
                        //4)Apply Discount on Products that have static Discount Val
                        var staticDiscountPercentage =product.DiscountPercentage;
                        order.UserID = request.UserID;
                        order.Quantity = request.OrderProducts.Find(x => x.ProductID == product.ProductId).Quantity;
                        order.ProductID = product.ProductId;
                        order.FinalPrice = staticDiscountPercentage==0? product.Price:product.Price - ((product.Price * staticDiscountPercentage) / 100);
                        finalOrdersAfterDiscount.Add(order);
                    }
                }
                _unitOfWork.Order.CreateRange(finalOrdersAfterDiscount!);
                //5)Descrese product quantity after order
                foreach (var product in orderProducts)
                {
                    var productOrderQuantity = request.OrderProducts.Find(x => x.ProductID == product.ProductId).Quantity;
                    product.Quantity = product.Quantity - productOrderQuantity;

                }
                _unitOfWork.Product.UpdateRange(orderProducts);
                _unitOfWork.Save();

                Response.OrderIDs = finalOrdersAfterDiscount.Select(o=>o.ProductID).ToList();
                return Response;
            }
        }

    }
}
