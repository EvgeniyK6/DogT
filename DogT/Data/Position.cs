using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogT.Data
{
    public enum Position
    {
        [Display(Name = "Молодший інспектор-кінолог")]
        Молодший,
        [Display(Name = "Інспектор-кінолог")]
        Інспектор,
        [Display(Name = "Старший інспектор-кінолог")]
        Старший
    }
}
