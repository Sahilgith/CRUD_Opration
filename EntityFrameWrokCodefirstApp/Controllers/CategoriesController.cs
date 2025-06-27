using System.Collections.Frozen;
using EntityFrameWrokCodefirstApp.Data;
using EntityFrameWrokCodefirstApp.DTO;
using EntityFrameWrokCodefirstApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameWrokCodefirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context) { 
          _context = context;
        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetAll() {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .Select(c => new CategoryReadDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Products = c.Products.Select(p => new ProductDto
                    {
                        id = p.Id,
                        Name = p.Name,  
                        Description = p.Description,
                        Price = p.Price,    
                        CategoryName = c.Name
                        
                    }).ToList()

                }).ToListAsync();

            return Ok(categories);
        }

        [HttpGet]
        [Route("GetCategory")]
        public async Task<IActionResult> GetById(int id) {

            var category = await _context.Categories
                .Include(c => c.Products)   
                .Where(c => c.Id == id)
                .Select(c => new CategoryReadDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Products = c.Products.Select(p => new ProductDto
                    {
                        id = p.Id,
                        Name = p.Name,  
                        Description = p.Description,    
                        Price = p.Price,    
                        CategoryName = c.Name   
                    }).ToList()

                }).FirstOrDefaultAsync();   

            return category == null ? NotFound() : Ok(category);    
        }

        [HttpPost]
        [Route("AddCategory")]

        public async Task<IActionResult> Create(CategoryCreateDto dto) {
            var category = new Category { Name = dto.Name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new {category.Id, category.Name} );   
        }

        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> Update(int id, CategoryCreateDto dto) { 
           var category = await _context .Categories.FindAsync(id); 
            if(category == null) return NotFound();

            category.Name = dto.Name;
            await _context.SaveChangesAsync();  
            return Ok("Category Updated");    
        }

        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<IActionResult> Delete(int id) {
            var category = await _context.Categories.FindAsync(id);
            if(category == null) return NotFound();

            category.IsDeleted= true;   
            await _context.SaveChangesAsync();
            return Ok("Category Deleted");
        }

    }
}
