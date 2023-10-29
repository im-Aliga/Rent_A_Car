using CarRent.Areas.Client.ViewModels;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "Categorys")]

    public class Categorys : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;


        public Categorys(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model =
               await _dataContext.Categories.Select(s => new CateCarViewModel(
                   s.OrderId,
                   s.Name
                   )).ToListAsync();

            return View(model);
        }
    }
}
