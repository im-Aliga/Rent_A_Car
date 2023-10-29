using CarRent.Areas.Client.ViewModels;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Client.Controllers
{
    [Area("client")]
    [Route("detail")]
    public class DetailController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public DetailController(DataContext dbContext,IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("list/{id}", Name = "client-detail-list")]

        public async Task<IActionResult> List(int id)
        {
            var carslist = await _dbContext.Cars.Include(c=> c.Category).
                Where(c=>c.CategoryId==id).Select(c => new CarCateViewModel(c.Brand, c.Model, c.Year, c.FuelType
              , c.SeatCount, c.GearBox, c.WeeklyPrice, c.DailyPrice, c.CreatedAt, _fileService.GetFileUrl(c.PhoteInFileSystem, UploadDirectory.Product))).ToListAsync();

         

            var model = new CarListViewModel
            {
                Cars = carslist,
            };

            return View(model);
        }
    }
}
