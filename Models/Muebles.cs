using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMuebles.Models
{
    [Table("t_muebles")]
    public class Muebles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
         public int Id {get; set; }

        [Column("nomPro")]
        public string Nombre {get; set; }
        [Column("estM")]
        public string EstadoMue {get; set; }
        [Column("desc")]
        public string Descripcion {get; set; }
        [Column("precio")]
        public Decimal Precio {get; set; }
        [Column("categoria")]
        public string categoria {get; set; }
        [Column("marca")]
        public string marca {get; set; }
        [Column("color")]
        
        public string color {get; set; }
        [Column("descuento")]
        public Decimal PorcentajeDesc {get; set; }
        [Column("imagen")]
        public string Imagen {get; set; }
        [Column("status")]
        public string Status {get; set; }
    }
}