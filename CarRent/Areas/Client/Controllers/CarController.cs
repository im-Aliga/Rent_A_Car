using CarRent.Areas.Client.ViewModels;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Client.Controllers
{
    [Area("client")]
    [Route("car")]
    public class CarController : Controller
    {

        private DataContext _dataContext;
        private readonly IFileService _fileService;

        public CarController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        [HttpGet("list/{id}", Name = "client-car-list")]
        public async Task<IActionResult> List(int id)
        {
            var carslist = await _dataContext.Cars.Include(c=>c.Category).Where(c=>c.Category.OrderId==id).Select(c => new CarCateViewModel(c.Brand, c.Model, c.Year, c.FuelType
                , c.SeatCount, c.GearBox, c.WeeklyPrice, c.DailyPrice, c.CreatedAt, _fileService.GetFileUrl(c.PhoteInFileSystem, UploadDirectory.Product))).ToListAsync() ;
                



            return View(carslist);
        }
    }
}
