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
    public partial class WebPedidosDespacho : System.Web.UI.Page
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

            Service1 service = new Service1();

            Modelo.Proveedor proveedor = new Modelo.Proveedor();

            if (MiSesion != null)
            {
                if (MiSesion.TIPO_USUARIO.Equals("Proveedor"))
                {
                    proveedor.ID_USUARIO = MiSesion.ID_USUARIO;

                    //Si el ID de empleado es encontrado, pasar al siguiente paso
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Proveedor));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, proveedor);
                    writer.Close();

                    Modelo.Proveedor proveedor2 = s.buscarIDP(writer.ToString());
                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Proveedor));
                    StringWriter writer2 = new StringWriter();
                    sr2.Serialize(writer2, proveedor2);
                    writer2.Close();

                    string datos = service.ListarPedidosporDespachar(writer2.ToString());
                    XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                    StringReader reader = new StringReader(datos);

                    Modelo.PedidoCollection listaPedido = (Modelo.PedidoCollection)ser3.Deserialize(reader);
                    reader.Close();
                    gvPedidoDespacho.DataSource = listaPedido;
                    gvPedidoDespacho.DataBind();

                }
                //Else si es el administrador deberia decir algo supongo
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

        protected void btnInfo2_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short numero_pedido = short.Parse(btn.CommandArgument);

            Pedido pedido = new Pedido();
            pedido.NUMERO_PEDIDO = numero_pedido;

            MiSesionPedido = pedido;

            Response.Redirect("../Proveedor/WebDetallePedido.aspx");
        }
    }
}