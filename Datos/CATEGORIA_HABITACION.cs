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
    
    public partial class CATEGORIA_HABITACION
    {
        public CATEGORIA_HABITACION()
        {
            this.HABITACION = new HashSet<HABITACION>();
            this.DETALLE_ORDEN = new HashSet<DETALLE_ORDEN>();
        }
    
        public short ID_CATEGORIA_HABITACION { get; set; }
        public string NOMBRE_CATEGORIA { get; set; }
        public int PRECIO_CATEGORIA { get; set; }
    
        public virtual ICollection<HABITACION> HABITACION { get; set; }
        public virtual ICollection<DETALLE_ORDEN> DETALLE_ORDEN { get; set; }
    }
}
