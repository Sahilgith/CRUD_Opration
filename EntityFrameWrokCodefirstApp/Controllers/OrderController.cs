﻿using EntityFrameWrokCodefirstApp.Data;
using EntityFrameWrokCodefirstApp.DTO;
using EntityFrameWrokCodefirstApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EntityFrameWrokCodefirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetMAx")]

        public async Task<ActionResult<IEnumerable<ProductAndOrder>>> getmax() {

            var result = await _context.OrderItems
                .GroupBy(x => x.ProductId)
                .Select(g => new ProductAndOrder
                {
                    productname = g.First().Product.Name,
                    ordercount = g.Count()
                }).OrderByDescending(x => x.ordercount)
                .ToListAsync();
            return result;
        }


        
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var orders = await _context.Orders
                .Where(o => !o.IsDeleted) 
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderReadDto
                {
                    Id = o.Id,
                    Orderdate = o.OrderDate,
                    UserName = o.User.Name,
                    Items = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity
                    }).ToList()
                })
                .ToListAsync();

            return Ok(orders);
        }



        [HttpGet]
        [Route("GetOrderById")]

        public async Task<IActionResult> GetById(int id) {
            var order = await _context.Orders
                  .Include(o => o.User)
                  .Include(o => o.OrderItems)
                  .ThenInclude(oi => oi.Product)
                  .Where(o => o.Id == id)
                  .Select(o => new OrderReadDto
                  {
                      Id = o.Id,
                      Orderdate = o.OrderDate,
                      UserName = o.User.Name,
                      Items = o.OrderItems.Select(oi => new OrderItemDto
                      {
                          ProductName = oi.Product.Name,
                          Quantity = oi.Quantity

                      }).ToList()
                  }).FirstOrDefaultAsync(); 

            return order == null ? NotFound() : Ok(order);  
        }

        [HttpPost]
        [Route("AddOrder")]

        public async Task<IActionResult> CreateOrder(OrderCreateDto dto) {
            var order = new Order
            {
                OrderDate = dto.Orderdate,
                UserId = dto.UserId,
                OrderItems = dto.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList(),
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(new { order.Id });
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> Update(int id, OrderCreateDto dto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            order.OrderDate = dto.Orderdate;
            order.UserId = dto.UserId;

            // Remove old items
            _context.OrderItems.RemoveRange(order.OrderItems);

            // Add updated items
            order.OrderItems = dto.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList();

            await _context.SaveChangesAsync();
            return Ok("Order updated successfully");
        }


        [HttpDelete]
        [Route("DeleteOrder")]

            public async Task<IActionResult> Delete(int id)
            {
            var order = await _context.Orders
              .Include(o => o.OrderItems)
              .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) {
                return NotFound();
            }
            // soft deleted
            order.IsDeleted = true;

            foreach (var item in order.OrderItems) {
                item.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
            return Ok($"Order with id: {id}  Soft deleted");

         }


        

    }
}
