namespace EntityFrameWrokCodefirstApp.Models
{
    public class Category
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        //navigation

        public List<Product> Products { get; set; } = new();

    }
}
