using CarRent.Areas.Admin.ViewModels.About;
using CarRent.Areas.Client.ViewModels;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "AboutUs")]

    public class AboutUs : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public AboutUs(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
                await _dataContext.Abouts.Select(s=>new AboutViewModel(
                    s.Id,
                    s.SmallHeader,
                    s.Header,
                    s.Tittle,
                    _fileService.GetFileUrl(s.PhoteInFileSystem, UploadDirectory.About))).FirstOrDefaultAsync();


            return View(model);
        }
    }
}
