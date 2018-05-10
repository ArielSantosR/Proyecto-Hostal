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
    public partial class WebVerPedido : System.Web.UI.Page
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

            Service1 service = new Service1();
            string datos = service.ListarPedidoAdmin();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PedidoCollection));
            StringReader reader = new StringReader(datos);

            Modelo.PedidoCollection listaPedido = (Modelo.PedidoCollection)ser.Deserialize(reader);
            reader.Close();
            gvPedido.DataSource = listaPedido;
            gvPedido.DataBind();
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short numero_pedido = short.Parse(btn.CommandArgument);

            Pedido pedido = new Pedido();
            pedido.NUMERO_PEDIDO = numero_pedido;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Pedido));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, pedido);

            if (s.ObtenerPedido(writer.ToString()) == null)
            {
                alerta_exito.Visible = false;
                error.Text = "No se ha encontrado el Pedido";
                alerta.Visible = true;
            }
            else
            {
                Pedido pedido2 = s.ObtenerPedido(writer.ToString());
                pedido2.ESTADO_PEDIDO = "Aceptado";

                XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                StringWriter writer2 = new StringWriter();
                sr.Serialize(writer2, pedido2);

                if (s.EditarEstadoPedido(writer2.ToString()))
                {
                    exito.Text = "El Pedido ha sido Aceptado";
                    alerta_exito.Visible = true;
                    alerta.Visible = false;

                    string datos = s.ListarPedidoAdmin();
                    XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                    StringReader reader2 = new StringReader(datos);

                    Modelo.PedidoCollection listaPedido = (Modelo.PedidoCollection)ser2.Deserialize(reader2);
                    reader2.Close();
                    gvPedido.DataSource = listaPedido;
                    gvPedido.DataBind();
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "La modificación de Estado Pedido ha fallado";
                    alerta.Visible = true;
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short numero_pedido = short.Parse(btn.CommandArgument);

            Pedido pedido = new Pedido();
            pedido.NUMERO_PEDIDO = numero_pedido;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Pedido));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, pedido);

            if (s.ObtenerPedido(writer.ToString()) == null)
            {
                alerta_exito.Visible = false;
                error.Text = "No se ha encontrado el Pedido";
                alerta.Visible = true;
            }
            else
            {
                Pedido pedido2 = s.ObtenerPedido(writer.ToString());
                pedido2.ESTADO_PEDIDO = "Rechazado";

                XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                StringWriter writer2 = new StringWriter();
                sr.Serialize(writer2, pedido2);

                if (s.EditarEstadoPedido(writer2.ToString()))
                {
                    exito.Text = "El Pedido ha sido Rechazado";
                    alerta_exito.Visible = true;
                    alerta.Visible = false;

                    string datos = s.ListarPedidoAdmin();
                    XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                    StringReader reader2 = new StringReader(datos);

                    Modelo.PedidoCollection listaPedido = (Modelo.PedidoCollection)ser2.Deserialize(reader2);
                    reader2.Close();
                    gvPedido.DataSource = listaPedido;
                    gvPedido.DataBind();
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "La modificación de Estado Pedido ha fallado";
                    alerta.Visible = true;
                }
            }
        }
    }
}