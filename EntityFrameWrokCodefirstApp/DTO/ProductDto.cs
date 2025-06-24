namespace EntityFrameWrokCodefirstApp.DTO
{
    public class ProductDto
    {
        public int id {  get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string CategoryName{  get; set; }    //This comes from navigation




    }
}
