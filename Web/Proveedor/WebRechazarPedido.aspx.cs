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

namespace Web.Proveedor
{
    public partial class WebRechazarPedido : System.Web.UI.Page
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
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;
            btnLimpiar.CausesValidation = false;
            btnLimpiar.UseSubmitBehavior = false;

            if (!IsPostBack)
            {
                if (MiSesionPedido.NUMERO_PEDIDO != 0)
                {
                    Modelo.Pedido pedido = new Modelo.Pedido();
                    pedido.NUMERO_PEDIDO = MiSesionPedido.NUMERO_PEDIDO;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Pedido));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, pedido);

                    if (s.ObtenerPedido(writer.ToString()) != null)
                    {
                        pedido = s.ObtenerPedido(writer.ToString());
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('No puede acceder a esta página');window.location='../Hostal/WebLogin.aspx';</script>");
                }
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
        public Pedido MiSesionPedido
        {
            get
            {
                if (Session["Pedido"] == null)
                {
                    Session["Pedido"] = new Pedido();
                }
                return (Pedido)Session["Pedido"];
            }
            set
            {
                Session["Pedido"] = value;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtComentario.Text != string.Empty)
                {
                    Modelo.Pedido pedido = new Modelo.Pedido();
                    pedido.NUMERO_PEDIDO = MiSesionPedido.NUMERO_PEDIDO;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Pedido));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, pedido);

                    if (s.ObtenerPedido(writer.ToString()) != null)
                    {
                        pedido = s.ObtenerPedido(writer.ToString());
                        pedido.COMENTARIO = txtComentario.Text;
                        pedido.ESTADO_DESPACHO = "Rechazado";

                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                        StringWriter writer2 = new StringWriter();
                        sr2.Serialize(writer2, pedido);

                        if (s.EditarEstadoPedido(writer2.ToString()))
                        {
                            MiSesionPedido = null;

                            Response.Write("<script language='javascript'>window.alert('Ha Rechazado el pedido');window.location='../Proveedor/WebProveedor.aspx';</script>");
                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "No se ha podido llevar a cabo la operacion";
                            alerta.Visible = true;
                        }
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "Pedido no encontrado";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Antes de rechazar el pedido debe mencionar las razones";
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
            txtComentario.Text = "";
        }
    }
}