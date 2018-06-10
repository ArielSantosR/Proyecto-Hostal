using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Habitacion
    {
        public short NUMERO_HABITACION { get; set; }
        public string ESTADO_HABITACION { get; set; }
        public short ID_TIPO_HABITACION { get; set; }
        public short ID_CATEGORIA_HABITACION { get; set; }

        public string DatosHabitacion
        {
            get
            {
                string datos = string.Empty;
                switch (this.ID_CATEGORIA_HABITACION)
                {
                    case 1:
                        datos = "Habitación: " + this.NUMERO_HABITACION + "- Categoría: Bronce - Cantidad de Huéspedes: " + this.ID_TIPO_HABITACION + " - Estado: " + this.ESTADO_HABITACION;
                        break;

                    case 2:
                        datos = "Habitación: " + this.NUMERO_HABITACION + "- Categoría: Plata - Cantidad de Huéspedes: " + this.ID_TIPO_HABITACION + " - Estado: " + this.ESTADO_HABITACION;
                        break;

                    case 3:
                        datos = "Habitación: " + this.NUMERO_HABITACION + "- Categoría: Oro - Cantidad de Huéspedes: " + this.ID_TIPO_HABITACION + " - Estado: " + this.ESTADO_HABITACION;
                        break;

                    case 4:
                        datos = "Habitación: " + this.NUMERO_HABITACION + "- Categoría: Platino - Cantidad de Huéspedes: " + this.ID_TIPO_HABITACION + " - Estado: " + this.ESTADO_HABITACION;
                        break;

                    case 5:
                        datos = "Habitación: " + this.NUMERO_HABITACION + "- Categoría: Diamante - Cantidad de Huéspedes: " + this.ID_TIPO_HABITACION + " - Estado: " + this.ESTADO_HABITACION;
                        break;
                }
                return datos;
             }
        }
    }
}
