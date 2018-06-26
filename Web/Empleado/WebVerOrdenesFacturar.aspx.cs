using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Empleado {
    public partial class WebVerOrdenesFacturar : System.Web.UI.Page {

        private List<OrdenCompra> ordenes;

        private void Page_PreInit(object sender,EventArgs e) {
            if (MiSesion != null) {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null) {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Administrador/AdminM.Master";
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Empleado.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Empleado/EmpleadoM.Master";
                    }
                    else {
                        Response.Write("<script language='javascript'>window.alert('No Posee los permisos necesarios');window.location='../Hostal/WebLogin.aspx';</script>");
                    }
                }
                else {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
        }

        protected void Page_Load(object sender,EventArgs e) {
            try {
                error.Text = "";
                exito.Text = "";
                alerta_exito.Visible = false;
                alerta.Visible = false;

                ordenes = OrdenCompraCollection.Listar().Where(x => x.ESTADO_ORDEN.Equals(Estado_Orden.Asignado.ToString())).ToList();

                if (!IsPostBack) {

                    ddlFiltro.DataSource = ClienteCollection.ListaClientes();
                    ddlFiltro.DataTextField = "NOMBRE_CLIENTE";
                    ddlFiltro.DataValueField = "RUT_CLIENTE";
                    ddlFiltro.DataBind();
                    ddlFiltro.Items.Insert(0,new ListItem("Seleccione Cliente...","0"));

                    CargarGridOrdenes(ordenes);
                }
            }
            catch (Exception) {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
            }
        }

        private void CargarGridOrdenes(List<OrdenCompra> ordenes) {
            Modelo.Cliente cliente;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn("Número Orden", typeof(int)),
                new DataColumn("Nombre Cliente", typeof(string)),
                new DataColumn("Fecha Llegada",typeof(string)),
                new DataColumn("Fecha Salida",typeof(string)),
                new DataColumn("Monto Total",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (OrdenCompra o in ordenes) {
                cliente = new Modelo.Cliente();
                cliente.RUT_CLIENTE = o.RUT_CLIENTE;
                cliente.BuscarCliente();
                
                dt.Rows.Add(o.NUMERO_ORDEN,cliente.NOMBRE_CLIENTE,o.FECHA_LLEGADA.ToShortDateString(),o.FECHA_SALIDA.ToShortDateString(),"$" + o.MONTO_TOTAL);
            }

            //Carga de GriedView
            gvOrdenes.DataSource = dt;
            gvOrdenes.DataBind();
        }

        //Creación de Sesión
        public Usuario MiSesion {
            get {
                if (Session["Usuario"] == null) {
                    Session["Usuario"] = new Usuario();
                }
                return (Usuario)Session["Usuario"];
            }
            set {
                Session["Usuario"] = value;
            }
        }

        public Modelo.Cliente SesionCli {
            get {
                if (Session["Cliente"] == null) {
                    Session["Cliente"] = new Modelo.Cliente();
                }
                return (Modelo.Cliente)Session["Cliente"];
            }
            set {
                Session["Cliente"] = value;
            }
        }

        public OrdenCompra SesionOrden {
            get {
                if (Session["Orden"] == null) {
                    Session["Orden"] = new OrdenCompra();
                }
                return (OrdenCompra)Session["Orden"];
            }
            set {
                Session["Orden"] = value;
            }
        }

        protected void btnFiltrarFecha_Click(object sender,EventArgs e) {
            try {
                if (!string.IsNullOrEmpty(txtFechaInicio.Text) && !string.IsNullOrEmpty(txtFechaFinal.Text)) {
                    DateTime fechaIn = DateTime.Parse(txtFechaInicio.Text);
                    DateTime fechaFin = DateTime.Parse(txtFechaFinal.Text);
                    fechaFin = fechaFin.AddHours(23);
                    fechaFin = fechaFin.AddMinutes(59);
                    fechaFin = fechaFin.AddSeconds(59);
                    if (DateTime.Compare(fechaFin,fechaIn) >= 0) {
                        if (ddlFiltro.SelectedIndex != 0) {
                            var lista = ordenes.Where(x => x.RUT_CLIENTE == int.Parse(ddlFiltro.SelectedValue.ToString()) 
                                                      && x.FECHA_LLEGADA >= fechaIn
                                                      && x.FECHA_SALIDA <= fechaFin).ToList();
                            CargarGridOrdenes(lista);

                        }
                        else {
                            var lista = ordenes.Where(x => x.FECHA_LLEGADA >= fechaIn
                                                           && x.FECHA_SALIDA <= fechaFin).ToList();
                            CargarGridOrdenes(lista);
                        }
                    }else {
                        error.Text = "Las fechas inicial no puede ser mayor a la final";
                        alerta.Visible = true;
                    }
                }
                else {
                    error.Text = "Las fechas no pueden ser nulas o estar vacias";
                    alerta.Visible = true;
                }
            }
            catch (Exception ex) {
                error.Text = "Excepcion: " + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void Limpiar_Click(object sender,EventArgs e) {
            try {
                ddlFiltro.SelectedIndex = 0;
                txtFechaInicio.Text = string.Empty;
                txtFechaFinal.Text = string.Empty;
                CargarGridOrdenes(ordenes);
            }catch (Exception ex) {
                error.Text = "Excepcion: " + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void ddlFiltro_SelectedIndexChanged(object sender,EventArgs e) {
            try {
                if (ddlFiltro.SelectedIndex != 0) {
                    txtFechaInicio.Text = string.Empty;
                    txtFechaFinal.Text = string.Empty;
                    var lista = ordenes.Where(x => x.RUT_CLIENTE == int.Parse(ddlFiltro.SelectedValue.ToString())).ToList();
                    CargarGridOrdenes(lista);
                } else {
                    error.Text = "Seleccione un filtro válido";
                    exito.Text = "";
                    alerta_exito.Visible = false;
                    alerta.Visible = true;
                }
            } catch(Exception ex) {
                error.Text = "Excepcion: " + ex.Message;
                alerta.Visible = true;
            }
            
        }

        protected void gvOrdenes_SelectedIndexChanged(object sender,EventArgs e) {
            try {
                short nOrden = short.Parse(gvOrdenes.DataKeys[gvOrdenes.SelectedIndex].Values["Número Orden"].ToString());
                OrdenCompra orden = new OrdenCompra();

                orden.NUMERO_ORDEN = nOrden;
                orden.BuscarOrden();

                Modelo.Cliente cliente = new Modelo.Cliente();
                cliente.RUT_CLIENTE = orden.RUT_CLIENTE;
                cliente.BuscarCliente();

                SesionOrden = orden;
                SesionCli = cliente;

                Response.Redirect("../Empleado/WebCrearFactura.aspx");
            }
            catch (Exception ex) {
                error.Text = "Excepcion: " + ex.Message;
                alerta.Visible = true;
            }
        }
    }
}