using LinkDevelopmentWorkshop.Domain.Entities;
using LinkDevelopmentWorkshop.Repo.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<GetProductQueryResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var response=new GetProductQueryResponse() {Products =new  List<ProductResponse>()};
            var Products = _unitOfWork.Product.FindAll(x => x.Category).Where(x=>x.Quantity>0).ToList();
            if (Products.Any())
            {
                response.Products = Map(Products);
            }
            return response;
        }

        private ICollection<ProductResponse> Map(List<Product> products)
        {
            return products.Select(product => new ProductResponse
            {
                NameAr = product.NameAr,
                NameEn = product.NameEn,
                DescAr = product.DescAr,
                DescEn = product.DescEn,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Id = product.ProductId,
                DiscountPercentage = product.DiscountPercentage,
                Quantity = product.Quantity
            }).ToList();
        }
    }
}
