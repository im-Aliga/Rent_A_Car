using CarRent.Areas.Admin.ViewModels.Category;
using CarRent.Areas.Client.ViewModels;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/contact")]
    public class ContactController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public ContactController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        [HttpGet("list", Name = "admin-contact-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dbContext.Contacts.Select(c => new ContactViewModel(c.EmailAdress, c.PhoneNumber, c.Name, c.Surname)).ToListAsync();
            return View(model);
        }

    }
}
