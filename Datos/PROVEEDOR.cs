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
    
    public partial class PROVEEDOR
    {
        public PROVEEDOR()
        {
            this.PEDIDO = new HashSet<PEDIDO>();
            this.RECEPCION = new HashSet<RECEPCION>();
            this.PRODUCTO = new HashSet<PRODUCTO>();
        }
    
        public int RUT_PROVEEDOR { get; set; }
        public string DV_PROVEEDOR { get; set; }
        public string PNOMBRE_PROVEEDOR { get; set; }
        public string SNOMBRE_PROVEEDOR { get; set; }
        public string APP_PATERNO_PROVEEDOR { get; set; }
        public string APP_MATERNO_PROVEEDOR { get; set; }
        public short ID_TIPO_PROVEEDOR { get; set; }
        public short ID_USUARIO { get; set; }
    
        public virtual ICollection<PEDIDO> PEDIDO { get; set; }
        public virtual TIPO_PROVEEDOR TIPO_PROVEEDOR { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        public virtual ICollection<RECEPCION> RECEPCION { get; set; }
        public virtual ICollection<PRODUCTO> PRODUCTO { get; set; }
    }
}
