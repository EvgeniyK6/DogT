using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogT.Models
{
    public class Training
    {
        public int Id { get; set; }
        
        [Display(Name ="Кінолог")]
        public int DogHandlerId { get; set; }
        public DogHandler DogHandler { get; set; }
        
        [Display(Name = "Пес")]
        public int DogId { get; set; }
        public Dog Dog { get; set; }

        [Display(Name = "Спеціалізація")]
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        [Display(Name = "Зміст тренування")]
        public string Context { get; set; }

        [Display(Name = "Результат/Оцінка")]
        public string Estimate { get; set; }

        [Display(Name = "Дата проведення")]
        public DateTime Date { get; set; }
        public List<TrainingComment> Comments { get; set; }
    }
}
