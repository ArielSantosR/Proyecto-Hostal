using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioPedido
    {
        HostalEntities ent = new HostalEntities();

        public bool AgregarPedido(PEDIDO p)
        {
            ent.PEDIDO.Add(p);
            ent.SaveChanges();
            return true;
        }

        public bool AgregarDetallePedido(DETALLE_PEDIDO d)
        {
            ent.DETALLE_PEDIDO.Add(d);
            ent.SaveChanges();
            return true;
        }

        public List<PEDIDO> ListarPedido(PROVEEDOR proveedor)
        {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_PROVEEDOR == proveedor.RUT_PROVEEDOR
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public List<PEDIDO> ListarPedidoAdmin()
        {
            return (from a in ent.PEDIDO where a.ESTADO_PEDIDO.Equals("Pendiente") select a).ToList();
        }
    }
}
