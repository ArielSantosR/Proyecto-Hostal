using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Region
    {
        public int Id_Region { get; set; }
        public string Nombre { get; set; }
        public int Id_Pais { get; set; }
    }
}
