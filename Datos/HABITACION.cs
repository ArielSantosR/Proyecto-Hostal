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
            this.HUESPED = new HashSet<HUESPED>();
            this.PENSION = new HashSet<PENSION>();
        }
    
        public short NUMERO_HABITACION { get; set; }
        public string ESTADO_HABITACION { get; set; }
        public short ID_TIPO_HABITACION { get; set; }
        public Nullable<int> RUT_CLIENTE { get; set; }
        public short ID_CATEGORIA_HABITACION { get; set; }
    
        public virtual CATEGORIA_HABITACION CATEGORIA_HABITACION { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual TIPO_HABITACION TIPO_HABITACION { get; set; }
        public virtual ICollection<HUESPED> HUESPED { get; set; }
        public virtual ICollection<PENSION> PENSION { get; set; }
    }
}
