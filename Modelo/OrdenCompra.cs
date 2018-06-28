using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class OrdenCompra {
        public short NUMERO_ORDEN { get; set; }
        public int CANTIDAD_HUESPEDES { get; set; }
        public System.DateTime FECHA_LLEGADA { get; set; }
        public System.DateTime FECHA_SALIDA { get; set; }
        public Nullable<int> RUT_EMPLEADO { get; set; }
        public int RUT_CLIENTE { get; set; }
        public string ESTADO_ORDEN { get; set; }
        public string COMENTARIO { get; set; }
        public int MONTO_TOTAL { get; set; }

        public string FechaLlegada {
            get {
                return FECHA_LLEGADA.ToString("dd/MM/yyyy");
            }
        }

        public string FechaSalida {
            get {
                return FECHA_SALIDA.ToString("dd/MM/yyyy");
            }
        }

        public bool BuscarOrden() {
            ServicioReserva serv = new ServicioReserva();
            ORDEN_COMPRA dato = serv.BuscarOrden(this.NUMERO_ORDEN);
            if (dato != null) {
                this.CANTIDAD_HUESPEDES = dato.CANTIDAD_HUESPEDES;
                this.COMENTARIO = dato.COMENTARIO;
                this.ESTADO_ORDEN = dato.ESTADO_ORDEN;
                this.FECHA_LLEGADA = dato.FECHA_LLEGADA;
                this.FECHA_SALIDA = dato.FECHA_SALIDA;
                this.MONTO_TOTAL = dato.MONTO_TOTAL;
                this.RUT_CLIENTE = dato.RUT_CLIENTE;
                this.RUT_EMPLEADO = dato.RUT_EMPLEADO;
                
                return true;
            }
            else {
                return false;

            }

        }

        public bool Update()
        {
            ServicioReserva serv = new ServicioReserva();
            ORDEN_COMPRA datos = new ORDEN_COMPRA();
            datos.CANTIDAD_HUESPEDES = this.CANTIDAD_HUESPEDES;
            datos.COMENTARIO = this.COMENTARIO;
            datos.ESTADO_ORDEN = this.ESTADO_ORDEN;
            datos.FECHA_LLEGADA = this.FECHA_LLEGADA;
            datos.FECHA_SALIDA = this.FECHA_SALIDA;
            datos.MONTO_TOTAL = this.MONTO_TOTAL;
            datos.RUT_CLIENTE = this.RUT_CLIENTE;
            datos.RUT_EMPLEADO = this.RUT_EMPLEADO;

            return serv.EditarEstadoReserva(datos);
        }
    }
}
