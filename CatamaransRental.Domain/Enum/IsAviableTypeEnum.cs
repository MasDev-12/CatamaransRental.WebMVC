using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CatamaransRental.Domain.Enum
{
    public enum IsAviableTypeEnum
    {
        [Display(Name = "Недоступно")]
        NotAviableToRent = 0,
        [Display(Name = "Доступно")]
        AviableToRent = 1,
    }
}
