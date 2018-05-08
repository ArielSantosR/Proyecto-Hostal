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
    public partial class WebEditarTipoProveedor : System.Web.UI.Page
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
            if (MiSesionProveedor.ID_TIPO_PROVEEDOR != 0)
            {
                Modelo.TipoProveedor tipoProveedor = new Modelo.TipoProveedor();
                tipoProveedor.ID_TIPO_PROVEEDOR = MiSesionProveedor.ID_TIPO_PROVEEDOR;

                Service1 s = new Service1();
                XmlSerializer sr = new XmlSerializer(typeof(Modelo.TipoProveedor));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, tipoProveedor);

                if (s.ObtenerTipoProveedor(writer.ToString()) != null)
                {
                    tipoProveedor = s.ObtenerTipoProveedor(writer.ToString());

                    txtNombre.Text = tipoProveedor.NOMBRE_TIPO;

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

                    Modelo.TipoProveedor tipoProveedor = new Modelo.TipoProveedor();
                    tipoProveedor.ID_TIPO_PROVEEDOR = MiSesionProveedor.ID_TIPO_PROVEEDOR;
                    tipoProveedor.NOMBRE_TIPO = txtNombre.Text;


                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.TipoProveedor));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, tipoProveedor);

                    if (s.ModificarTipoProveedor(writer.ToString()))
                    {
                        exito.Text = "Tipo Proveedor modificado con éxito";
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

        public TipoProveedor MiSesionProveedor
        {
            get
            {
                if (Session["Proveedor"] == null)
                {
                    Session["Proveedor"] = new Plato();
                }
                return (TipoProveedor)Session["Proveedor"];
            }
            set
            {
                Session["Proveedor"] = value;
            }
        }
    }
}