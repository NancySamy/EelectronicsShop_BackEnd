namespace LinkDevelopmentWorkshop.ModelDtos
{
    public class ProductsDto
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescAr { get; set; }
        public string DescEn { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int Quantity { get; set; }
    }
}
