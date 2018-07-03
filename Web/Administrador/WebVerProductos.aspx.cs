using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using WcfNegocio;
using System.Xml.Serialization;
using System.IO;
using System.Data;

namespace Web.Administrador
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
                        MasterPageFile = "~/Administrador/AdminM.Master";
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
            Service1 service = new Service1();
            string datos = service.ListarProducto();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.ProductoCollection));
            StringReader reader = new StringReader(datos);

            Modelo.ProductoCollection listaProducto = (Modelo.ProductoCollection)ser.Deserialize(reader);
            reader.Close();
            CargarGrid(listaProducto);
        }

        private void CargarGrid (ProductoCollection listaProducto) {
            Familia familia;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID_PRODUCTO", typeof(string)),
                new DataColumn("NOMBRE_PRODUCTO", typeof(string)),
                new DataColumn("PRECIO_PRODUCTO",typeof(string)),
                new DataColumn("DESCRIPCION_PRODUCTO",typeof(string)),
                new DataColumn("STOCK_PRODUCTO",typeof(string)),
                new DataColumn("STOCK_CRITICO_PRODUCTO",typeof(string)),
                new DataColumn("UNIDAD_MEDIDA",typeof(string)),
                new DataColumn("FAMILIA",typeof(string)),
                new DataColumn("FECHA_VENCIMIENTO_PRODUCTO",typeof(string))
            });

            foreach (Producto p in listaProducto) {
                familia = new Familia();
                familia.ID_FAMILIA = p.ID_FAMILIA;
                familia.BuscarFamilia();

                dt.Rows.Add(p.ID_PRODUCTO,p.NOMBRE_PRODUCTO,"$" + p.PRECIO_PRODUCTO,p.STOCK_PRODUCTO,p.STOCK_CRITICO_PRODUCTO,p.UNIDAD_MEDIDA,familia.NOMBRE_FAMILIA,p.FECHA_VENCIMIENTO_PRODUCTO.Value.ToShortDateString());
            }

            //Carga de GriedView
            gvProducto.DataSource = dt;
            gvProducto.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            long ID_PRODUCTO = long.Parse(btn.CommandArgument);

            Producto producto = new Producto();
            producto.ID_PRODUCTO = ID_PRODUCTO;
            MiSesionp = producto;
            

            Response.Redirect("../Administrador/WebEditarProducto.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            long ID_PRODUCTO = long.Parse(btn.CommandArgument);

            Producto producto = new Producto();
            producto.ID_PRODUCTO = ID_PRODUCTO;
            MiSesionp = producto;

            Response.Redirect("../Administrador/WebEliminarProducto.aspx");
        }
        
        protected void gvProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProducto.PageIndex = e.NewPageIndex;
            gvProducto.DataBind();
        }
        
        public Producto MiSesionp
        {
            get
            {
                if (Session["Producto"] == null)
                {
                    Session["Producto"] = new Producto();
                }
                return (Producto)Session["Producto"];
            }
            set
            {
                Session["Producto"] = value;
            }
        }
        
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
    }
}