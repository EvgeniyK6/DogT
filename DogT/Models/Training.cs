using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogT.Models
{
    public class Training
    {
        public int Id { get; set; }
        public int DogHandlerId { get; set; }
        public DogHandler DogHandler { get; set; }
        public int DogId { get; set; }
        public Dog Dog { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public string Context { get; set; }
        public string Estimate { get; set; }
        public DateTime Date { get; set; }
        public List<TrainingComment> Comments { get; set; }
    }
}
