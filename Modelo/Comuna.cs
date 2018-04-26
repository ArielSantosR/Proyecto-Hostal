using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    [Serializable]
    public class Comuna
    {
        public int Id_Comuna { get; set; }
        public string Nombre { get; set; }
        public int Id_Region { get; set; }
    }
}
