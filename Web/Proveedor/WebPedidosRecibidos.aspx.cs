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
    public partial class WebRecibidos : System.Web.UI.Page
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
            try
            {
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

                        string datos = service.ListarPedidoProveedor(writer2.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.PedidoCollection listaPedido = (Modelo.PedidoCollection)ser3.Deserialize(reader);
                        reader.Close();
                        gvPedido.DataSource = listaPedido;
                        gvPedido.DataBind();
                    }
                    //Else Administrador 
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
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
                    pedido2.ESTADO_DESPACHO = "Aceptado";

                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                    StringWriter writer2 = new StringWriter();
                    sr2.Serialize(writer2, pedido2);

                    if (s.EditarEstadoPedido(writer2.ToString()))
                    {
                        MiSesionPedido = null;

                        Response.Write("<script language='javascript'>window.alert('Ha aceptado el pedido. Para despacharlo debe ir a Historial');window.location='../Proveedor/WebPedidosRecibidos.aspx';</script>");
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "La modificación de Estado Pedido ha fallado";
                        alerta.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                short numero_pedido = short.Parse(btn.CommandArgument);

                Pedido pedido = new Pedido();
                pedido.NUMERO_PEDIDO = numero_pedido;

                MiSesionPedido = pedido;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal').modal();", true);
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnInfo_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                short numero_pedido = short.Parse(btn.CommandArgument);

                Pedido pedido = new Pedido();
                pedido.NUMERO_PEDIDO = numero_pedido;

                MiSesionPedido = pedido;

                if (MiSesionPedido.NUMERO_PEDIDO != 0)
                {

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Pedido));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, pedido);

                    if (s.ListarDetallePedido(writer.ToString()) != null)
                    {
                        string datos = s.ListarDetallePedido(writer.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetallePedidoCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.DetallePedidoCollection listaDetalle = (Modelo.DetallePedidoCollection)ser3.Deserialize(reader);
                        reader.Close();
                        gvDetalle.DataSource = listaDetalle;
                        gvDetalle.DataBind();

                        MiSesionPedido = s.ObtenerPedido(writer.ToString());

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
                    }
                }

            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnModal_Click(object sender, EventArgs e)
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

                            Response.Write("<script language='javascript'>window.alert('Ha Rechazado el pedido');window.location='../Proveedor/WebPedidosRecibidos.aspx';</script>");
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
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }
    }
}