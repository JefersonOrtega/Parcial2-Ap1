using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_AP1.Entidades
{
    public class ServicioDetalle
    {
        [Key]
        public int ServicioId { get; set; }
        public int FacturaId { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public float Importe { get; set; }

        public ServicioDetalle()
        {
            ServicioId = 0;
            FacturaId = 0;
            Categoria = string.Empty;
            Cantidad = 0;
            Precio = 0;
            Importe = 0;
        }


        public ServicioDetalle(int ServicioId, int FacturaId, string Categoria, int Cantidad, float Precio, float Importe)
        {
            this.ServicioId = ServicioId;
            this.FacturaId = FacturaId;
            this.Categoria = Categoria;
            this.Cantidad = Cantidad;
            this.Precio = Precio;
            this.Importe = Importe;
        }
        


    }
}
