using CatamaransRental.Domain;
using CatamaransRental.Domain.ViewModel;
using CatamaransRental.Services.Implementions;
using CatamaransRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatamaransRental.WebMVC.Controllers
{
    public class TicketController:Controller
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService) 
        {
            _ticketService=ticketService;
        }
        public IActionResult GetAll()
        {
            IEnumerable<TicketViewModel> catamaranVIewModels = _ticketService.GetAllTickets().Data;
            return View(catamaranVIewModels);
        }

        public async Task<IActionResult> Detail(TicketViewModel ticketViewModel)
        {
            var catamaranViewModel = await _ticketService.GetTicketByDate(ticketViewModel.PurchaseDate);
            return View(catamaranViewModel.Data);
        }

     

        [HttpGet]
        public async Task<IActionResult> RegisterTicket(TicketViewModel ticketViewMode)
        {
            var response = _ticketService.RegisterTicket(ticketViewMode.PurchaseDate);
            if (response.Result.StatusCode==Domain.Enum.StatusCodeEnum.OK)
            {
                ViewBag.AlertType = "success";
                ViewBag.AlertMessage = response.Result.Description;
                return View("Notification");

            }
            if (response.Result.StatusCode ==Domain.Enum.StatusCodeEnum.NotFound)
            {
                ViewBag.AlertType = "NotFound";
                ViewBag.AlertMessage = response.Result.Description;
                return View("Notification");
            }
            if (response.Result.StatusCode ==Domain.Enum.StatusCodeEnum.InternalServerError)
            {
                ViewBag.AlertType = "Error";
                ViewBag.AlertMessage = response.Result.Description;
                return View("Notification");
            }
            return RedirectToAction("GetAll", "Ticket");
        }
    }
}
