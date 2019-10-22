using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_AP1.Entidades
{
    public class Facturas
    {
        [Key]
        public int FacturaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estudiante { get; set; }
    
        public float Total { get; set; }

        public virtual List<ServicioDetalle> Servicios { get; set; }

        public Facturas()
        {
            FacturaId = 0;
            Fecha = DateTime.Now;
            Estudiante = string.Empty;
            Servicios = new List<ServicioDetalle>();
            Total = 0;
        }
    }
}
