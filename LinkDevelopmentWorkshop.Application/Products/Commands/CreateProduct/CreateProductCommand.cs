using MediatR;

namespace LinkDevelopmentWorkshop.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand:IRequest<CreateProductResponse>
    {
        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string DescAr { get; set; } = null!;
        public string DescEn { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int Quantity { get; set; }
    }
}
