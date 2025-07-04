using EntityFrameWrokCodefirstApp.Data;
using EntityFrameWrokCodefirstApp.DTO;
using EntityFrameWrokCodefirstApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWrokCodefirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        [Route("GetProducts")]

        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10) { 
        
            if(page <=0) page = 1;  
            if(pageSize <= 0) pageSize = 10;

            var products = await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryName = p.Category.Name
                }).ToListAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
             .Include(p => p.Category)
             .Where(p => p.Id == id) //ussi product ko leke aayega jiski id match karega
             .Select(p => new ProductDto
             {
                 id = p.Id,
                 Name = p.Name, 
                 Description = p.Description,
                 Price = p.Price,   
                 CategoryName= p.Category.Name  
             }).FirstOrDefaultAsync();
            
            return product == null? NotFound(): Ok(product);
        }

        [HttpPost]
        [Route("AddProduct")]

        public async Task<IActionResult> Create(CreateProductDto dto) {

            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid Category");
            }

            var product = new Product
            {
                Name = dto.Name,    
                Description = dto.Description,  
                Price = dto.Price,  
                CategoryId = dto.CategoryId,
            };

            _context.Products.Add(product);
            await  _context.SaveChangesAsync(); 
            return Ok(new {product.Id});
        }

        [HttpPut]
        [Route("UpdateProduct")]

        public async Task<IActionResult> Update(int id, CreateProductDto dto) {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category == null)
                return BadRequest("Invalid Category");

            product.Name = dto.Name;    
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return Ok("Prodcut updated Succesfully");
        }

        [HttpDelete]
        [Route("DeleteProduct")]

        public async Task<IActionResult> Delete(int id) { 
        var product = await _context.Products.FindAsync(id);

            if (product == null) 
                return NotFound();

            product.IsDeleted = true;    
            await _context.SaveChangesAsync();

            return Ok($"Product soft deleted");
        }

    }
}
