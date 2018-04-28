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
    public partial class WebHabitaciones : System.Web.UI.Page
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
            string datos = service.ListarHabitacion();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.HabitacionCollection));
            StringReader reader = new StringReader(datos);

            Modelo.HabitacionCollection listaHabitacion = (Modelo.HabitacionCollection)ser.Deserialize(reader);
            reader.Close();
            gvHabitacion.DataSource = listaHabitacion;
            gvHabitacion.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short NUMERO_HABITACION = short.Parse(btn.CommandArgument);

            Habitacion habitacion = new Habitacion();
            habitacion.NUMERO_HABITACION = NUMERO_HABITACION;
            MiSesionH = habitacion;

            Response.Redirect("../Administrador/WebEditarHabitacion.aspx");
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
    }
}