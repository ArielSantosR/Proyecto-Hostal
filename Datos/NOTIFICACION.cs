//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class NOTIFICACION
    {
        public int ID_NOTIFICACION { get; set; }
        public string MENSAJE { get; set; }
        public string URL { get; set; }
        public short ID_USUARIO { get; set; }
        public Nullable<short> NUMERO_PEDIDO { get; set; }
        public Nullable<long> ID_PRODUCTO { get; set; }
        public Nullable<short> NUMERO_ORDEN { get; set; }
        public string ESTADO_NOTIFICACION { get; set; }
    
        public virtual ORDEN_COMPRA ORDEN_COMPRA { get; set; }
        public virtual PEDIDO PEDIDO { get; set; }
        public virtual PRODUCTO PRODUCTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
