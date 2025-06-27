using EntityFrameWrokCodefirstApp.Common;

namespace EntityFrameWrokCodefirstApp.Models
{
    public class  OrderItem : BaseEntity
    {
        public int Id { get; set; } 
         public int OrderId {  get; set; }   //foreign key to order, order and product ke bich link banata hai
        public int ProductId { get; set; }  //foregin key to prodcut
        //order <-> orderItem <--> Prodcuct --> thats why orderItem
        public int Quantity { get; set; }   

        //Navigation Property
        public Order Order { get; set; }
        public Product Product { get; set; }


    }
}
