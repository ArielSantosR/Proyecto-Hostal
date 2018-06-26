using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class DetalleOrdenCollection : List<DetalleOrden> {
        public static List<DetalleOrden> ListarDetalles() {
            ServicioReserva ser = new ServicioReserva();
            return GenerarLista(ser.ListaDetalleReserva());
        }

        private static List<DetalleOrden> GenerarLista(List<DETALLE_ORDEN> listDatos) {
            List<DetalleOrden> list = new List<DetalleOrden>();
            DetalleOrden detalle;
            foreach (DETALLE_ORDEN dato in listDatos) {
                detalle = new DetalleOrden();
                detalle.ESTADO = dato.ESTADO;
                detalle.ID_CATEGORIA_HABITACION = dato.ID_CATEGORIA_HABITACION;
                detalle.ID_DETALLE = dato.ID_DETALLE;
                detalle.ID_PENSION = dato.ID_PENSION;
                detalle.NUMERO_ORDEN = dato.NUMERO_ORDEN;
                detalle.RUT_HUESPED = dato.RUT_HUESPED;
                detalle.VALOR_HABITACION = dato.VALOR_HABITACION;
                detalle.VALOR_MINUTA = dato.VALOR_MINUTA;

                list.Add(detalle);
            }
            return list;
        }
    }
}
