using CarRent.Areas.Client.ViewModels;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {

        private DataContext _dataContext;
        private readonly IFileService _fileService;

        public HomeController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("~/")]
        [HttpGet("index", Name = "client-home-index")]
        public async  Task<IActionResult> Index()
        {
            var carslist = await _dataContext.Cars.Select(c => new CarCateViewModel(c.Brand, c.Model, c.Year, c.FuelType
                , c.SeatCount, c.GearBox, c.WeeklyPrice, c.DailyPrice, c.CreatedAt,_fileService.GetFileUrl(c.PhoteInFileSystem, UploadDirectory.Product)
                ,_dataContext.Categories.OrderBy(cat=>cat.OrderId)
                .Select(cat=> new CateCarViewModel(cat.OrderId,cat.Name)).ToList()))
                .ToListAsync();


            
            return View(carslist);
        }
    }
}
