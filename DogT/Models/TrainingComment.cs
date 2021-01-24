using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogT.Models
{
    public class TrainingComment
    {
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public int DogHandlerId { get; set; }
        public DogHandler DogHandler { get; set; }
        public string CommentContext { get; set; }
        public DateTime Date { get; set; }
    }
}
