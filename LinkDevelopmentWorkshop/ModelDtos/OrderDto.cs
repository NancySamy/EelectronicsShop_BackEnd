namespace LinkDevelopmentWorkshop.ModelDtos
{
    public class OrderDto
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
