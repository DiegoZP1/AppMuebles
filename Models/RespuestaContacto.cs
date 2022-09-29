using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace AppMuebles.Models
{
    [Table("t_resp_contact")]
    public class RespuestaContacto
    {    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID {get; set;}

        public Contacto Contacto {get; set;}

        public String Respuesta {get; set;}
        public DateTime RespuestatDate { get; set; }
        
    }
}