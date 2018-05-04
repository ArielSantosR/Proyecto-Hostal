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
    public partial class WebEliminarPlato : System.Web.UI.Page
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
                if (MiSesionPlato.ID_PLATO != 0)
                {
                    Modelo.Plato plato = new Modelo.Plato();
                    plato.ID_PLATO = MiSesionPlato.ID_PLATO;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Plato));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, plato);

                    if (s.ObtenerPlato(writer.ToString()) != null)
                    {
                        plato = s.ObtenerPlato(writer.ToString());

                        txtNumero.Text = plato.ID_PLATO + "";

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
            Modelo.Plato plato = new Modelo.Plato();
            plato.ID_PLATO = MiSesionPlato.ID_PLATO;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Plato));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, plato);

            if (s.EliminarPlato(writer.ToString()))
            {
                MiSesionPlato = null;
                Response.Write("<script language='javascript'>window.alert('El Plato ha sido Eliminado con éxito');window.location='../Administrador/WebVerPlatos.aspx';</script>");
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
            MiSesionPlato = null;
            Response.Write("<script language='javascript'>window.alert('Ha decidido no eliminar el Plato');window.location='../Administrador/WebVerPlatos.aspx';</script>");
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

        public Plato MiSesionPlato
        {
            get
            {
                if (Session["Plato"] == null)
                {
                    Session["Plato"] = new Plato();
                }
                return (Plato)Session["Plato"];
            }
            set
            {
                Session["Plato"] = value;
            }
        }   
    }
}