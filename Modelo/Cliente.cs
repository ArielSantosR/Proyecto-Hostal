using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Cliente
    {
        public int RUT_CLIENTE { get; set; }
        public string DV_CLIENTE { get; set; }
        public string DIRECCION_CLIENTE { get; set; }
        public string CORREO_CLIENTE { get; set; }
        public Nullable<long> TELEFONO_CLIENTE { get; set; }
        public short ID_COMUNA { get; set; }
        public short ID_USUARIO { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
    }
}
