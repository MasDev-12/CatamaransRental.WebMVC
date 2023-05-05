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
    public interface IPurchaseService
    {
        Task<IBaseResponse<bool>> CreatePurchase(RentalViewModel rentalViewModel);
    }
}
