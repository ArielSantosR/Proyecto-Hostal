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

namespace Web.Empleado
{
    public partial class WebHistorialPedidos : System.Web.UI.Page
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
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;

            Service1 service = new Service1();
            Modelo.Empleado empleado = new Modelo.Empleado();

            if (MiSesion != null)
            {
                empleado.ID_USUARIO = MiSesion.ID_USUARIO;

                Service1 s = new Service1();
                XmlSerializer sr = new XmlSerializer(typeof(Modelo.Empleado));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, empleado);
                writer.Close();

                Modelo.Empleado empleado2 = s.buscarIDE(writer.ToString());
                XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Empleado));
                StringWriter writer2 = new StringWriter();
                sr2.Serialize(writer2, empleado2);
                writer2.Close();

                string datosListo = service.ListarPedidoEmpleadoListo(writer2.ToString());
                XmlSerializer serListo = new XmlSerializer(typeof(Modelo.PedidoCollection));
                StringReader readerListo = new StringReader(datosListo);

                Modelo.PedidoCollection listaPedido2 = (Modelo.PedidoCollection)serListo.Deserialize(readerListo);
                readerListo.Close();
                gvPedidoListo.DataSource = listaPedido2;
                gvPedidoListo.DataBind();

                string datos = service.ListarPedidoEmpleadoPendiente();
                XmlSerializer ser = new XmlSerializer(typeof(Modelo.PedidoCollection));
                StringReader reader = new StringReader(datos);
                Modelo.PedidoCollection listaPedido = (Modelo.PedidoCollection)ser.Deserialize(reader);
                reader.Close();
                gvPedidoPendiente.DataSource = listaPedido;
                gvPedidoPendiente.DataBind();
            }
            else
            {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
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

            Response.Redirect("../Empleado/WebDetallePedido.aspx");
        }
    }
}