﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class OrdenCompra
    {
        public short NUMERO_ORDEN { get; set; }
        public int CANTIDAD_HUESPEDES { get; set; }
        public System.DateTime FECHA_LLEGADA { get; set; }
        public Nullable<System.DateTime> FECHA_SALIDA { get; set; }
        public Nullable<int> RUT_EMPLEADO { get; set; }
        public Nullable<int> RUT_CLIENTE { get; set; }
        public string ESTADO_ORDEN { get; set; }
        public string COMENTARIO { get; set; }
    }
}