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
    public partial class WebEditarTipoPlato : System.Web.UI.Page
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
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;
            btnLimpiar.CausesValidation = false;
            btnLimpiar.UseSubmitBehavior = false;
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

                    txtNombre.Text = tipoplato.NOMBRE_TIPO_PLATO;

                }
            }
            else
            {
                Response.Write("<script language='javascript'>window.alert('No puede acceder a esta página');window.location='../Hostal/WebLogin.aspx';</script>");
            }
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtNombre.Text != string.Empty)
                {

                    Modelo.TipoPlato tipoplato = new Modelo.TipoPlato();
                    tipoplato.ID_TIPO_PLATO = MiSesionPlato.ID_TIPO_PLATO;
                    tipoplato.NOMBRE_TIPO_PLATO = txtNombre.Text;


                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.TipoPlato));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, tipoplato);

                    if (s.ModificarTipoPlato(writer.ToString()))
                    {
                        exito.Text = "Tipo Plato modificado con éxito";
                        alerta_exito.Visible = true;
                        alerta.Visible = false;
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "No se ha podido modificar";
                        alerta.Visible = true;
                    }

                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Datos Ingresados incorrectamente, verifique que ha ingresado números correctamente";
                    alerta.Visible = true;
                }  
            }
            catch (Exception)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepcion";
                alerta.Visible = true;
            }
          }
        

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            
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
                    Session["Plato"] = new Plato();
                }
                return (TipoPlato)Session["Plato"];
            }
            set
            {
                Session["Plato"] = value;
            }
        }
    }
}