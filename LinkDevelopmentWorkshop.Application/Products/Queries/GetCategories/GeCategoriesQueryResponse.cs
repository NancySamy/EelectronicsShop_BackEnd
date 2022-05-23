using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Products.Queries.GetCategories
{
    public class GeCategoriesQueryResponse
    {
        public ICollection<CategoryResponse> Categories { get; set; }
    }
        public class CategoryResponse
        {
        public int Id { get; set; }
        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string DescAr { get; set; } = null!;
        public string DescEn { get; set; } = null!;
    }
}
