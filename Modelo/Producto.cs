using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Producto

        
    {
        public long ID_PRODUCTO { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public DateTime? FECHA_VENCIMIENTO_PRODUCTO { get; set; }
        public short STOCK_PRODUCTO { get; set; }
        public short STOCK_CRITICO_PRODUCTO { get; set; }
        public string DESCRIPCION_PRODUCTO { get; set; }
        public int PRECIO_PRODUCTO { get; set; }
        public short ID_FAMILIA { get; set; }
        public int RUT_PROVEEDOR { get; set; }
        public short ID_PRODUCTO_SEQ { get; set; }
        public string UNIDAD_MEDIDA { get; set; }

        public string NombreYPrecio
        {
            get { return this.NOMBRE_PRODUCTO + " " + this.UNIDAD_MEDIDA + " $" + this.PRECIO_PRODUCTO; }
        }

        public bool Read() {
            Datos.ServicioProducto ser = new Datos.ServicioProducto();
            Datos.PRODUCTO dato = ser.BuscarProducto(this.ID_PRODUCTO);
            if (dato != null) {
                this.DESCRIPCION_PRODUCTO = dato.DESCRIPCION_PRODUCTO;
                this.FECHA_VENCIMIENTO_PRODUCTO = dato.FECHA_VENCIMIENTO_PRODUCTO;
                this.ID_FAMILIA = dato.ID_FAMILIA;
                this.ID_PRODUCTO_SEQ = dato.ID_PRODUCTO_SEQ;
                this.NOMBRE_PRODUCTO = dato.NOMBRE_PRODUCTO;
                this.PRECIO_PRODUCTO = dato.PRECIO_PRODUCTO;
                this.RUT_PROVEEDOR = dato.RUT_PROVEEDOR;
                this.STOCK_CRITICO_PRODUCTO = dato.STOCK_CRITICO_PRODUCTO;
                this.STOCK_PRODUCTO = dato.STOCK_PRODUCTO;
                this.UNIDAD_MEDIDA = dato.UNIDAD_MEDIDA;

                return true;
            }
            else {
                return false;
            }
        }
    }
}
