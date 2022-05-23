namespace LinkDevelopmentWorkshop.ModelDtos
{
    public class AddOrderRequestDto
    {
        public int UserID { get; set; }
        public List<OrderRequestDto> OrderProducts { get; set; }
    }
    public class OrderRequestDto
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
