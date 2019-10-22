using Parcial2_AP1.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_AP1.BLL
{
    public class FacturasBLL : RepositorioBase<Facturas>
    {
        
        public override bool Modificar(Facturas factura)
        {

            var Anterior = base._contexto.Facturas.Find(factura.FacturaId);
            foreach (var item in Anterior.Servicios)
            {
                if (!factura.Servicios.Exists(d => d.ServicioId == item.ServicioId)) //
                    base._contexto.Entry(item).State = EntityState.Deleted;
            }

            bool paso = base.Modificar(factura);
            return paso;
        }

        public override Facturas Buscar(int id)
        {
            Facturas facturas = new Facturas();

            facturas.Servicios.Count(); //COunt para hacer al lazyloading cargar los detalles

            return base.Buscar(id);
        }
    }
}
