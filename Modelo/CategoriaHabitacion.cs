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
    }
}
