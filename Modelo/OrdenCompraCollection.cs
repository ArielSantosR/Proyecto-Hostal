using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class OrdenCompraCollection : List<OrdenCompra> {

        public static List<OrdenCompra> Listar() {
            ServicioReserva ser = new ServicioReserva();
            return GenerarLista(ser.ListarReservaAsignada());
        }

        private static List<OrdenCompra> GenerarLista(List<ORDEN_COMPRA> listDatos) {
            List<OrdenCompra> list = new List<OrdenCompra>();
            OrdenCompra orden;
            foreach (ORDEN_COMPRA dato in listDatos) {
                orden = new OrdenCompra();
                orden.CANTIDAD_HUESPEDES = dato.CANTIDAD_HUESPEDES;
                orden.COMENTARIO = dato.COMENTARIO;
                orden.ESTADO_ORDEN = dato.ESTADO_ORDEN;
                orden.FECHA_LLEGADA = dato.FECHA_LLEGADA;
                orden.FECHA_SALIDA = dato.FECHA_SALIDA;
                orden.MONTO_TOTAL = dato.MONTO_TOTAL;
                orden.NUMERO_ORDEN = dato.NUMERO_ORDEN;
                orden.RUT_CLIENTE = dato.RUT_CLIENTE;
                orden.RUT_EMPLEADO = dato.RUT_EMPLEADO;

                list.Add(orden);
            }
            return list;
        }
    }
}
