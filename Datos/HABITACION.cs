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
    
    public partial class HABITACION
    {
        public HABITACION()
        {
            this.DETALLE_HABITACION = new HashSet<DETALLE_HABITACION>();
        }
    
        public short NUMERO_HABITACION { get; set; }
        public string ESTADO_HABITACION { get; set; }
        public short ID_TIPO_HABITACION { get; set; }
        public short ID_CATEGORIA_HABITACION { get; set; }
    
        public virtual CATEGORIA_HABITACION CATEGORIA_HABITACION { get; set; }
        public virtual ICollection<DETALLE_HABITACION> DETALLE_HABITACION { get; set; }
        public virtual TIPO_HABITACION TIPO_HABITACION { get; set; }
    }
}
