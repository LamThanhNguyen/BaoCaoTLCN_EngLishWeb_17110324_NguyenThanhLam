using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Entities
{
    [Table("Grammar")]
    public class Grammar
    {
        public int Id { get; set; }
        [Required]
        public string GrammarName { get; set; }
        [Required]
        public string Structure { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Example { get; set; }
    }
}
