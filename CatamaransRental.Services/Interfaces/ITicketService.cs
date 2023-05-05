using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.Response;
using CatamaransRental.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IBaseResponse<bool>> CreateTicket(RentalViewModel rentalViewModel);

        IBaseResponse<IEnumerable<TicketViewModel>> GetAllTickets();

        Task<IBaseResponse<TicketViewModel>> GetTicketByDate(DateTime purchaseDate);

        Task<IBaseResponse<bool>> RegisterTicket(DateTime purchaseDate);
    }
}
