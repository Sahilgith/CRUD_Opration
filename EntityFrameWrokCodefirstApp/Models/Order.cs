namespace EntityFrameWrokCodefirstApp.Models
{
    public class Order
    {
        public int Id { get; set; } 
        public DateTime OrderDate { get; set; } 

        //Foreign key
        public int UserId { get; set; } 

        //Navigation Property
        public Users User {  get; set; }    
        public List<OrderItem> OrderItems { get; set; } 
    }
}
