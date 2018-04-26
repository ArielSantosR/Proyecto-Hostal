using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class WebAdmin1 : System.Web.UI.Page
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

        protected void btnCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Cliente/WebCliente.aspx");
        }

        protected void btnEmpleado_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Empleado/WebEmpleado.aspx");
        }

        protected void btnProveedor_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Proveedor/WebProveedor.aspx");
        }

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebRegistroAdmin.aspx");
        }

        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebEditarUsuario.aspx");
        }

        protected void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebVerUsuarios.aspx");
        }

        protected void btnVerHabitaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebVerHabitacion.aspx");
        }

        protected void btnCrearHabitacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebCrearHabitacion.aspx");
        }

        protected void btnModificarHabitacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebModificarHabitacion.aspx");
        }

        protected void btnEliminarHabitacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebEliminarHabitacion.aspx");
        }
    }
}