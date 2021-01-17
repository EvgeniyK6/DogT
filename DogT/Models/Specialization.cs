using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogT.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        [Display(Name = "Спеціалізація")]
        public string Title { get; set; }
        public List<Dog> Dogs { get; set; }
        public List<Training> Trainings { get; set; }
    }
}
