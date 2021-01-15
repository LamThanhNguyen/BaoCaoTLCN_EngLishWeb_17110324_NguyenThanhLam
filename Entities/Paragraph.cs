using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Entities
{
    [Table("Paragraph")]
    public class Paragraph
    {
        public int Id { get; set; }
        [Required]
        public string ToeicNumber { get; set; }
        [Required]
        public string ToeicPart { get; set; }
        [Required]
        public string QuestionNumber { get; set; }
        [Required]
        public string ParagraphText { get; set; }
    }
}
