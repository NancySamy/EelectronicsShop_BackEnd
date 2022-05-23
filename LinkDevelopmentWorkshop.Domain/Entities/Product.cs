using LinkDevelopmentWorkshop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescAr { get; set; }
        public string DescEn { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
    }
}
