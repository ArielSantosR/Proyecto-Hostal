﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Recepcion
    {
        public short NUMERO_RECEPCION { get; set; }
        public System.DateTime FECHA_RECEPCION { get; set; }
        public int RUT_PROVEEDOR { get; set; }
        public int RUT_EMPLEADO { get; set; }
    }
}