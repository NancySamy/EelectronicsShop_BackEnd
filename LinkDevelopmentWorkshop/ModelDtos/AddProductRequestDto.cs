using LinkDevelopmentWorkshop.Domain.Enums;

namespace LinkDevelopmentWorkshop.ModelDtos
{
    /// <summary>
    /// Add product Request Dto
    /// </summary>
    public class AddProductRequestDto
    {
        /// <summary>
        /// Product Arabic Name
        /// </summary>
        public string NameAr { get; set; } = null!;
        /// <summary>
        /// Product English Name
        /// </summary>
        public string NameEn { get; set; } = null!;
        /// <summary>
        /// Product Arabic Description
        /// </summary>
        public string DescAr { get; set; } = null!;
        /// <summary>
        /// Product English Desc
        /// </summary>
        public string DescEn { get; set; } = null!;
        /// <summary>
        /// Product Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Product Category (TVs,Laptops,SoundSys
        /// </summary>
        public CategoryTypes CategoryId { get; set; }
        /// <summary>
        /// Product DiscountPercentage
        /// </summary>
        public decimal DiscountPercentage { get; set; }
        /// <summary>
        /// Product Avalibale Quantity
        /// </summary>
        public int Quantity { get; set; }
    }
}
