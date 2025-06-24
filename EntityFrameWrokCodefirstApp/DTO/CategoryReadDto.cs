namespace EntityFrameWrokCodefirstApp.DTO
{
    public class CategoryReadDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
