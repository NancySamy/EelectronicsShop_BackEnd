using LinkDevelopmentWorkshop.Domain.Entities;
using LinkDevelopmentWorkshop.Repo.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Products.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GeCategoriesQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<GeCategoriesQueryResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var response=new GeCategoriesQueryResponse() { Categories = new  List<CategoryResponse>()};
            var categories = _unitOfWork.Category.FindAll().ToList();
            if (categories.Any())
            {
                response.Categories = Map(categories);
            }
            return response;
        }

        private  ICollection<CategoryResponse> Map(List<Category> categories)
        {
            return categories.Select(category => new CategoryResponse
            {
                Id = category.Id,
                NameAr = category.NameAr,
                NameEn = category.NameEn,
                Code = category.Code,
                DescAr = category.DescAr,
                DescEn = category.DescEn
            }).ToList();
        }
    }
}
