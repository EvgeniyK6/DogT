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
        [Display(Name = "Кінолог")]
        public DogHandler DogHandler { get; set; }
        
        [Display(Name = "Пес")]
        public int DogId { get; set; }
        [Display(Name = "Пес")]
        public Dog Dog { get; set; }

        [Display(Name = "Спеціалізація")]
        public int SpecializationId { get; set; }
        [Display(Name = "Спеціалізація")]
        public Specialization Specialization { get; set; }

        [Display(Name = "Зміст")]
        public string Context { get; set; }

        [Display(Name = "Результат")]
        public string Estimate { get; set; }

        [Required, Display(Name = "Дата")]
        public DateTime Date { get; set; }
        public List<TrainingComment> Comments { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
