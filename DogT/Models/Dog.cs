using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogT.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Display(Name = "Кличка")]
        public string Name { get; set; }

        [Display(Name = "Вік")]
        public int Age { get; set; }

        [Display(Name = "Кінолог")]
        public int DogHandlerId { get; set; }
        [Display(Name = "Кінолог")]
        public DogHandler DogHandler { get; set; }

        [Display(Name = "Спеціалізація")]
        public int SpecializationId { get; set; }
        [Display(Name = "Спеціалізація")]
        public Specialization Specialization { get; set; }

        public List<Training> Trainings { get; set; }
        public List<TrainingTask> TrainingTasks { get; set; }
        public string Avatar { get; set; }
        public string AvatarPath { get; set; }
    }
}
