using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class CategoriaHabitacion
    {
        public short ID_CATEGORIA_HABITACION { get; set; }
        public string NOMBRE_CATEGORIA { get; set; }
        public int PRECIO_CATEGORIA { get; set; }

        public bool BuscarCategoria() {
            ServicioCategoria serv = new ServicioCategoria();
            CATEGORIA_HABITACION datos = serv.BuscarCategoria(this.ID_CATEGORIA_HABITACION);
            if (datos != null) {
                this.NOMBRE_CATEGORIA = datos.NOMBRE_CATEGORIA;
                this.PRECIO_CATEGORIA = datos.PRECIO_CATEGORIA;

                return true;
            }
            else {
                return false;
            }
        }

        public string NombreYPrecio
        {
            get { return this.NOMBRE_CATEGORIA + " $" + this.PRECIO_CATEGORIA; }
        }


    }
}
