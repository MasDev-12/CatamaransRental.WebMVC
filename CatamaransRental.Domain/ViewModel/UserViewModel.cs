using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CatamaransRental.Domain
{
    public class UserViewModel
    {
        [ValidateNever]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [ValidateNever]
        [Required(ErrorMessage = "Укажите роль")]
        [Display(Name = "Роль")]
        public string Role { get; set; }
        [ValidateNever]
        [Required(ErrorMessage = "Укажите логин")]
        [Display(Name = "Логин")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Укажите пароль")]
        //[Display(Name = "Пароль")]
        //public string Password { get; set; }
    }
}
