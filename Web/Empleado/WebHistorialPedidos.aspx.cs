﻿using Modelo;
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

            try
            {
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

                    string datos = service.ListarPedidoEmpleadoPendiente(writer2.ToString());
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
            catch (Exception)
            {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
            }
            
        }
        protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalle.PageIndex = e.NewPageIndex;
            gvDetalle.DataBind();
        }
        protected void gvPedidoListo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPedidoListo.PageIndex = e.NewPageIndex;
            gvPedidoListo.DataBind();
        }
        protected void gvDetalleHistorial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalleHistorial.PageIndex = e.NewPageIndex;
            gvDetalleHistorial.DataBind();
        }
        protected void gvPedidoPendiente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPedidoPendiente.PageIndex = e.NewPageIndex;
            gvPedidoPendiente.DataBind();
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

        public DetallePedido MiSesionDetalle
        {
            get
            {
                if (Session["DetallePedido"] == null)
                {
                    Session["DetallePedido"] = new Pedido();
                }
                return (DetallePedido)Session["DetallePedido"];
            }
            set
            {
                Session["DetallePedido"] = value;
            }
        }

        public List<DetallePedido> MiSesionD
        {
            get
            {
                if (Session["ListaDetalle"] == null)
                {
                    Session["ListaDetalle"] = new List<DetallePedido>();
                }
                return (List<DetallePedido>)Session["ListaDetalle"];
            }
            set
            {
                Session["ListaDetalle"] = value;
            }
        }

        private void CargarTablaDetalle(List<DetallePedido> lista) {
            Producto producto = new Producto();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID Detalle",typeof(short)),
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

                dt.Rows.Add(item.ID_DETALLE_PEDIDO,producto.ID_PRODUCTO,producto.NOMBRE_PRODUCTO,producto.DESCRIPCION_PRODUCTO,producto.UNIDAD_MEDIDA,item.CANTIDAD);
            }
            

            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
        }

        private void CargarTablaHistorial(List<DetallePedido> lista) {
            Producto producto = new Producto();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID Detalle",typeof(short)),
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

                dt.Rows.Add(item.ID_DETALLE_PEDIDO,producto.ID_PRODUCTO,producto.NOMBRE_PRODUCTO,producto.DESCRIPCION_PRODUCTO,producto.UNIDAD_MEDIDA,item.CANTIDAD);
            }


            gvDetalleHistorial.DataSource = dt;
            gvDetalleHistorial.DataBind();
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
                        CargarTablaHistorial(listaDetalle);

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
                        CargarTablaDetalle(listaDetalle);

                        MiSesionPedido = s.ObtenerPedido(writer.ToString());

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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                MiSesionD = null;

                LinkButton btn = (LinkButton)(sender);
                short ID_DETALLE_PEDIDO = short.Parse(btn.CommandArgument);

                DetallePedido detalle = new DetallePedido();
                detalle.ID_DETALLE_PEDIDO = ID_DETALLE_PEDIDO;

                MiSesionDetalle = detalle;

                Response.Redirect("../Empleado/WebEditarPedido.aspx");
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
                short ID_DETALLE_PEDIDO = short.Parse(btn.CommandArgument);

                DetallePedido detalle = new DetallePedido();
                detalle.ID_DETALLE_PEDIDO = ID_DETALLE_PEDIDO;

                Service1 s = new Service1();
                XmlSerializer sr = new XmlSerializer(typeof(Modelo.DetallePedido));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, detalle);

                if (s.EliminarDetallePedido(writer.ToString()))
                {
                    Pedido pedido = new Pedido();
                    pedido = MiSesionPedido;

                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                    StringWriter writer2 = new StringWriter();
                    sr2.Serialize(writer2, pedido);

                    string datos = s.ListarDetallePedido(writer2.ToString());
                    XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetallePedidoCollection));
                    StringReader reader = new StringReader(datos);

                    Modelo.DetallePedidoCollection listaDetalle = (Modelo.DetallePedidoCollection)ser3.Deserialize(reader);

                    if (listaDetalle.Count == 0)
                    {
                        if (s.ObtenerPedido(writer2.ToString()) == null)
                        {
                            alerta_exito.Visible = false;
                            error.Text = "No se ha encontrado el Pedido";
                            alerta.Visible = true;
                        }
                        else
                        {
                            Pedido pedido2 = s.ObtenerPedido(writer2.ToString());
                            pedido2.ESTADO_PEDIDO = "Rechazado";
                            pedido2.COMENTARIO = "No tiene ningún detalle Pedido asociado";

                            XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.Pedido));
                            StringWriter writer3 = new StringWriter();
                            sr3.Serialize(writer3, pedido2);

                            if (s.EditarEstadoPedido(writer3.ToString()))
                            {
                                Response.Write("<script language='javascript'>window.alert('No tiene ningún detalle pedido asociado a este pedido. Por ende será rechazado');window.location='../Empleado/WebHistorialPedidos.aspx';</script>");
                            }
                            else
                            {
                                alerta_exito.Visible = false;
                                error.Text = "La modificación de Estado Pedido ha fallado";
                                alerta.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>window.alert('Detalle Pedido Eliminado');window.location='../Empleado/WebHistorialPedidos.aspx';</script>");
                    }
                }
            }
            catch(Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                MiSesionD = null;

                Response.Redirect("../Empleado/WebCrearDetalle.aspx");
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void gvDetalleHistorial_RowDataBound(object sender,GridViewRowEventArgs e) {
            e.Row.Cells[0].Visible = false;
        }
    }
}