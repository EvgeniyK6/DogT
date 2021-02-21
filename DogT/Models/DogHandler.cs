using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DogT.Data;

namespace DogT.Models
{
    public class DogHandler
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Display(Name = "Прізвище")]
        public string Surname { get; set; }
        
        [Display(Name = "Посада")]
        public Position Position { get; set; }
        public List<Dog> Dogs { get; set; }
        public List<Training> Trainings { get; set; }
        public List<TrainingTask> TrainingTasks { get; set; }
        public List<TrainingComment> TrainingComments { get; set; }
        public List<Proposal> Proposals { get; set; }
    }
}
