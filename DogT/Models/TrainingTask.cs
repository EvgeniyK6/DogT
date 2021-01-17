using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DogT.Models
{
    public class TrainingTask
    {
        public int Id { get; set; }

        [Display(Name = "Собака")]
        public int DogId { get; set; }
        public Dog Dog { get; set; }

        [Display(Name = "Кінолог")]
        public int DogHandlerId { get; set; }
        public DogHandler DogHandler { get; set; }

        [Display(Name = "Зміст завдання")]
        public string Context { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Чи виконано завдання?")]
        public bool IsCompleted { get; set; }
    }
}
