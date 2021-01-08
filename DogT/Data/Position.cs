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
        Junior,
        [Display(Name = "Інспектор-кінолог")]
        Middle,
        [Display(Name = "Старший інспектор-кінолог")]
        Senior
    }
}
