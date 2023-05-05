using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CatamaransRental.Domain.Enum
{
    public enum Role
    {
        [Display(Name = "Пользователь")]
        User = 0,
        [Display(Name = "Контроллер")]
        Сontroller = 1,
        [Display(Name = "Админ")]
        Admin = 2,
    }
}
