using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Usuario
    {
        public short ID_USUARIO { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string PASSWORD { get; set; }
        public string TIPO_USUARIO { get; set; }
        public string ESTADO { get; set; }
    }
}
