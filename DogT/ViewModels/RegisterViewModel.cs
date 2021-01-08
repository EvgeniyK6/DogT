using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogT.Data;
using System.ComponentModel.DataAnnotations;

namespace DogT.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не введений емейл")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Емейл")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Не введений пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [Display(Name = "Підтверження паролю")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Прізвище")]
        public string Surname { get; set; }
        
        [Required]
        [Display(Name = "Позиція")]
        public Position Position { get; set; }
    }
}
