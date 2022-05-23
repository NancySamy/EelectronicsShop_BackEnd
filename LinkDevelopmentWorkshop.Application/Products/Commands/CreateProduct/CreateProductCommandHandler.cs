using LinkDevelopmentWorkshop.Domain.Entities;
using LinkDevelopmentWorkshop.Repo.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LinkDevelopmentWorkshop.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<CreateProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                var Response = new CreateProductResponse();
                var ProductEntity = Map(request);
                _unitOfWork.Product.Create(ProductEntity!);
                _unitOfWork.Save();
                Response.ProductID = ProductEntity.ProductId;
                return Response;
            }
        }

        private Product Map(CreateProductCommand request)
        {
            var result = new Product
            {
                NameAr = request.NameAr,
                NameEn = request.NameEn,
                DescAr = request.DescAr,
                DescEn = request.DescEn,
                Price = request.Price,
                CategoryId = request.CategoryId,
                DiscountPercentage = request.DiscountPercentage,
                Quantity = request.Quantity
            };
            return result;
        }
    }
}
