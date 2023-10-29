using CarRent.Areas.Admin.ViewModels.Category;
using CarRent.Areas.Admin.ViewModels.Product;
using CarRent.Areas.Client.ViewModels;
using CarRent.Database;
using CarRent.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Client.Controllers
{
    [Area("client")]
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly DataContext _dbContext;
        private static int count;

        public OrderController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        [HttpGet("add", Name = "client-order-add")]
        public async Task<IActionResult> Add()
        {
            var model = new OrderCarListViewModel
            {
                Cars= await _dbContext.Cars.Select(c=> new OrderCarViewModel(c.Id,c.Brand,c.Model,c.Year)).ToListAsync(),
            };
         
            return View(model);
        }

        [HttpPost("add", Name = "client-order-add")]
        public async Task<IActionResult> Add([FromForm] OrderCarListViewModel product)
        {
            if (product is null)
            {
                var model = await _dbContext.Cars
                    .Select(c => new OrderCarViewModel(c.Id, c.Brand, c.Model, c.Year))
                    .ToListAsync();

                return View(model);
            }
            count++;


       

            var order = new Order
            {
                OrderCarId = product.CarIds.FirstOrDefault(),
                CreatedAt=DateTime.Now,
                Count=count
            };

            await _dbContext.Order.AddAsync(order);
            await _dbContext.SaveChangesAsync();


            var modelforid = await _dbContext.Order.OrderBy(o=>o.CreatedAt).LastOrDefaultAsync();
    
            return RedirectToRoute("client-order-getdate",new { Id= modelforid.Id});


        }


        [HttpGet("getdate/{id}", Name = "client-order-getdate")]
        public async Task<IActionResult> GetDate(int id)
        {
            var neworder = await _dbContext.Order.FirstOrDefaultAsync(c => c.Id == id);
            var model = new OrderDateViewModel
            {
                Pickup=DateTime.Now,
                Drop=DateTime.Now,
                OrderId = neworder.Id

            };


            return View(model);
        }

        [HttpPost("getdateasync", Name = "client-order-getdateasync")]
        public async Task<IActionResult> GetDateAsync(OrderDateViewModel orderdates)
        {
            if (orderdates is null)
            {
            
                return View(new OrderDateViewModel(orderdates.OrderId));
            }

            var order = await _dbContext.Order.FirstOrDefaultAsync(o => o.Id == orderdates.OrderId);


            order!.PickupDate = orderdates.Pickup;
            order.DropDate = orderdates.Drop;

            await _dbContext.SaveChangesAsync();


            return RedirectToRoute("client-order-getdetails", new { Id=order.Id });


        }

        [HttpGet("getdetails/{id}", Name = "client-order-getdetails")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var neworder = await _dbContext.Order.FirstOrDefaultAsync(c => c.Id == id);
            var model = new OrderViewModel
            {
                OrderId = neworder.Id
            };


            return View(model);
        }

        [HttpPost("getdetailsasync", Name = "client-order-getdetailsasync")]
        public async Task<IActionResult> GetDetailsAsync(OrderViewModel orderdates)
        {
            if (orderdates is null)
            {

                return View(new OrderViewModel(orderdates.OrderId));
            }

            var order = await _dbContext.Order.FirstOrDefaultAsync(o => o.Id == orderdates.OrderId);


            order!.Name = orderdates.Name;
            order.Surname = orderdates.Surname;
            order.Email = orderdates.Email;
            order.PhoneNumber = orderdates.PhoneNumber;

            await _dbContext.SaveChangesAsync();


            return RedirectToRoute("client-home-index");


        }
    }
}
