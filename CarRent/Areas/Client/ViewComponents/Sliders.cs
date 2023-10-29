using CarRent.Areas.Client.ViewModels;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarRent.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "Sliders")]
    public class Sliders : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public Sliders(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Sliders.Select(s => new SliderListViewMOdel(s.SmallHeader,
                s.Header, s.Tittle,_fileService.GetFileUrl(s.PhoteInFileSystem, UploadDirectory.Slider))).FirstOrDefaultAsync();

            return View(model);
        }
    }
}
