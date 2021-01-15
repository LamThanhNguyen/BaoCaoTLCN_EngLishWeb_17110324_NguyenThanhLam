using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Entities
{
    [Table("QuestionReading")]
    public class QuestionReading
    {
        public int Id { get; set; }
        [Required]
        public string ToeicNumber { get; set; }
        [Required]
        public string ToeicPart { get; set; }
        [Required]
        public string QuestionNumber { get; set; }
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
