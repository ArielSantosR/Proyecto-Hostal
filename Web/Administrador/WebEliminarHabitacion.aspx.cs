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
    public partial class WebEliminarHabitacion : System.Web.UI.Page
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
                if (MiSesionH.NUMERO_HABITACION != 0)
                {
                    Modelo.Habitacion habitacion = new Modelo.Habitacion();
                    habitacion.NUMERO_HABITACION = MiSesionH.NUMERO_HABITACION;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Habitacion));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, habitacion);

                    if (s.ObtenerHabitacion(writer.ToString()) != null)
                    {
                        habitacion = s.ObtenerHabitacion(writer.ToString());

                        txtNumero.Text = habitacion.NUMERO_HABITACION + "";

                        txtNumero.ReadOnly = true;
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('No puede acceder a esta página');window.location='../Hostal/WebLogin.aspx';</script>");
                }
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

        public Habitacion MiSesionH
        {
            get
            {
                if (Session["Habitacion"] == null)
                {
                    Session["Habitacion"] = new Habitacion();
                }
                return (Habitacion)Session["Habitacion"];
            }
            set
            {
                Session["Habitacion"] = value;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Modelo.Habitacion habitacion = new Modelo.Habitacion();
            habitacion.NUMERO_HABITACION = short.Parse(txtNumero.Text);

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Habitacion));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, habitacion);

            if (s.EliminarHabitacion(writer.ToString()))
            {
                MiSesionH = null;
                Response.Write("<script language='javascript'>window.alert('La habitación ha sido Eliminada con éxito');window.location='../Administrador/WebVerHabitacion.aspx';</script>");
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
            MiSesionH = null;
            Response.Write("<script language='javascript'>window.alert('Ha decidido no eliminar la habitación');window.location='../Hostal/WebLogin.aspx';</script>");
        }
    }
}