using CarRent.Areas.Client.ViewModels;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "NavbarCategory")]
    public class NavbarCategory:ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public NavbarCategory(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
                await _dataContext.Categories.OrderBy(c => c.OrderId)
                .Select(c => new CateCarViewModel(c.Id,c.OrderId, c.Name,_fileService.GetFileUrl(c.PhoteInFileSystem, UploadDirectory.Category))).ToListAsync();


            return View(model);
        }
    }
}
