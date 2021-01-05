using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogT.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int DogHadnlerId { get; set; }
        public DogHandler DogHandler { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public List<Training> Trainings { get; set; }
    }
}
