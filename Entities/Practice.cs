using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Entities
{
    [Table("Practice")]
    public class Practice
    {
        public int Id { get; set; }
        [Required]
        public string PracticeName { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string AnswerA { get; set; }
        [Required]
        public string AnswerB { get; set; }
        [Required]
        public string AnswerC { get; set; }
        [Required]
        public string AnswerD { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
