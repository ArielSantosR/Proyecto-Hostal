using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using WcfNegocio;

namespace Web.Empleado
{
    public partial class WebInventario : System.Web.UI.Page
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
            Service1 service = new Service1();
            string datos = service.ListarProducto();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.ProductoCollection));
            StringReader reader = new StringReader(datos);

            Modelo.ProductoCollection listaProducto = (Modelo.ProductoCollection)ser.Deserialize(reader);
            reader.Close();
            gvProducto.DataSource = listaProducto;
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


            Response.Redirect("../Empleado/WebEditarInventario.aspx");
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