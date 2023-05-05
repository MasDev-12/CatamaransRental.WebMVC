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
    public class TicketProfile:Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketViewModel>()
                .ForMember(dest => dest.CatamaranId, opt => opt.MapFrom(src => src.CatamaranId))
                .ForMember(dest => dest.CatamaranViewModel, opt => opt.MapFrom(src => src.Catamaran))
                .ForMember(dest => dest.RentalId, opt => opt.MapFrom(src => src.RentalId))
                .ForMember(dest => dest.Rental, opt => opt.MapFrom(src => src.Rental))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate))
                .ForMember(dest => dest.DurationInHours, opt => opt.MapFrom(src => src.DurationInHours))
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
                .ForMember(dest => dest.IsUsed, opt => opt.MapFrom(src => src.IsUsed))
                .ReverseMap();
        }
    }
}
