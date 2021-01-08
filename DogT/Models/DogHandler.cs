using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public string Surname { get; set; }
        public Position Position { get; set; }
        public List<Dog> Dogs { get; set; }
        public List<Training> Trainings { get; set; }

    }
}
