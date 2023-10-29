using CarRent.Areas.Client.ViewModels;
using CarRent.Contracts.File;
using CarRent.Database;
using CarRent.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Client.Controllers
{
    [Area("client")]
    [Route("contact")]
    public class ContactController : Controller
    {

        private DataContext _datacontext;

        public ContactController(DataContext datacontext)
        {
            _datacontext = datacontext;
        }
        [HttpGet("list", Name = "client-contact-list")]
        public async Task<IActionResult> List()
        {
           
            return View();
        }

        [HttpPost("list", Name = "client-contact-list")]
        public async Task<IActionResult> List([FromForm] ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var model = new Contact
            {
                Name = contactViewModel.Name,
                Surname = contactViewModel.Surname,
                EmailAdress = contactViewModel.EmailAdress,
                PhoneNumber = contactViewModel.PhoneNumber,


            };
            await _datacontext.AddAsync(model);
            await _datacontext.SaveChangesAsync();

            return RedirectToRoute("client-home-index");
        }
    }
}
