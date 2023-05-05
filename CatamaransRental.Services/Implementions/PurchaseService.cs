using AutoMapper;
using CatamaransRental.DAL.Interfaces;
using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.Response;
using CatamaransRental.Domain.ViewModel;
using CatamaransRental.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.Implementions
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IBaseRepository<Purchase> _purchaseRepository;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Ticket> _ticketRepository;
        private readonly IBaseRepository<Rental> _rentalRepository;

        public PurchaseService(IBaseRepository<Purchase> purchaseRepository,IMapper mapper,IBaseRepository<Ticket> ticketRepository
            ,IBaseRepository<Rental> rentalRepository)
        {
            _purchaseRepository=purchaseRepository;
            _mapper=mapper;
            _ticketRepository=ticketRepository;
            _rentalRepository=rentalRepository;
        }
        public async Task<IBaseResponse<bool>> CreatePurchase(RentalViewModel rentalViewModel)
        {

            try
            {
                var respone = await _rentalRepository.GetAll().FirstOrDefaultAsync(r => r.StartTime==rentalViewModel.StartTime);
                var purchase = new Purchase()
                {
                    PurchaseDate= respone.StartTime,
                    Price = respone.TotalCost,
                    TicketId = _ticketRepository.GetAll().FirstOrDefaultAsync(t => t.PurchaseDate==rentalViewModel.StartTime).Result.Id,
                };
                await _purchaseRepository.Create(purchase);
                return new BaseResponse<bool>()
                {
                    Description="Покупка создана, на данный катамаран",
                    StatusCode=Domain.Enum.StatusCodeEnum.NotFound,
                    Data=true,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {

                    StatusCode=Domain.Enum.StatusCodeEnum.InternalServerError,
                    Description=$"Ошибка подключения + {ex.Message}",
                    Data=false
                };
            }

        }
    }
}
