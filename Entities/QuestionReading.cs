using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Entities
{
    [Table("QuestionReading")]
    public class QuestionReading
    {
        public int Id { get; set; }
        public string ToeicNumber { get; set; }
        public string ToeicPart { get; set; }
        public string QuestionNumber { get; set; }
        public string Question { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string Answer { get; set; }
    }
}
