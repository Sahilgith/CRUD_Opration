using EntityFrameWrokCodefirstApp.Common;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWrokCodefirstApp.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; }  
        public string Description { get; set; }
        
        [Precision(18,2)]
        public decimal Price { get; set; }

        //Navigation Property
        public int CategoryId { get; set; } //foreign key
        public Category Category { get; set; } //Nvaigationx1

        public List<OrderItem> OrderItems { get; set; }  
    }
}
