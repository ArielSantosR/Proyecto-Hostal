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
    public partial class WebProveedor : System.Web.UI.Page
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
                    else if (MiSesion.TIPO_USUARIO.Equals("Proveedor") &&
                    MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        MasterPageFile = "~/Proveedor/ProveedorM.Master";
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

        protected void btnRecibidos_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Proveedor/WebHistorialPedidos.aspx");
        }

        protected void btnPendientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Proveedor/WebPedidosRecibidos.aspx");
        }

        protected void btnDespacho_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Proveedor/WebPedidosDespacho.aspx");
        }
    }
}