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
    public partial class WebEliminarTipoPlato : System.Web.UI.Page
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

        public TipoPlato MiSesionPlato
        {
            get
            {
                if (Session["Plato"] == null)
                {
                    Session["Plato"] = new TipoPlato();
                }
                return (TipoPlato)Session["Plato"];
            }
            set
            {
                Session["Plato"] = value;
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MiSesionPlato = null;
            Response.Write("<script language='javascript'>window.alert('Ha decidido no eliminar el Tipo de Plato');window.location='../Administrador/WebVerPlatos.aspx';</script>");
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Modelo.TipoPlato tipoplato = new Modelo.TipoPlato();
            tipoplato.ID_TIPO_PLATO = MiSesionPlato.ID_TIPO_PLATO;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.TipoPlato));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, tipoplato);

            if (s.EliminarTipoPlato(writer.ToString()))
            {
                MiSesionPlato = null;
                Response.Write("<script language='javascript'>window.alert('El Tipo de Plato ha sido Eliminado con éxito');window.location='../Administrador/WebVerTipoPlato.aspx';</script>");
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
                if (MiSesionPlato.ID_TIPO_PLATO != 0)
                {
                    Modelo.TipoPlato tipoplato = new Modelo.TipoPlato();
                    tipoplato.ID_TIPO_PLATO = MiSesionPlato.ID_TIPO_PLATO;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.TipoPlato));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, tipoplato);

                    if (s.ObtenerTipoPlato(writer.ToString()) != null)
                    {
                        tipoplato = s.ObtenerTipoPlato(writer.ToString());

                        txtNombre.Text = tipoplato.NOMBRE_TIPO_PLATO + "";

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