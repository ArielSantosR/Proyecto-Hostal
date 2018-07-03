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

namespace Web.Empleado
{
    public partial class WebAsignarHabitacion : System.Web.UI.Page
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
            try
            {
                if (MiSesion != null)
                {
                    error.Text = "";
                    exito.Text = "";
                    alerta_exito.Visible = false;
                    alerta.Visible = false;

                    Service1 s = new Service1();
                    string datos = s.ListarReservaAceptada();
                    XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
                    StringReader reader = new StringReader(datos);

                    Modelo.OrdenCompraCollection listaOrden = (Modelo.OrdenCompraCollection)ser.Deserialize(reader);
                    reader.Close();
                    CargarOrden(listaOrden);
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

        private void CargarOrden (OrdenCompraCollection listaOrden) {
            Modelo.Cliente cliente;
            Modelo.Empleado emp;
            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("NUMERO_ORDEN", typeof(int)),
                new DataColumn("CANTIDAD_HUESPEDES", typeof(int)),
                new DataColumn("FechaLlegada",typeof(string)),
                new DataColumn("FechaSalida",typeof(string)),
                new DataColumn("EMPLEADO",typeof(string)),
                new DataColumn("CLIENTE",typeof(string)),
                new DataColumn("COMENTARIO",typeof(string)),
                new DataColumn("MONTO_TOTAL",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (OrdenCompra o in listaOrden) {
                cliente = new Modelo.Cliente();
                cliente.RUT_CLIENTE = o.RUT_CLIENTE;
                cliente.BuscarCliente();

                if (o.RUT_EMPLEADO.HasValue) {
                    emp = new Modelo.Empleado();
                    emp.RUT_EMPLEADO = o.RUT_EMPLEADO.Value;
                    emp.BuscarEmpleado();
                    dt.Rows.Add(o.NUMERO_ORDEN,o.CANTIDAD_HUESPEDES,o.FECHA_LLEGADA.ToShortDateString(),o.FECHA_SALIDA.ToShortDateString(),emp.PNOMBRE_EMPLEADO + " " + emp.APP_PATERNO_EMPLEADO + " " + emp.APP_MATERNO_EMPLEADO,cliente.NOMBRE_CLIENTE,o.COMENTARIO,"$" + o.MONTO_TOTAL);
                }else {
                    dt.Rows.Add(o.NUMERO_ORDEN,o.CANTIDAD_HUESPEDES,o.FECHA_LLEGADA.ToShortDateString(),o.FECHA_SALIDA.ToShortDateString(),"",cliente.NOMBRE_CLIENTE,o.COMENTARIO,"$" + o.MONTO_TOTAL);
                }
            }

            //Carga de GriedView
            gvOrden.DataSource = dt;
            gvOrden.DataBind();
        }

        private void CargarGridDetalles (List<DetalleOrden> detalle) {
            Huesped huesped;
            Pension pension;
            CategoriaHabitacion cat;
            TipoHabitacion tipo;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("RUT_HUSPED",typeof(string)),
                new DataColumn("HUESPED", typeof(string)),
                new DataColumn("HABITACION",typeof(string)),
                new DataColumn("PENSION",typeof(string)),
                new DataColumn("ESTADO",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (DetalleOrden d in detalle) {
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

                dt.Rows.Add(huesped.RUT_HUESPED + "-" + huesped.DV_HUESPED,huesped.PNOMBRE_HUESPED + " " + huesped.APP_PATERNO_HUESPED + " " + huesped.APP_MATERNO_HUESPED,tipo.NOMBRE_TIPO_HABITACION + "-" + cat.NOMBRE_CATEGORIA,pension.NOMBRE_PENSION,d.ESTADO);
            }

            //Carga de GriedView
            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                short numero_orden = short.Parse(btn.CommandArgument);

                OrdenCompra orden = new OrdenCompra();
                orden.NUMERO_ORDEN = numero_orden;

                MiSesionOrden = orden;
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
            Response.Redirect("WebAsignacion.aspx");
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
                        CargarGridDetalles(listaDetalle);

                        MiSesionOrden = s.ObtenerReserva(writer.ToString());
                        exampleModalLabel2.InnerText = "Detalle Orden N°" + MiSesionOrden.NUMERO_ORDEN;
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
    }
}