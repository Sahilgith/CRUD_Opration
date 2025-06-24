namespace EntityFrameWrokCodefirstApp.DTO
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public DateTime Orderdate  { get; set; }    
        public string UserName { get; set; }    
         public List<OrderItemDto> Items {  get; set; } 
    }


    public class OrderItemDto
    {
        public string ProductName { get; set; } 
        public int Quantity { get; set; }   

    }
}
