using LinkDevelopmentWorkshop.Application.Constants;
using LinkDevelopmentWorkshop.Application.Products.Commands.CreateProduct;
using LinkDevelopmentWorkshop.Application.Products.Queries.GetCategories;
using LinkDevelopmentWorkshop.Application.Products.Queries.GetProducts;
using LinkDevelopmentWorkshop.ModelDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkDevelopmentWorkshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpPost]
        [Route("AddProduct")]
        [Authorize(Policy = AuthorizationPolicies.ProcessAdministrators)]

        public async Task<int> AddProduct([FromBody] AddProductRequestDto product)
        {

            var command = new CreateProductCommand
            {
                NameAr = product.NameAr,
                NameEn = product.NameEn,
                DescAr = product.DescAr,
                DescEn = product.DescEn,
                Price = product.Price,
                CategoryId = (int)product.CategoryId,
                DiscountPercentage = product.DiscountPercentage,
                Quantity = product.Quantity
            };
            var response = _mediator.Send(command).Result;

            return response.ProductID;

        }

        [HttpGet]
        [Route("GetAllProducts")]
        [Authorize(Policy = AuthorizationPolicies.ProcessAdministratorsOrCustomerUser)]
        public async Task<GetProductsResponseDto> GetAllProducts()
        {

            var query = new GetProductsQuery();
           
            var response = _mediator.Send(query).Result;

            var ResponseProducts = Map(response.Products);
            return new GetProductsResponseDto { Products= ResponseProducts };
        }

        [HttpGet]
        [Route("GetAllCategories")]
        [Authorize(Policy = AuthorizationPolicies.ProcessAdministrators)]
        public async Task<GetCategoriesResponseDto> GetAllCategories()
        {

            var query = new GetCategoriesQuery();

            var response = _mediator.Send(query).Result;

            var ResponseCategories = Map(response.Categories);
            return new GetCategoriesResponseDto { Categories = ResponseCategories };
        }

        private List<CategoryDto> Map(ICollection<CategoryResponse> categories)
        {
            return categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                NameAr = category.NameAr,
                NameEn = category.NameEn,
                Code = category.Code,
                DescAr = category.DescAr,
                DescEn = category.DescEn
            }).ToList();
        }

        private List<ProductsDto> Map(ICollection<ProductResponse> products)
        {
            return products.Select(product => new ProductsDto
            {
                Id = product.Id,
                NameAr = product.NameAr,
                NameEn = product.NameEn,
                DescAr = product.DescAr,
                DescEn = product.DescEn,
                Price = product.Price,
                CategoryId = product.CategoryId,
                DiscountPercentage = product.DiscountPercentage,
                Quantity = product.Quantity
            }).ToList();
        }
    }
}
