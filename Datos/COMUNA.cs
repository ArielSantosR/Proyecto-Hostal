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
    
    public partial class COMUNA
    {
        public COMUNA()
        {
            this.CLIENTE = new HashSet<CLIENTE>();
        }
    
        public short ID_COMUNA { get; set; }
        public string NOMBRE_COMUNA { get; set; }
        public short ID_REGION { get; set; }
    
        public virtual ICollection<CLIENTE> CLIENTE { get; set; }
        public virtual REGION REGION { get; set; }
    }
}
