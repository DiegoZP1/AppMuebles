using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMuebles.Models
{


    [Table("t_contacto")]
    public class Contacto
    {
        
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
         public int Id {get; set; }

          [Column("nom")]
        public string Nombre {get; set; }
        [Column("email")]
        public string Email {get; set; }
        [Column("asunto")]
        public string Asunto {get; set; }
        [Column("mensj")]
        public string Mensaje {get; set; }
    }
}