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

namespace Web
{
    public partial class AdminM : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            nombre_usuario.Text = MiSesion.NOMBRE_USUARIO;

            Service1 service = new Service1();

            Modelo.Usuario usuario = new Modelo.Usuario();
            usuario = MiSesion;

            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Usuario));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, usuario);

            string datos = service.listaNotificacion(writer.ToString());
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.NotificacionCollection));
            StringReader reader = new StringReader(datos);

            Modelo.NotificacionCollection listaNotificacion = (Modelo.NotificacionCollection)ser.Deserialize(reader);

            MiSesionNotificacion = listaNotificacion;

            for (int i = 0; i < MiSesionNotificacion.Count; i++)
            {

                HyperLink URL = new HyperLink();
                URL.NavigateUrl = MiSesionNotificacion[i].URL;
                URL.Text = MiSesionNotificacion[i].MENSAJE;
                URL.CssClass = "dropdown-item";

                panel.Controls.Add(URL);
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

        //Creación de Sesión
        public List<Notificacion> MiSesionNotificacion
        {
            get
            {
                if (Session["Notificacion"] == null)
                {
                    Session["Notificacion"] = new List<Notificacion>();
                }
                return (List<Notificacion>)Session["Notificacion"];
            }
            set
            {
                Session["Notificacion"] = value;
            }
        }
    }
}