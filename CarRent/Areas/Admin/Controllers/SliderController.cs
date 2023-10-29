using CarRent.Areas.Admin.ViewModels.About;
using CarRent.Areas.Admin.ViewModels.Category;
using CarRent.Areas.Admin.ViewModels.Product;
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
    [Route("admin/slider")]
    public class SliderController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public SliderController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-slider-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dbContext.Sliders.Select(c => new SliderListViewModel(
                c.Id, 
                c.SmallHeader,
                c.Header,
                c.Tittle,
               _fileService.GetFileUrl(c.PhoteInFileSystem, UploadDirectory.Slider))).ToListAsync();
            return View(model);
        }

        [HttpGet("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add()
        {
            return View(new SliderAddViewModel());
        }

        [HttpPost("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add(SliderAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Slider);
            await AddSlider(model.Image!.FileName, imageNameInSystem);
            return RedirectToRoute("admin-slider-list");

            async Task AddSlider(string imageName, string imageNameInSystem)
            {
                var newProduct = new Slider
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



        [HttpGet("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> Update(int id)
        {
            var slider = await _dbContext.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null) return NotFound();

            var model = new SliderAddViewModel
            {
                Id = slider.Id,
                SmallHeader = slider.SmallHeader,
                Header = slider.Header,
                Tittle = slider.Tittle,
                ImageUrl = _fileService.GetFileUrl(slider.PhoteInFileSystem, UploadDirectory.Slider)
            };



            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> Update(int id,[FromForm] SliderAddViewModel newSlider)
        {
            if (!ModelState.IsValid)
            {
                return View(newSlider);
            }
            var slider = await _dbContext.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null) return NotFound();



            if (newSlider.Image != null)
            {
                await _fileService.DeleteAsync(slider.PhoteInFileSystem, UploadDirectory.Slider);
                var imageFileNameInSystem = await _fileService.UploadAsync(newSlider.Image, UploadDirectory.Slider);
                await UpdateSliderAsync(newSlider.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateSliderAsync(slider.PhoteImageName, slider.PhoteInFileSystem);
            }



            return RedirectToRoute("admin-slider-list");


            async Task UpdateSliderAsync(string imageName, string imageNameInFileSystem)
            {

                slider.SmallHeader = newSlider.SmallHeader;
                slider.Header = newSlider.Header;
                slider.Tittle = newSlider.Tittle;
                slider.PhoteImageName = imageName;
                slider.PhoteInFileSystem = imageNameInFileSystem;
                await _dbContext.SaveChangesAsync();
            }


           



           
        }

        [HttpPost("delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var slider = await _dbContext.Sliders.FirstOrDefaultAsync(c => c.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(slider.PhoteInFileSystem, UploadDirectory.Slider);
            _dbContext.Sliders.Remove(slider);
            


            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return RedirectToRoute("admin-slider-list");
            }
            return RedirectToRoute("admin-slider-list");



        }
    }
}
