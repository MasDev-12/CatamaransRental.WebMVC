using AutoMapper;
using CatamaransRental.Domain.ViewModel;
using CatamaransRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatamaransRental.WebMVC.Controllers
{
    public class CatamaranController: Controller
    {
        private readonly ICatamaranService _catamaranService;

        public CatamaranController(ICatamaranService catamaranService)
        {
            _catamaranService=catamaranService;
        }

        public IActionResult GetAll()
        {
            IEnumerable<CatamaranViewModel> catamaranVIewModels = _catamaranService.GetAllCatamarans().Data;
            return View(catamaranVIewModels);
        }

        public async Task<IActionResult> Detail(CatamaranViewModel catamaranVIewModel)
        {
            var catamaranViewModel = await _catamaranService.GetCatamaranByModel(catamaranVIewModel.Model);
            return View(catamaranViewModel.Data);
        }
    }
}
