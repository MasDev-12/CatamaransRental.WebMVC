using AutoMapper;
using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.AutoMapper
{
    public class PurchaseProfile:Profile
    {
        public PurchaseProfile()
        {
            CreateMap<Purchase, PurchaseViewModel>()
                .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.TicketId))
                .ForMember(dest => dest.TicketViewModel, opt => opt.MapFrom(src => src.Ticket))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ReverseMap();
        }
    }
}
