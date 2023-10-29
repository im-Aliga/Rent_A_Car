using CarRent.Areas.Admin.ViewModels.About;
using CarRent.Areas.Client.ViewModels;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "Brand")]

    public class Brand : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;


        public Brand(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model =
               await _dataContext.Brands.Select(s => new BrandViewModel(
                   s.Id,
                   s.Name,
                   _fileService.GetFileUrl(s.PhoteInFileSystem, UploadDirectory.Brand))).ToListAsync();

            return View(model);
        }
    }
}
