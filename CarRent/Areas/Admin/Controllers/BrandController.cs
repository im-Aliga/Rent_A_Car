using CarRent.Areas.Admin.ViewModels.Brand;
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
    [Route("admin/brand")]
    public class BrandController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public BrandController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("list", Name = "admin-brand-list")]
        public async Task<IActionResult> List()
        {
            var categories = await _dataContext.Categories.ToListAsync();
            var brands = await _dataContext.Brands
                .Select(p => new BrandListViewModel(
                    p.Id,
                    p.Name,
                    _fileService.GetFileUrl(p.PhoteInFileSystem, UploadDirectory.Brand))).ToListAsync();


            return View(brands);
        }
        [HttpGet("add", Name = "admin-brand-add")]
        public async Task<IActionResult> Add()
        {

            return View();
        }
        [HttpPost("add", Name = "admin-brand-add")]
        public async Task<IActionResult> Add(BrandAddViewModel brand)
        {
            try
            {
                if (brand == null) { return View(new BrandAddViewModel()); }
                var imageNameInSystem = await _fileService.UploadAsync(brand!.Image, UploadDirectory.Brand);
                await AddProduct(brand.Image!.FileName, imageNameInSystem);
                return RedirectToRoute("admin-brand-list");

                async Task AddProduct(string imageName, string imageNameInSystem)
                {
                    var newBrand = new Brand
                    {
                        Name = brand.Name,
                        PhoteImageName = imageName,
                        PhoteInFileSystem = imageNameInSystem,
                    };

                    await _dataContext.AddAsync(newBrand);
                    await _dataContext.SaveChangesAsync();

                }




            }
            catch (Exception ex)
            {
                return RedirectToRoute("admin-brand-list");
            }


        }

        [HttpGet("update/{id}", Name = "admin-brand-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var brand = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if (brand is null) return NotFound();




            var model = new BrandAddViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                ImageUrl = _fileService.GetFileUrl(brand.PhoteInFileSystem, UploadDirectory.Brand)
            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-brand-update")]
        public async Task<IActionResult> Update(int id, [FromForm] BrandAddViewModel newBrand)
        {
            var brands = await _dataContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (brands == null) return NotFound();

            if (newBrand.Image != null)
            {
                await _fileService.DeleteAsync(brands.PhoteInFileSystem, UploadDirectory.Brand);
                var imageFileNameInSystem = await _fileService.UploadAsync(newBrand.Image, UploadDirectory.Brand);
                await UpdateBrandAsync(newBrand.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateBrandAsync(brands.PhoteImageName, brands.PhoteInFileSystem);
            }



            return RedirectToRoute("admin-brand-list");


            async Task UpdateBrandAsync(string imageName, string imageNameInFileSystem)
            {

                brands.Name = newBrand.Name;
                brands.PhoteImageName = imageName;
                brands.PhoteInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }



        }
        [HttpPost("delete/{id}", Name = "admin-brand-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var brand = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if (brand is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(brand.PhoteInFileSystem, UploadDirectory.Brand);

            _dataContext.Brands.Remove(brand);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-brand-list");
        }

    }
}
