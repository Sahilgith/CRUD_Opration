using EntityFrameWrokCodefirstApp.Common;

namespace EntityFrameWrokCodefirstApp.Models
{
    public class Category :BaseEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        //navigation
        public List<Product> Products { get; set; } = new List<Product>();  

    }
}
