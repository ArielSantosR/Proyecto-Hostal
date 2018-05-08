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
    public partial class WebEliminarCategoriaPlato : System.Web.UI.Page
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

        public Categoria MiSesionCategoria
        {
            get
            {
                if (Session["Categoria"] == null)
                {
                    Session["Categoria"] = new Categoria();
                }
                return (Categoria)Session["Categoria"];
            }
            set
            {
                Session["Categoria"] = value;
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MiSesionCategoria = null;
            Response.Write("<script language='javascript'>window.alert('Ha decidido no eliminar la Categoría');window.location='../Administrador/WebVerCategoriaPlato.aspx';</script>");
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Modelo.Categoria categoria = new Modelo.Categoria();
            categoria.ID_CATEGORIA = MiSesionCategoria.ID_CATEGORIA;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Categoria));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, categoria);

            if (s.EliminarCategoria(writer.ToString()))
            {
                MiSesionCategoria = null;
                Response.Write("<script language='javascript'>window.alert('La categoría ha sido Eliminado con éxito');window.location='../Administrador/WebVerCategoriaPlato.aspx';</script>");
                alerta.Visible = false;
            }
            else
            {
                error.Text = "No se ha podido Eliminar";
                alerta.Visible = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            error.Text = "";
            alerta.Visible = false;

            if (!IsPostBack)
            {
                if (MiSesionCategoria.ID_CATEGORIA != 0)
                {
                    Modelo.Categoria categoria = new Modelo.Categoria();
                    categoria.ID_CATEGORIA = MiSesionCategoria.ID_CATEGORIA;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Categoria));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, categoria);

                    if (s.ObtenerCategoria(writer.ToString()) != null)
                    {
                        categoria = s.ObtenerCategoria(writer.ToString());

                        txtNombre.Text = categoria.NOMBRE_CATEGORIA + "";

                        txtNombre.ReadOnly = true;


                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('No puede acceder a esta página');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
        }
    }
}