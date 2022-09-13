
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMuebles.Models
{
    [Table("t_proforma")]
    public class Proforma
    {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       [Column("id")]

       public int id { get; set; }

        public String UserID { get; set; }
        public Muebles Producto { get; set; }
        public int Cantidad { get; set; }
        public Decimal Precio{ get; set;}
        public String Status{ get; set;}="Pendiente";

    }
}


