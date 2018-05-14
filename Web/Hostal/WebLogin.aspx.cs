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
    public partial class WebLogin2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            error.Text = "";
            alerta.Visible = false;

            if (MiSesion != null)
            {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null)
                {
                    if (MiSesion.TIPO_USUARIO.Equals("Administrador") && MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        Response.Redirect("../Administrador/WebAdmin.aspx");
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals("Empleado") && MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        Response.Redirect("../Empleado/WebEmpleado.aspx");
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals("Proveedor") && MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        Response.Redirect("../Proveedor/WebProveedor.aspx");
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals("Cliente") && MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        Response.Redirect("../Cliente/WebCliente.aspx");
                    }
                }
            }
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                if (Login1.UserName != string.Empty && Login1.Password != string.Empty)
                {
                    Modelo.Usuario u = new Modelo.Usuario();
                    u.NOMBRE_USUARIO = Login1.UserName;
                    u.PASSWORD = Login1.Password;
                    Service1 s = new Service1();
                    //Envía los datos de Usuario(Modelo) como un string
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Usuario));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, u);

                    if (s.Login(writer.ToString()))
                    {
                        Modelo.Usuario sesionUsuario = s.GetUsuario(writer.ToString());
                        MiSesion = sesionUsuario;

                        if (MiSesion.TIPO_USUARIO.Equals("Administrador") && MiSesion.ESTADO.Equals("Habilitado"))
                        {
                            Response.Redirect("../Administrador/WebAdmin.aspx");
                        }
                        else if (MiSesion.TIPO_USUARIO.Equals("Empleado") && MiSesion.ESTADO.Equals("Habilitado"))
                        {
                            Response.Redirect("../Empleado/WebEmpleado.aspx");
                        }
                        else if (MiSesion.TIPO_USUARIO.Equals("Proveedor") && MiSesion.ESTADO.Equals("Habilitado"))
                        {
                            Response.Redirect("../Proveedor/WebProveedor.aspx");
                        }
                        else if (MiSesion.TIPO_USUARIO.Equals("Cliente") && MiSesion.ESTADO.Equals("Habilitado"))
                        {
                            Response.Redirect("../Cliente/WebCliente.aspx");
                        }
                        else
                        {
                            Response.Write("<script language='javascript'>window.alert('Posee Permisos incorrectos o su cuenta ha sido Deshabilitada. Contáctese con un Administrador para más Información');window.location='../Hostal/WebLogin.aspx';</script>");
                        }
                    }
                    else
                    {
                        error.Text = "Su Nombre de Usuario o Contraseña es Incorrecta. Vuelva a intentarlo";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    error.Text = "Debe ingresar su Usuario y Contraseña";
                    alerta.Visible = true;
                }
            }
            catch (Exception ex)
            {
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
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

    }
}