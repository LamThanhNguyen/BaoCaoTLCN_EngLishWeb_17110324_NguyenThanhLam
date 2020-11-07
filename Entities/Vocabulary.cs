using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Entities
{
    [Table("Vocabulary")]
    public class Vocabulary
    {
        public int Id { get; set; }
        public string VietName { get; set; }
        public string EngName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
