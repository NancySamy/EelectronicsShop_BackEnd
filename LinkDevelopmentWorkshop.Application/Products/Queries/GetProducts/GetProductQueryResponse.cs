using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Products.Queries.GetProducts
{
    public class GetProductQueryResponse
    {
        public ICollection<ProductResponse> Products { get; set; }
    }
        public class ProductResponse
        {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescAr { get; set; }
        public string DescEn { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int Quantity { get; set; }
    }
}
