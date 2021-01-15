using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Entities
{
    [Table("Vocabulary")]
    public class Vocabulary
    {
        public int Id { get; set; }
        [Required]
        public string VietName { get; set; }
        [Required]
        public string EngName { get; set; }
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
