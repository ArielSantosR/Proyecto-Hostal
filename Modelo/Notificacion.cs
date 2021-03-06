﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Notificacion
    {
        public int ID_NOTIFICACION { get; set; }
        public string MENSAJE { get; set; }
        public short ID_USUARIO { get; set; }
        public string ESTADO_NOTIFICACION { get; set; }
        public string URL { get; set; }
        public Nullable<short> NUMERO_PEDIDO { get; set; }
        public Nullable<long> ID_PRODUCTO { get; set; }
        public Nullable<short> NUMERO_ORDEN { get; set; }
    }
}
