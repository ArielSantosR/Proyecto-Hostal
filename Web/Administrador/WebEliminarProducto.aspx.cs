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

namespace Web.Administrador
{
    public partial class WebEliminarProducto : System.Web.UI.Page
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
            error.Text = "";
            alerta.Visible = false;

            if (!IsPostBack)
            {
                if (MiSesionP.ID_PRODUCTO != 0)
                {
                    Modelo.Producto producto = new Modelo.Producto();
                    producto.ID_PRODUCTO = MiSesionP.ID_PRODUCTO;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Producto));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, producto);

                    if (s.ObtenerProducto(writer.ToString()) != null)
                    {
                        producto = s.ObtenerProducto(writer.ToString());

                        txtNumero.Text = producto.ID_PRODUCTO + "";

                        txtNumero.ReadOnly = true;
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('No puede acceder a esta página');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Modelo.Producto producto = new Modelo.Producto();
            producto.ID_PRODUCTO = MiSesionP.ID_PRODUCTO;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Producto));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, producto);

            if (s.EliminarProducto(writer.ToString()))
            {
                MiSesionP = null;
                Response.Write("<script language='javascript'>window.alert('El Producto ha sido Eliminado con éxito');window.location='../Administrador/WebVerProductos.aspx';</script>");
                alerta.Visible = false;
            }
            else
            {
                error.Text = "No se ha podido Eliminar";
                alerta.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MiSesionP = null;
            Response.Write("<script language='javascript'>window.alert('Ha decidido no eliminar el Producto');window.location='../Administrador/WebVerProductos.aspx';</script>");
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

        public Producto MiSesionP
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
    }
}