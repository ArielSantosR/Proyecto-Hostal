using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Plato
    {
        public short ID_PLATO { get; set; }
        public string NOMBRE_PLATO { get; set; }
        public int PRECIO_PLATO { get; set; }
        public short ID_CATEGORIA { get; set; }
        public short ID_TIPO_PLATO { get; set; }

        public string NombreYPrecio
        {
            get
            {
                string nombre_precio = string.Empty;

                switch (this.ID_TIPO_PLATO){
                    case 1:
                        nombre_precio = "Desayuno: " + this.NOMBRE_PLATO + " $" + this.PRECIO_PLATO;
                        break;

                    case 2:
                        nombre_precio = "Almuerzo: " + this.NOMBRE_PLATO + " $" + this.PRECIO_PLATO;
                        break;

                    case 3:
                        nombre_precio = "Cena: " + this.NOMBRE_PLATO + " $" + this.PRECIO_PLATO;
                        break;

                }
                return nombre_precio;
            }
        }
    }

    
}
