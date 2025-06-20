namespace EntityFrameWrokCodefirstApp.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }  
        public string Description { get; set; }
        public string Price { get; set; }

        //Navigation Property
       public int? CatogoryId { get; set; } //foreign key
        public Category Category { get; set; } //Nvaigation




    }
}
