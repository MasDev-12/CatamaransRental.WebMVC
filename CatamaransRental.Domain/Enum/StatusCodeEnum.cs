using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatamaransRental.Domain.Enum
{
    public enum StatusCodeEnum
    {
        //Car
        NotFound = 0,
        //общее
        OK = 200,
        InternalServerError = 500//Ошибка на стороне нашего сервеса
    }
}
