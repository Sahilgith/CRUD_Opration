namespace EntityFrameWrokCodefirstApp.DTO
{
    public class OrderCreateDto
    {
        //public int Id { get; set; } 
        public int UserId {  get; set; }    
        public DateTime Orderdate { get; set; }
        public List<OrderItemCreateDto> Items { get; set; } 
    }

    public class OrderItemCreateDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
