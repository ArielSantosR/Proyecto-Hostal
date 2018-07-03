using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Categoria
    {
        public short ID_CATEGORIA { get; set; }
        public string NOMBRE_CATEGORIA { get; set; }

        public bool BuscarCategoria () {
            ServicioMinuta serv = new ServicioMinuta();
            CATEGORIA dato = serv.BuscarCategoria(this.ID_CATEGORIA);
            if (dato != null) {
                this.NOMBRE_CATEGORIA = dato.NOMBRE_CATEGORIA;

                return true;
            } else {
                return false;

            }
        }
    }
}
