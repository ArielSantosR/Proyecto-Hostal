using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
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
        protected void gvPedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPedido.PageIndex = e.NewPageIndex;
            gvPedido.DataBind();
        }
        protected void gvDetalleHistorial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalleHistorial.PageIndex = e.NewPageIndex;
            gvDetalleHistorial.DataBind();
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
                    Response.Write("<script language='javascript'>window.alert('Ha Aceptado el pedido');window.location='../Administrador/WebVerPedido.aspx';</script>");
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
            LinkButton btn = (LinkButton)(sender);
            short numero_pedido = short.Parse(btn.CommandArgument);

            Pedido pedido = new Pedido();
            pedido.NUMERO_PEDIDO = numero_pedido;

            MiSesionPedido = pedido;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal').modal();", true);
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
                        pedido.ESTADO_PEDIDO = "No Recepcionado";

                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                        StringWriter writer2 = new StringWriter();
                        sr2.Serialize(writer2, pedido);

                        if (s.EditarEstadoPedido(writer2.ToString()))
                        {
                            MiSesionPedido = null;

                            Response.Write("<script language='javascript'>window.alert('Ha Rechazado el pedido');window.location='../Administrador/WebVerPedido.aspx';</script>");

                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "No se ha podido modificar el Estado de Producto";
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

        protected void btnInfo_Click(object sender, EventArgs e)
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
                    CargarTablaHistorial(listaDetalle);
                }
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
        }

        private void CargarTablaHistorial(List<DetallePedido> lista) {
            Producto producto = new Producto();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("Código", typeof(long)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Descripción",typeof(string)),
                new DataColumn("Unidad Medida",typeof(string)),
                new DataColumn("Cantidad Pedida",typeof(int))
            });
            foreach (DetallePedido item in lista) {
                producto = new Producto();
                producto.ID_PRODUCTO = item.ID_PRODUCTO;
                producto.Read();

                dt.Rows.Add(producto.ID_PRODUCTO,producto.NOMBRE_PRODUCTO,producto.DESCRIPCION_PRODUCTO,producto.UNIDAD_MEDIDA,item.CANTIDAD);
            }


            gvDetalleHistorial.DataSource = dt;
            gvDetalleHistorial.DataBind();
        }
    }
}