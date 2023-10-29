using CarRent.Areas.Admin.ViewModels.About;
using CarRent.Areas.Admin.ViewModels.Category;
using CarRent.Areas.Admin.ViewModels.Slider;
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
    [Route("admin/about")]
    public class AboutController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public AboutController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-about-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dbContext.Abouts.Select(c => new AboutViewModel(
                c.Id,
                c.SmallHeader,
                c.Header,
                c.Tittle,
                _fileService.GetFileUrl(c.PhoteInFileSystem, UploadDirectory.About)
                )).ToListAsync();
            return View(model);
        }

        [HttpGet("add", Name = "admin-about-add")]
        public async Task<IActionResult> Add()
        {
            return View(new AboutViewModel());
        }

        [HttpPost("add", Name = "admin-about-add")]
        public async Task<IActionResult> Add(AboutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Slider);
            await AddAbout(model.Image!.FileName, imageNameInSystem);
            return RedirectToRoute("admin-about-list");

            async Task AddAbout(string imageName, string imageNameInSystem)
            {
                var newProduct = new About
                {
                    SmallHeader = model.SmallHeader,
                    Header = model.Header,
                    Tittle = model.Tittle,
                    PhoteImageName = imageName,
                    PhoteInFileSystem = imageNameInSystem,
                };

                await _dbContext.AddAsync(newProduct);
                await _dbContext.SaveChangesAsync();

            }
            

        }


        [HttpGet("update/{id}", Name = "admin-about-update")]
        public async Task<IActionResult> Update(int id)
        {
            var about = await _dbContext.Abouts.FirstOrDefaultAsync(x => x.Id == id);
            if (about == null) return NotFound();
            var model = new AboutViewModel
            {
                Id = about.Id,
                SmallHeader = about.SmallHeader,
                Header = about.Header,
                Tittle = about.Tittle,
                ImageUrl = _fileService.GetFileUrl(about.PhoteInFileSystem, UploadDirectory.About)
            };



            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-about-update")]
        public async Task<IActionResult> Update(int id, [FromForm] AboutViewModel newAbout)
        {
            if (!ModelState.IsValid)
            {
                return View(newAbout);
            }
            var about = await _dbContext.Abouts.FirstOrDefaultAsync(x => x.Id == id);
            if (about == null) return NotFound();



            if (newAbout.Image != null)
            {
                await _fileService.DeleteAsync(about.PhoteInFileSystem, UploadDirectory.Slider);
                var imageFileNameInSystem = await _fileService.UploadAsync(newAbout.Image, UploadDirectory.About);
                await UpdateSliderAsync(newAbout.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateSliderAsync(about.PhoteImageName, about.PhoteInFileSystem);
            }



            return RedirectToRoute("admin-about-list");
            async Task UpdateSliderAsync(string imageName, string imageNameInFileSystem)
            {
                about.SmallHeader = newAbout.SmallHeader;
                about.Header = newAbout.Header;
                about.Tittle = newAbout.Tittle;
                about.PhoteImageName = imageName;
                about.PhoteInFileSystem = imageNameInFileSystem;
                await _dbContext.SaveChangesAsync();
            }

            
        }

        [HttpPost("delete/{id}", Name = "admin-about-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var about = await _dbContext.Abouts.FirstOrDefaultAsync(c => c.Id == id);
            if (about is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(about.PhoteInFileSystem, UploadDirectory.About);
            

            _dbContext.Abouts.Remove(about);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return RedirectToRoute("admin-about-list");
            }
            return RedirectToRoute("admin-about-list");

        }
    }
}
