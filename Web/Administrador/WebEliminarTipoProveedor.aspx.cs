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
    public partial class WebEliminarTipoProveedor : System.Web.UI.Page
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

        public TipoProveedor MiSesionProveedor
        {
            get
            {
                if (Session["Proveedor"] == null)
                {
                    Session["Proveedor"] = new TipoProveedor();
                }
                return (TipoProveedor)Session["Proveedor"];
            }
            set
            {
                Session["Proveedor"] = value;
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MiSesionProveedor = null;
            Response.Write("<script language='javascript'>window.alert('Ha decidido no eliminar el Tipo de Plato');window.location='../Administrador/WebVerPlatos.aspx';</script>");
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Modelo.TipoProveedor tipoProveedor = new Modelo.TipoProveedor();
            tipoProveedor.ID_TIPO_PROVEEDOR = MiSesionProveedor.ID_TIPO_PROVEEDOR;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.TipoProveedor));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, tipoProveedor);

            if (s.EliminarTipoProveedor(writer.ToString()))
            {
                MiSesionProveedor = null;
                Response.Write("<script language='javascript'>window.alert('El Tipo de Proveedor ha sido Eliminado con éxito');window.location='../Administrador/WebVerTipoProveedor.aspx';</script>");
                alerta.Visible = false;
            }
            else
            {
                error.Text = "No se ha podido Eliminar";
                alerta.Visible = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            error.Text = "";
            alerta.Visible = false;

            if (!IsPostBack)
            {
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

                        txtNombre.Text = tipoProveedor.NOMBRE_TIPO + "";

                        txtNombre.ReadOnly = true;


                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('No puede acceder a esta página');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
        }
    }
  }
