using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogT.Data;
using System.ComponentModel.DataAnnotations;


namespace DogT.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не введений емейл")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Емейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не введений пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
