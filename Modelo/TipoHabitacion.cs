using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class TipoHabitacion
    {
        public short ID_TIPO_HABITACION { get; set; }
        public string NOMBRE_TIPO_HABITACION { get; set; }
        public short CANTIDAD_PASAJERO { get; set; }
        public int PRECIO_TIPO { get; set; }

        public bool BuscarTipo() {
            ServicioCategoria serv = new ServicioCategoria();
            TIPO_HABITACION datos = serv.BuscarTipo(this.ID_TIPO_HABITACION);
            if (datos != null) {
                this.NOMBRE_TIPO_HABITACION = datos.NOMBRE_TIPO_HABITACION;
                this.PRECIO_TIPO = datos.PRECIO_TIPO;
                this.CANTIDAD_PASAJERO = datos.CANTIDAD_PASAJERO;

                return true;
            }
            else {
                return false;
            }
        }

        public string NombreYPrecio {
            get { return this.NOMBRE_TIPO_HABITACION + " $" + this.PRECIO_TIPO; }
        }
    }
}
