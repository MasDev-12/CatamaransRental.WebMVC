using CatamaransRental.Domain;
using CatamaransRental.Domain.ViewModel;
using CatamaransRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatamaransRental.WebMVC.Controllers
{
    public class RentalController:Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService=rentalService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRental(RentalViewModel rentalViewModel, string userName, string model)
        {
            rentalViewModel.UserViewModel.Name = User.Identity.Name;
            rentalViewModel.CatamaranViewModel.Model = model;
            if (ModelState.IsValid)
            {
                var response = await _rentalService.CreateRental(rentalViewModel);
                if (response.Data ==false)
                {
                    ViewBag.ShowSuccessMessage = false;
                    return View(rentalViewModel);
                }
                ViewBag.ShowSuccessMessage = true;
                return View(rentalViewModel);
            }
            return RedirectToAction("GetAll", "Catamaran");
        }

        [HttpGet]
        public IActionResult CreateRental(string model)
        {
            var userName = User.Identity.Name;
            ViewData["Model"] = model;
            return View(new RentalViewModel { CatamaranViewModel = new CatamaranViewModel { Model = model }, UserViewModel = new UserViewModel { Name = userName } });
        }
    }
}
