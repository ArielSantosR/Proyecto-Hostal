using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{

    [Serializable]
   public class Minuta
    {
        public short ID_PENSION { get; set; }
        public string NOMBRE_PENSION { get; set; }
        public int VALOR_PENSION { get; set; }
        public string HABILITADO { get; set; }

    }
}
