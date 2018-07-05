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

namespace Web.Proveedor
{
    public partial class WebDespachados : System.Web.UI.Page
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

                        string datos = service.ListarHistorialProveedor(writer2.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.PedidoCollection listaPedido = (Modelo.PedidoCollection)ser3.Deserialize(reader);
                        reader.Close();
                        gvPedidoListo.DataSource = listaPedido;
                        gvPedidoListo.DataBind();


                        string datos2 = service.ListarPedidoDespacho(writer2.ToString());
                        XmlSerializer ser4 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                        StringReader reader2 = new StringReader(datos2);

                        Modelo.PedidoCollection listaPedido2 = (Modelo.PedidoCollection)ser4.Deserialize(reader2);
                        reader.Close();
                        gvPedidoDespacho.DataSource = listaPedido2;
                        gvPedidoDespacho.DataBind();
                    }
                    //Else si es el administrador deberia decir algo supongo
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

        protected void btnInfo_Click(object sender, EventArgs e)
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
                    pedido2.ESTADO_DESPACHO = "Despachado";

                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                    StringWriter writer2 = new StringWriter();
                    sr2.Serialize(writer2, pedido2);

                    if (s.EditarEstadoPedido(writer2.ToString()))
                    {
                        Response.Write("<script language='javascript'>window.alert('Despacho Aceptado. Por favor realice el despacho con la orden correspondiente');window.location='../Proveedor/WebHistorialPedidos.aspx';</script>");
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

        protected void btnInfo2_Click(object sender, EventArgs e)
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
                        CargarGrid(listaDetalle);

                        MiSesionPedido = s.ObtenerPedido(writer.ToString());

                        exampleModalLabel.InnerText = "Detalle Pedido N°" + MiSesionPedido.NUMERO_PEDIDO;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal').modal();", true);
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

        private void CargarGrid (DetallePedidoCollection listaDetalle) {
            Producto producto = new Producto();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("Código", typeof(long)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Descripción",typeof(string)),
                new DataColumn("Unidad Medida",typeof(string)),
                new DataColumn("Cantidad",typeof(int))
            });

            foreach (DetallePedido item in listaDetalle) {
                producto = new Producto();
                producto.ID_PRODUCTO = item.ID_PRODUCTO;
                producto.Read();

                dt.Rows.Add(producto.ID_PRODUCTO,producto.NOMBRE_PRODUCTO,producto.DESCRIPCION_PRODUCTO,producto.UNIDAD_MEDIDA,item.CANTIDAD);
            }

            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
        }
    }
}