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
    
    public partial class FACTURA
    {
        public FACTURA()
        {
            this.DETALLE_FACTURA = new HashSet<DETALLE_FACTURA>();
        }
    
        public int ID_FACTURA { get; set; }
        public long VALOR_NETO_FACTURA { get; set; }
        public long VALOR_IVA_FACTURA { get; set; }
        public Nullable<decimal> VALOR_DESC_FACTURA { get; set; }
        public long VALOR_TOTAL_FACTURA { get; set; }
        public System.DateTime FECHA_EMISION_FACTURA { get; set; }
        public int RUT_CLIENTE { get; set; }
        public int RUT_EMPLEADO { get; set; }
        public string METODO_PAGO { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual ICollection<DETALLE_FACTURA> DETALLE_FACTURA { get; set; }
        public virtual EMPLEADO EMPLEADO { get; set; }
    }
}
