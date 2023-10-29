using CarRent.Areas.Admin.ViewModels.Category;
using CarRent.Areas.Admin.ViewModels.Product;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Database.Models;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private DataContext _dataContext;
  
        private readonly ILogger<ProductController> _logger;
        private readonly IFileService _fileService;
        public ProductController(DataContext dataContext,IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        //[HttpGet("~/")]
        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> List()
        {
          
            var products = await _dataContext.Cars
                .Select(p => new ProductListViewModel(
                    p.Id,
                    p.Brand,
                    p.Model,
                    p.Year,
                    p.FuelType,
                    p.SeatCount,
                    p.GearBox,
                    p.WeeklyPrice,
                    p.DailyPrice,
                    _fileService.GetFileUrl(p.PhoteInFileSystem, UploadDirectory.Product))).ToListAsync();


            return View(products);
        }
        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add()
        {
            var model = new ProductAddViewModel
            {
                Categories = await _dataContext.Categories
                    .Select(c => new CategoryListViewModel(c.Id, c.Name))
                    .ToListAsync(),
            };
            return View(model);
        }

        [HttpPost("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add(ProductAddViewModel product)
        {
            try
            {
                if (product == null) { return View(new ProductAddViewModel()); }
                var imageNameInSystem = await _fileService.UploadAsync(product!.Image, UploadDirectory.Product);
                await AddProduct(product.Image!.FileName, imageNameInSystem);
                return RedirectToRoute("admin-product-list");

                async Task AddProduct(string imageName, string imageNameInSystem)
                {
                    var newProduct = new Car
                    {
                        Brand = product.Brand,
                        Model = product.Model,
                        Year = product.Year,
                        FuelType = product.FuelType,
                        SeatCount = product.SeatCount,
                        GearBox = product.GearBox,
                        WeeklyPrice = product.WeeklyPrice,
                        DailyPrice = product.DailyPrice,
                        CategoryId = product.CategoryIds,
                        CreatedAt = DateTime.Now,
                        PhoteImageName = imageName,
                        PhoteInFileSystem = imageNameInSystem,
                    };

                      await _dataContext.AddAsync(newProduct);
                      await _dataContext.SaveChangesAsync();
                   
                }

               


            }
            catch (Exception ex)
            {
                return RedirectToRoute("admin-product-add");
            }


        }

        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _dataContext.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return NotFound();

            var model = new ProductAddViewModel
            {
                Id = product.Id,
                Brand = product.Brand,
                Model = product.Model,
                Year = product.Year,
                FuelType = product.FuelType,
                SeatCount = product.SeatCount,
                GearBox = product.GearBox,
                WeeklyPrice = product.WeeklyPrice,
                DailyPrice = product.DailyPrice,
                Categories= await _dataContext.Categories
                    .Select(c => new CategoryListViewModel(c.Id, c.Name))
                    .ToListAsync(),
                ImageUrl = _fileService.GetFileUrl(product.PhoteInFileSystem, UploadDirectory.Product)
            };

            return View(model);

            
        }

        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductAddViewModel newCar)
        {
            var car = await _dataContext.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (car == null) return NotFound();

            if (newCar.Image != null)
            {
                await _fileService.DeleteAsync(car.PhoteInFileSystem, UploadDirectory.Product);
                var imageFileNameInSystem = await _fileService.UploadAsync(newCar.Image, UploadDirectory.Product);
                await UpdateCarAsync(newCar.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateCarAsync(car.PhoteImageName, car.PhoteInFileSystem);
            }



            return RedirectToRoute("admin-product-list");

            
            async Task UpdateCarAsync(string imageName, string imageNameInFileSystem)
            {

                car.Brand = newCar.Brand;
                car.Model = newCar.Model;
                car.Year = newCar.Year;
                car.FuelType = newCar.FuelType;
                car.SeatCount = newCar.SeatCount;
                car.GearBox = newCar.GearBox;
                car.WeeklyPrice = newCar.WeeklyPrice;
                car.DailyPrice = newCar.DailyPrice;
                car.CreatedAt = DateTime.Now;
                car.PhoteImageName = imageName;
                car.PhoteInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
            

        }


        [HttpPost("delete/{id}", Name = "admin-product-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _dataContext.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (car == null) return NotFound();
            await _fileService.DeleteAsync(car.PhoteInFileSystem, UploadDirectory.Product);
            _dataContext.Cars.Remove(car);




            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return RedirectToRoute("admin-product-list");
            }


            return RedirectToRoute("admin-product-list");
        }
    }
}
