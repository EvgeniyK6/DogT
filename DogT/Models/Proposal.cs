using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogT.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        [Display(Name ="Зміст")]
        public string Context { get; set; }
        public DateTime Date { get; set; }
        public int DogHandlerId { get; set; }
        public DogHandler DogHandler { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
