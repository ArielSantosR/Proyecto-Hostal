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

namespace Web.Cliente
{
    public partial class WebVerHistorial : System.Web.UI.Page
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
                    else if (MiSesion.TIPO_USUARIO.Equals("Cliente") &&
                    MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        MasterPageFile = "~/Cliente/ClienteM.Master";
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

            Modelo.Cliente cliente = new Modelo.Cliente();

            try
            {
                if (MiSesion != null)
                {
                    if (MiSesion.TIPO_USUARIO.Equals("Cliente"))
                    {
                        cliente.ID_USUARIO = MiSesion.ID_USUARIO;

                        //Si el ID de empleado es encontrado, pasar al siguiente paso
                        Service1 s = new Service1();
                        XmlSerializer sr = new XmlSerializer(typeof(Modelo.Cliente));
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, cliente);
                        writer.Close();

                        Modelo.Cliente cliente2 = s.buscarIDC(writer.ToString());
                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Cliente));
                        StringWriter writer2 = new StringWriter();
                        sr2.Serialize(writer2, cliente2);
                        writer2.Close();

                        string datos = service.HistorialReserva(writer2.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.OrdenCompraCollection listaPedido = (Modelo.OrdenCompraCollection)ser3.Deserialize(reader);
                        reader.Close();
                        CargarGridOrdenes(listaPedido);

                        string datos2 = service.HistorialReservaPendiente(writer2.ToString());
                        XmlSerializer ser4 = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
                        StringReader reader2 = new StringReader(datos2);

                        Modelo.OrdenCompraCollection listaPedido2 = (Modelo.OrdenCompraCollection)ser4.Deserialize(reader2);
                        reader.Close();
                        CargarGridPendientes(listaPedido2);

                        string datos3 = service.HistorialReservaAsignado(writer2.ToString());
                        XmlSerializer ser5 = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
                        StringReader reader3 = new StringReader(datos3);

                        Modelo.OrdenCompraCollection listaPedido3 = (Modelo.OrdenCompraCollection)ser5.Deserialize(reader3);
                        reader.Close();
                        CargarGridAsignado(listaPedido3);
                    }else {
                        divClientes.Visible = true;
                        List<Modelo.Cliente> clientes = ClienteCollection.ListaClientes();
                        if (!IsPostBack) {
                            ddlClientes.DataSource = clientes;
                            ddlClientes.DataTextField = "NOMBRE_CLIENTE";
                            ddlClientes.DataValueField = "RUT_CLIENTE";
                            ddlClientes.DataBind();
                            ddlClientes.Items.Insert(0,new ListItem("Seleccione una Empresa...","0"));
                        }
                    }
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

        private void CargarGridOrdenes (List<OrdenCompra> ordenes) {
            Modelo.Cliente cliente;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("NUMERO_ORDEN", typeof(int)),
                new DataColumn("CANTIDAD_HUESPEDES", typeof(int)),
                new DataColumn("FechaLlegada",typeof(string)),
                new DataColumn("FechaSalida",typeof(string)),
                new DataColumn("NOMBRE_CLIENTE",typeof(string)),
                new DataColumn("ESTADO_ORDEN",typeof(string)),
                new DataColumn("COMENTARIO",typeof(string)),
                new DataColumn("MONTO_TOTAL",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (OrdenCompra o in ordenes) {
                cliente = new Modelo.Cliente();
                cliente.RUT_CLIENTE = o.RUT_CLIENTE;
                cliente.BuscarCliente();

                dt.Rows.Add(o.NUMERO_ORDEN,o.CANTIDAD_HUESPEDES,o.FECHA_LLEGADA.ToShortDateString(),o.FECHA_SALIDA.ToShortDateString(),cliente.NOMBRE_CLIENTE,o.ESTADO_ORDEN,o.COMENTARIO,"$" + o.MONTO_TOTAL);
            }

            //Carga de GriedView
            gvOrden.DataSource = dt;
            gvOrden.DataBind();
            if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString())) {
                gvOrden.Columns[4].Visible = true;
            }
        }

        private void CargarGridPendientes (List<OrdenCompra> ordenes) {
            Modelo.Cliente cliente;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("NUMERO_ORDEN", typeof(int)),
                new DataColumn("CANTIDAD_HUESPEDES", typeof(int)),
                new DataColumn("FechaLlegada",typeof(string)),
                new DataColumn("FechaSalida",typeof(string)),
                new DataColumn("NOMBRE_CLIENTE",typeof(string)),
                new DataColumn("ESTADO_ORDEN",typeof(string)),
                new DataColumn("COMENTARIO",typeof(string)),
                new DataColumn("MONTO_TOTAL",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (OrdenCompra o in ordenes) {
                cliente = new Modelo.Cliente();
                cliente.RUT_CLIENTE = o.RUT_CLIENTE;
                cliente.BuscarCliente();

                dt.Rows.Add(o.NUMERO_ORDEN,o.CANTIDAD_HUESPEDES,o.FECHA_LLEGADA.ToShortDateString(),o.FECHA_SALIDA.ToShortDateString(),cliente.NOMBRE_CLIENTE,o.ESTADO_ORDEN,o.COMENTARIO,"$" + o.MONTO_TOTAL);
            }

            //Carga de GriedView
            gvOrdenPendiente.DataSource = dt;
            gvOrdenPendiente.DataBind();
            if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString())) {
                gvOrdenPendiente.Columns[4].Visible = true;
            }
        }

        private void CargarGridAsignado (List<OrdenCompra> ordenes) {
            Modelo.Cliente cliente;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("NUMERO_ORDEN", typeof(int)),
                new DataColumn("CANTIDAD_HUESPEDES", typeof(int)),
                new DataColumn("FechaLlegada",typeof(string)),
                new DataColumn("FechaSalida",typeof(string)),
                new DataColumn("NOMBRE_CLIENTE",typeof(string)),
                new DataColumn("RUT_CLIENTE",typeof(int)),
                new DataColumn("ESTADO_ORDEN",typeof(string)),
                new DataColumn("COMENTARIO",typeof(string)),
                new DataColumn("MONTO_TOTAL",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (OrdenCompra o in ordenes) {
                cliente = new Modelo.Cliente();
                cliente.RUT_CLIENTE = o.RUT_CLIENTE;
                cliente.BuscarCliente();

                dt.Rows.Add(o.NUMERO_ORDEN,o.CANTIDAD_HUESPEDES,o.FECHA_LLEGADA.ToShortDateString(),o.FECHA_SALIDA.ToShortDateString(),cliente.NOMBRE_CLIENTE,o.RUT_CLIENTE,o.ESTADO_ORDEN,o.COMENTARIO,"$" + o.MONTO_TOTAL);
            }

            //Carga de GriedView
            gvOrdenAsignada.DataSource = dt;
            gvOrdenAsignada.DataBind();
            if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString())) {
                gvOrdenPendiente.Columns[4].Visible = true;
            }
        }

        private void CargarGridDetalle1 (List<DetalleOrden> detalle) {
            Huesped huesped;
            Pension pension;
            CategoriaHabitacion cat;
            TipoHabitacion tipo;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("NOMBRE_HUESPED", typeof(string)),
                new DataColumn("HABITACION",typeof(string)),
                new DataColumn("PENSION",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (Modelo.DetalleOrden d in detalle) {
                huesped = new Huesped();
                huesped.RUT_HUESPED = d.RUT_HUESPED;
                huesped.BuscarHuesped();

                pension = new Pension();
                pension.ID_PENSION = d.ID_PENSION;
                pension.BuscarPension();

                cat = new CategoriaHabitacion();
                cat.ID_CATEGORIA_HABITACION = d.ID_CATEGORIA_HABITACION;
                cat.BuscarCategoria();

                tipo = new TipoHabitacion();
                tipo.ID_TIPO_HABITACION = d.ID_TIPO_HABITACION;
                tipo.BuscarTipo();

                dt.Rows.Add(huesped.PNOMBRE_HUESPED + " " + huesped.APP_PATERNO_HUESPED + " " + huesped.APP_MATERNO_HUESPED,pension.NOMBRE_PENSION,tipo.NOMBRE_TIPO_HABITACION + "-" + cat.NOMBRE_CATEGORIA);
            }

            //Carga de GriedView
            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
        }

        private void CargarGridDetalle2 (List<DetalleHabitacion> detalle) {
            Huesped huesped;
            Pension pension;
            Habitacion hab;
            CategoriaHabitacion cat;
            TipoHabitacion tipo;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("NUMERO_HABITACION", typeof(string)),
                new DataColumn("HUESPED", typeof(string)),
                new DataColumn("PENSION",typeof(string)),
                new DataColumn("HABITACION",typeof(string)),
                new DataColumn("CATEGORIA_HAB",typeof(string)),
                new DataColumn("FechaLlegada",typeof(string)),
                new DataColumn("FechaSalida",typeof(int))
            });

            //Carga de datos en DataTable
            foreach (DetalleHabitacion d in detalle) {
                huesped = new Huesped();
                huesped.RUT_HUESPED = d.RUT_HUESPED;
                huesped.BuscarHuesped();
                
                pension = new Pension();
                pension.ID_PENSION = d.ID_PENSION;
                pension.BuscarPension();

                hab = new Habitacion();
                hab.NUMERO_HABITACION = d.NUMERO_HABITACION;
                hab.BuscarHabitacion();

                cat = new CategoriaHabitacion();
                cat.ID_CATEGORIA_HABITACION = hab.ID_CATEGORIA_HABITACION;
                cat.BuscarCategoria();

                tipo = new TipoHabitacion();
                tipo.ID_TIPO_HABITACION = hab.ID_TIPO_HABITACION;
                tipo.BuscarTipo();

                dt.Rows.Add(d.NUMERO_HABITACION,huesped.PNOMBRE_HUESPED + " " + huesped.APP_PATERNO_HUESPED + " " + huesped.APP_MATERNO_HUESPED,pension.NOMBRE_PENSION,tipo.NOMBRE_TIPO_HABITACION,cat.NOMBRE_CATEGORIA,d.FECHA_LLEGADA.ToShortDateString(),d.FECHA_SALIDA.ToShortDateString());
            }

            //Carga de GriedView
            gvDetalle2.DataSource = dt;
            gvDetalle2.DataBind();
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

        public Modelo.Cliente MiSesionCl
        {
            get
            {
                if (Session["Cliente"] == null)
                {
                    Session["Cliente"] = new Modelo.Cliente();
                }
                return (Modelo.Cliente)Session["Cliente"];
            }
            set
            {
                Session["Cliente"] = value;
            }
        }

        public OrdenCompra MiSesionOrden
        {
            get
            {
                if (Session["OrdenCompra"] == null)
                {
                    Session["OrdenCompra"] = new OrdenCompra();
                }
                return (OrdenCompra)Session["OrdenCompra"];
            }
            set
            {
                Session["OrdenCompra"] = value;
            }
        }

        protected void btnInfo2_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                short numero_orden = short.Parse(btn.CommandArgument);

                OrdenCompra orden = new OrdenCompra();
                orden.NUMERO_ORDEN = numero_orden;

                MiSesionOrden = orden;

                if (MiSesionOrden.NUMERO_ORDEN != 0)
                {
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.OrdenCompra));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, orden);

                    if (s.ListarDetalleReserva(writer.ToString()) != null)
                    {
                        string datos = s.ListarDetalleReserva(writer.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetalleOrdenCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.DetalleOrdenCollection listaDetalle = (Modelo.DetalleOrdenCollection)ser3.Deserialize(reader);
                        reader.Close();
                        CargarGridDetalle1(listaDetalle);

                        MiSesionOrden = s.ObtenerReserva(writer.ToString());
                        exampleModalLabel.InnerText = "Detalle Reserva N°" + MiSesionOrden.NUMERO_ORDEN;
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                short numero_orden = short.Parse(btn.CommandArgument);

                OrdenCompra orden = new OrdenCompra();
                orden.NUMERO_ORDEN = numero_orden;

                MiSesionOrden = orden;

                if (MiSesionOrden.NUMERO_ORDEN != 0)
                {
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.OrdenCompra));
                    StringWriter writer = new StringWriter();

                    orden.ESTADO_ORDEN = "Cancelado";
                    orden.COMENTARIO = "Cancelado por el Cliente";
                    sr.Serialize(writer, orden);

                    if (s.EditarEstadoReserva(writer.ToString()))
                    {
                        Response.Write("<script language='javascript'>window.alert('Reserva Cancelada');window.location='../Cliente/WebVerHistorial.aspx';</script>");
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "No se pudo cancelar la orden";
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

        protected void btnInfo3_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                int rut_cliente = int.Parse(btn.CommandArgument);

                Modelo.Cliente cliente = new Modelo.Cliente();
                cliente.RUT_CLIENTE = rut_cliente;

                MiSesionCl = cliente;

                if (MiSesionCl.RUT_CLIENTE != 0)
                {
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Cliente));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, cliente);

                    if (s.ListaDetalleHabitacionCliente(writer.ToString()) != null)
                    {
                        string datos = s.ListaDetalleHabitacionCliente(writer.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetalleHabitacionCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.DetalleHabitacionCollection listaDetalle = (Modelo.DetalleHabitacionCollection)ser3.Deserialize(reader);
                        reader.Close();
                        CargarGridDetalle2(listaDetalle);
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

        protected void ddlClientes_SelectedIndexChanged (object sender,EventArgs e) {
            try {
                if (ddlClientes.SelectedIndex != 0) {
                    List<OrdenCompra> ordenes = OrdenCompraCollection.ListarOrdenes().Where(x => x.RUT_CLIENTE == int.Parse(ddlClientes.SelectedValue)).ToList();
                    CargarGridOrdenes(ordenes);

                    var listaP = ordenes.Where(x => x.ESTADO_ORDEN.Equals(Estado_Orden.Pendiente.ToString())).ToList();
                    CargarGridPendientes(listaP);

                    var listaA = ordenes.Where(x => x.ESTADO_ORDEN.Equals(Estado_Orden.Asignado.ToString())).ToList();
                    CargarGridAsignado(listaA);

                } else {
                    alerta_exito.Visible = false;
                    error.Text = "Debe seleccionar una empresa válida";
                    alerta.Visible = true;
                }
            } catch (Exception ex) {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }
    }
}