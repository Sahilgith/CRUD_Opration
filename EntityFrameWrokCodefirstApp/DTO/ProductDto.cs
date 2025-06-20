namespace EntityFrameWrokCodefirstApp.DTO
{
    public class ProductDto
    {
        public int id {  get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }

        public string CatagoryName{  get; set; }    //This comes from navigation




    }
}
