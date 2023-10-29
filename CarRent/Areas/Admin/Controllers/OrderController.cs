using CarRent.Areas.Admin.ViewModels.Brand;
using CarRent.Areas.Admin.ViewModels.Order;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/order")]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public OrderController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("list", Name = "admin-order-list")]
        public async Task<IActionResult> List()
        {
            var orders = await _dataContext.Order.OrderBy(o=>o.CreatedAt).Include(o => o.OrderCar).Select(o => new OrderViewModel(o.Id, o.PickupDate,
                o.DropDate, o.Name, o.Surname, o.Email, o.PhoneNumber, o.OrderCar.Brand, o.OrderCar.Model, o.OrderCar.Year, o.CreatedAt)).ToListAsync();


            return View(orders);
        }
    }
}
