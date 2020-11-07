using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Entities
{
    [Table("Grammar")]
    public class Grammar
    {
        public int Id { get; set; }
        public string GrammarName { get; set; }
        public string Structure { get; set; }
        public string Description { get; set; }
        public string Example { get; set; }
    }
}
