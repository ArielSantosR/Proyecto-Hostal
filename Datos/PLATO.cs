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
    
    public partial class PLATO
    {
        public PLATO()
        {
            this.DETALLE_PLATOS = new HashSet<DETALLE_PLATOS>();
        }
    
        public short ID_PLATO { get; set; }
        public string NOMBRE_PLATO { get; set; }
        public int PRECIO_PLATO { get; set; }
        public short ID_CATEGORIA { get; set; }
        public short ID_TIPO_PLATO { get; set; }
    
        public virtual CATEGORIA CATEGORIA { get; set; }
        public virtual ICollection<DETALLE_PLATOS> DETALLE_PLATOS { get; set; }
        public virtual TIPO_PLATO TIPO_PLATO { get; set; }
    }
}
