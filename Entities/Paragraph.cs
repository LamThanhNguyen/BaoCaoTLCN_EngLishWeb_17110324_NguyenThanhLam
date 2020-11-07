using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Entities
{
    [Table("Paragraph")]
    public class Paragraph
    {
        public int Id { get; set; }
        public string ToeicNumber { get; set; }
        public string ToeicPart { get; set; }
        public string QuestionNumber { get; set; }
        public string ParagraphText { get; set; }
    }
}
