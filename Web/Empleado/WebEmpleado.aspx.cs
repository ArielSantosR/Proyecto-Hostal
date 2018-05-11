﻿using Modelo;
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
    public partial class WebEmpleado : System.Web.UI.Page
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
            alerta.Visible = false;

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

            foreach (Notificacion n in MiSesionNotificacion)
            {
                alerta.Visible = true;
                notificacion.Text = n.MENSAJE;
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

        protected void btnAsignarHabitacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebAsignarHabitacion.aspx");
        }

        protected void btnCrearPedido_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebCrearPedido.aspx");
        }

        protected void btnRecepcionarPedido_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebRecibirPedido.aspx");
        }

        protected void btnCrearFactura_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebCrearFactura.aspx");
        }

        protected void btnVerFacturas_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebVerFacturas.aspx");
        }

        protected void btnMinuta_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebCrearMinuta.aspx");
        }

        protected void btnRegistrarOrden_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebRegistrarOrden.aspx");
        }

        protected void btnHistorial_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebHistorialPedidos.aspx");
        }
    }
}