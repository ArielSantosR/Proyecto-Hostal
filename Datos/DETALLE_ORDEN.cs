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
    
    public partial class DETALLE_ORDEN
    {
        public int ID_DETALLE { get; set; }
        public short NUMERO_ORDEN { get; set; }
        public int RUT_HUESPED { get; set; }
        public short ID_CATEGORIA_HABITACION { get; set; }
        public short ID_PENSION { get; set; }
        public string ESTADO { get; set; }
        public int VALOR_MINUTA { get; set; }
        public int VALOR_HABITACION { get; set; }
        public short ID_TIPO_HABITACION { get; set; }
    
        public virtual CATEGORIA_HABITACION CATEGORIA_HABITACION { get; set; }
        public virtual TIPO_HABITACION TIPO_HABITACION { get; set; }
        public virtual PENSION PENSION { get; set; }
        public virtual HUESPED HUESPED { get; set; }
        public virtual ORDEN_COMPRA ORDEN_COMPRA { get; set; }
    }
}
