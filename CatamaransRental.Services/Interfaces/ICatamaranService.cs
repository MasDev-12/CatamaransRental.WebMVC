using CatamaransRental.Domain.Models;
using CatamaransRental.Domain.Response;
using CatamaransRental.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Services.Interfaces
{
    public interface ICatamaranService
    {
        IBaseResponse<IEnumerable<CatamaranViewModel>> GetAllCatamarans();
        Task<IBaseResponse<bool>> Update(int id);
        Task<IBaseResponse<CatamaranViewModel>> GetCatamaranByModel(string model);//искать по модели, конечно не самый лучший поиск, но т.к. использую viewmodel,
                                                                            //можно было придумать vin код и скрыть его, но т.к. база тестовая сделал так
    }
}
