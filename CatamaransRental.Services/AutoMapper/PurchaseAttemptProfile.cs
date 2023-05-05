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
    public class PurchaseAttemptProfile:Profile
    {
        public PurchaseAttemptProfile()
        {
            CreateMap<PurchaseAttempt, PurchaseAttemptViewModel>()
                .ForMember(dest=>dest.CatamaranId,opt=>opt.MapFrom(src=>src.CatamaranId))
                .ForMember(dest => dest.CatamaranVIewModel, opt => opt.MapFrom(src => src.Catamaran))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate))
                .ForMember(dest => dest.IsSuccessful, opt => opt.MapFrom(src => src.IsSuccessful))
                .ForMember(dest => dest.ErrorMessage, opt => opt.MapFrom(src => src.ErrorMessage))
                .ForMember(dest => dest.SuccessMessage, opt => opt.MapFrom(src => src.SuccessMessage))
                .ReverseMap();
        }
    }
}
