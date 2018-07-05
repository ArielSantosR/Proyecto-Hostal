using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using WcfNegocio;

namespace Web.Empleado
{
    public partial class WebVerProductos : System.Web.UI.Page
    {
        void Page_PreInit(object sender, EventArgs e)
        {
            if (MiSesion != null)
            {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null)
                {
                    if (MiSesion.TIPO_USUARIO.Equals("Administrador") &&
                    MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        Response.Redirect("../Administrador/WebVerProductos.aspx");
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals("Empleado") &&
                    MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        MasterPageFile = "~/Empleado/EmpleadoM.Master";
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>window.alert('No Posee los permisos necesarios');window.location='../Hostal/WebLogin.aspx';</script>");
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Service1 service = new Service1();
                string datos = service.ListarProducto();
                XmlSerializer ser = new XmlSerializer(typeof(Modelo.ProductoCollection));
                StringReader reader = new StringReader(datos);

                Modelo.ProductoCollection listaProducto = (Modelo.ProductoCollection)ser.Deserialize(reader);
                reader.Close();
                gvProducto.DataSource = listaProducto;
                gvProducto.DataBind();
            }
            catch (Exception)
            {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
            }

        }

        //Creación de Sesión
        public Usuario MiSesion
        {
            get
            {
                if (Session["Usuario"] == null)
                {
                    Session["Usuario"] = new Usuario();
                }
                return (Usuario)Session["Usuario"];
            }
            set
            {
                Session["Usuario"] = value;
            }
        }

        private void CargarGrid (ProductoCollection listaProducto) {
            Familia familia;

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID_PRODUCTO", typeof(long)),
                new DataColumn("NOMBRE_PRODUCTO", typeof(string)),
                new DataColumn("PRECIO_PRODUCTO",typeof(string)),
                new DataColumn("DESCRIPCION_PRODUCTO",typeof(string)),
                new DataColumn("STOCK_PRODUCTO",typeof(short)),
                new DataColumn("STOCK_CRITICO_PRODUCTO",typeof(short)),
                new DataColumn("UNIDAD_MEDIDA",typeof(string)),
                new DataColumn("FAMILIA",typeof(string)),
                new DataColumn("FECHA_VENCIMIENTO_PRODUCTO",typeof(string))
            });

            foreach (Producto item in listaProducto) {
                familia = new Familia();
                familia.ID_FAMILIA = item.ID_FAMILIA;
                familia.BuscarFamilia();
                if (item.FECHA_VENCIMIENTO_PRODUCTO.HasValue) {
                    dt.Rows.Add(item.ID_PRODUCTO,item.NOMBRE_PRODUCTO,"$" + item.PRECIO_PRODUCTO,item.DESCRIPCION_PRODUCTO,item.STOCK_PRODUCTO,item.STOCK_CRITICO_PRODUCTO,item.UNIDAD_MEDIDA,familia.NOMBRE_FAMILIA,item.FECHA_VENCIMIENTO_PRODUCTO.Value.ToShortDateString());
                } else {
                    dt.Rows.Add(item.ID_PRODUCTO,item.NOMBRE_PRODUCTO,"$" + item.PRECIO_PRODUCTO,item.DESCRIPCION_PRODUCTO,item.STOCK_PRODUCTO,item.STOCK_CRITICO_PRODUCTO,item.UNIDAD_MEDIDA,familia.NOMBRE_FAMILIA,"Sin Fecha Vencimiento");
                }
            }

            gvProducto.DataSource = dt;
            gvProducto.DataBind();
        }
    }
}