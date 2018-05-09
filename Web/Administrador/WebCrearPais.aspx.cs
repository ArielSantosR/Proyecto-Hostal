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
    public partial class WebCrearPais : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;
            btnLimpiar.CausesValidation = false;
            btnLimpiar.UseSubmitBehavior = false;
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            if (txtNombre.Text != string.Empty)
            {
                Modelo.Pais pais = new Modelo.Pais();
                pais.NOMBRE_PAIS = txtNombre.Text;


                Service1 s = new Service1();
                XmlSerializer sr = new XmlSerializer(typeof(Modelo.Pais));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, pais);

                if (s.AgregarPais(writer.ToString()))
                {
                    exito.Text = "El país ha sido agregado con éxito";
                    alerta_exito.Visible = true;
                    alerta.Visible = false;
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "El ingreso ha fallado";
                    alerta.Visible = true;
                }
            }
            else
            {
                alerta_exito.Visible = false;
                error.Text = "Revise los datos ingresados";
                alerta.Visible = true;
            }
        }
    }
}