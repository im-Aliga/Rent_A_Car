using CarRent.Database.Models;
using CarRent.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarRent.Areas.Admin.ViewModels.Category;
using Microsoft.EntityFrameworkCore;
using CarRent.Services.Abstracts;
using CarRent.Contracts.File;
using CarRent.Areas.Client.ViewComponents;
using CarRent.Areas.Admin.ViewModels.Brand;

namespace CarRent.Areas.Admin.Controllers
{

    [Authorize]
    [Area("admin")]
    [Route("admin/category")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public CategoryController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-cate-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dbContext.Categories.Select(c => new CategoryListViewModel(c.Id, c.Name,_fileService.GetFileUrl(c.PhoteInFileSystem, UploadDirectory.Category))).ToListAsync();
            return View(model);
        }

        [HttpGet("add", Name = "admin-cate-add")]
        public async Task<IActionResult> Add()
        {
            return View(new CategoryAddViewModel());
        }

        [HttpPost("add", Name = "admin-cate-add")]
        public async Task<IActionResult> Add(CategoryAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Category);
            await AddCategory(model.Image!.FileName, imageNameInSystem);
            return RedirectToRoute("admin-cate-list");


            async Task AddCategory(string imageName, string imageNameInSystem)
            {
                var newBrand = new Category
                {
                    Name = model.Name,
                    OrderId = model.OrderId,
                    CreatedAt = DateTime.Now,
                    PhoteImageName = imageName,
                    PhoteInFileSystem = imageNameInSystem,
                };

                await _dbContext.AddAsync(newBrand);
                await _dbContext.SaveChangesAsync();

            }
            

        }

        [HttpGet("update/{id}", Name = "admin-cate-update")]
        public async Task<IActionResult> Update(int id)
        {
            var cate = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (cate == null) return NotFound();
            var model = new CategoryAddViewModel
            {
                Id = cate.Id,
                Name = cate.Name,
                OrderId= cate.OrderId,
                ImageUrl = _fileService.GetFileUrl(cate.PhoteInFileSystem, UploadDirectory.Category)
            };


            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-cate-update")]
        public async Task<IActionResult> Update(int id, [FromForm] CategoryAddViewModel newCate)
        {
            var cate = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (cate == null) return NotFound();

            if (newCate.Image != null)
            {
                await _fileService.DeleteAsync(cate.PhoteInFileSystem, UploadDirectory.Brand);
                var imageFileNameInSystem = await _fileService.UploadAsync(newCate.Image, UploadDirectory.Category);
                await UpdateBrandAsync(newCate.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateBrandAsync(cate.PhoteImageName, cate.PhoteInFileSystem);
            }



            return RedirectToRoute("admin-cate-list");


            async Task UpdateBrandAsync(string imageName, string imageNameInFileSystem)
            {

                cate.Name = newCate.Name;
                cate.OrderId = newCate.OrderId;
                cate.PhoteImageName = imageName;
                cate.PhoteInFileSystem = imageNameInFileSystem;
                await _dbContext.SaveChangesAsync();
            }

           



          
        }

        [HttpPost("delete/{id}", Name = "admin-cate-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(category.PhoteInFileSystem, UploadDirectory.Category);
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return RedirectToRoute("admin-cate-list");
        }
    }
}
