using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Empleado
{
    public partial class WebCrearFactura : System.Web.UI.Page
    {
        private List<Comuna> coleccionComuna;
        private List<Region> coleccionRegion;
        void Page_PreInit(object sender, EventArgs e)
        {
            if (MiSesion != null)
            {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null)
                {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString()))
                    {
                        MasterPageFile = "~/Administrador/AdminM.Master";
                        Modelo.Empleado emp = new Modelo.Empleado();
                        emp.BuscarEmpleado(MiSesion.ID_USUARIO);
                        SesionEmp = emp;
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Empleado.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString()))
                    {
                        Modelo.Empleado emp = new Modelo.Empleado();
                        emp.BuscarEmpleado(MiSesion.ID_USUARIO);
                        SesionEmp = emp;
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
            try {
                error.Text = "";
                exito.Text = "";
                alerta_exito.Visible = false;
                alerta.Visible = false;

                coleccionComuna = ComunaCollection.ListaComuna();
                coleccionRegion = RegionCollection.ListaRegion();

                if (!IsPostBack) {
                    ddlGiro.DataSource = GiroCollection.ListaGiro();
                    ddlGiro.DataTextField = "NOMBRE_GIRO";
                    ddlGiro.DataValueField = "ID_GIRO";
                    ddlGiro.DataBind();
                    ddlGiro.Items.Insert(0,new ListItem("Seleccione Giro...","0"));

                    Comuna com = new Comuna();
                    com.Id_Comuna = SesionCli.ID_COMUNA;
                    com.BuscarComuna();

                    Region reg = new Region();
                    reg.Id_Region = com.Id_Region;
                    reg.BuscarRegion();

                    Pais pais = new Pais();
                    pais.ID_PAIS = reg.Id_Pais;
                    pais.BuscarPais();

                    ddlPais.DataSource = PaisCollection.ListaPais();
                    ddlPais.DataTextField = "NOMBRE_PAIS";
                    ddlPais.DataValueField = "ID_PAIS";
                    ddlPais.DataBind();
                    ddlPais.Items.Insert(0,new ListItem("Seleccione País...","0"));

                    ddlRegion.Items.Insert(0,new ListItem("Seleccione Región...","0"));
                    ddlComuna.Items.Insert(0,new ListItem("Seleccione Comuna...","0"));

                    ddlCategoria.DataSource = CategoriaHabitacionCollection.ListarCategorias();
                    ddlCategoria.DataTextField = "NombreYPrecio";
                    ddlCategoria.DataValueField = "ID_CATEGORIA_HABITACION";
                    ddlCategoria.DataBind();
                    ddlCategoria.Items.Insert(0,new ListItem("Seleccione Categoría de Habitación...","0"));

                    ddlTipo.DataSource = TipoHabitacionCollection.ListarTipos();
                    ddlTipo.DataTextField = "NombreYPrecio";
                    ddlTipo.DataValueField = "ID_TIPO_HABITACION";
                    ddlTipo.DataBind();
                    ddlTipo.Items.Insert(0,new ListItem("Seleccione Tipo de Habitación...","0"));

                    ddlPension.DataSource = PensionCollection.ListarPensiones();
                    ddlPension.DataTextField = "NombreYPrecio";
                    ddlPension.DataValueField = "ID_PENSION";
                    ddlPension.DataBind();
                    ddlPension.Items.Insert(0,new ListItem("Seleccione Pensión...","0"));

                    if (SesionOrden.NUMERO_ORDEN != 0) {
                        txtRut.Text = SesionCli.RUT_CLIENTE + "-" + SesionCli.DV_CLIENTE;
                        txtDireccion.Text = SesionCli.DIRECCION_CLIENTE;
                        txtNombre.Text = SesionCli.NOMBRE_CLIENTE;
                        txtOrden.Text = SesionOrden.NUMERO_ORDEN.ToString();
                        txtTelefono.Text = SesionCli.TELEFONO_CLIENTE.ToString();
                        ddlPais.SelectedValue = pais.ID_PAIS.ToString();
                        FiltrarRegion();
                        ddlRegion.SelectedValue = reg.Id_Region.ToString();
                        FiltrarComuna();
                        ddlComuna.SelectedValue = com.Id_Comuna.ToString();
                        ddlGiro.SelectedValue = SesionCli.ID_GIRO.ToString();

                        int neto = (int)(SesionOrden.MONTO_TOTAL - (SesionOrden.MONTO_TOTAL * 0.19));
                        txtNeto.Text = neto.ToString();

                        int iva = (int)(SesionOrden.MONTO_TOTAL * 0.19);
                        txtIva.Text = iva.ToString();

                        txtTotal.Text = SesionOrden.MONTO_TOTAL.ToString();
                    }

                    CargarGrid(SesionOrden);
                }
            }
            catch (Exception) {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
            }

        }

        private void CargarGrid(OrdenCompra sesionOrden) {
            List<DetalleOrden> detalles = DetalleOrdenCollection.ListarDetalles().Where(x => x.NUMERO_ORDEN == sesionOrden.NUMERO_ORDEN).ToList();

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("Cantidad", typeof(int)),
                new DataColumn("Descripcion", typeof(string)),
                new DataColumn("Descuento",typeof(int)),
                new DataColumn("ValorUni",typeof(int)),
                new DataColumn("Total",typeof(int))
            });

            TimeSpan ts = sesionOrden.FECHA_SALIDA - sesionOrden.FECHA_LLEGADA;
            int cantidad = ts.Days;


            //Carga de datos en DataTable
            foreach (DetalleOrden d in detalles) {
                dt.Rows.Add(cantidad,GenerarServicioOrden(d),0,d.VALOR_HABITACION + d.VALOR_MINUTA,cantidad * (d.VALOR_HABITACION + d.VALOR_MINUTA));
            }

            //Carga de GriedView
            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
        }

        private string GenerarServicioOrden(DetalleOrden d) {
            TipoHabitacion tipo = new TipoHabitacion();
            tipo.ID_TIPO_HABITACION = d.ID_TIPO_HABITACION;
            tipo.BuscarTipo();

            CategoriaHabitacion cat = new CategoriaHabitacion();
            cat.ID_CATEGORIA_HABITACION = d.ID_CATEGORIA_HABITACION;
            cat.BuscarCategoria();

            Pension pen = new Pension();
            pen.ID_PENSION = d.ID_PENSION;
            pen.BuscarPension();

            return "Servicio de Estadía: Habitación " + tipo.NOMBRE_TIPO_HABITACION + "-" + cat.NOMBRE_CATEGORIA + " con Pensión " + pen.NOMBRE_PENSION;
        }

        private string GenerarServicioPension(Pension pension) {
            return "Servicio de Pensión: " + pension.NOMBRE_PENSION;
        }

        private string GenerarServicioEstadia(TipoHabitacion tipo,CategoriaHabitacion cat) {
            return "Servicio de Estadía: Habitación " + tipo.NOMBRE_TIPO_HABITACION + "-" + cat.NOMBRE_CATEGORIA;
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

        public Modelo.Empleado SesionEmp {
            get {
                if (Session["Empleado"] == null) {
                    Session["Empleado"] = new Modelo.Empleado();
                }
                return (Modelo.Empleado)Session["Empleado"];
            }
            set {
                Session["Empleado"] = value;
            }
        }

        protected void ddlPais_SelectedIndexChanged(object sender,EventArgs e) {
            FiltrarRegion();
        }

        protected void ddlRegion_SelectedIndexChanged(object sender,EventArgs e) {
            FiltrarComuna();
        }

        private void FiltrarComuna() {
            ddlComuna.DataSource = coleccionComuna.Where(x => x.Id_Region == int.Parse(ddlRegion.SelectedValue));
            ddlComuna.DataTextField = "Nombre";
            ddlComuna.DataValueField = "Id_Comuna";
            ddlComuna.DataBind();
            ddlComuna.Items.Insert(0,new ListItem("Selecione Comuna...","0"));
        }

        private void FiltrarRegion() {
            ddlRegion.DataSource = coleccionRegion.Where(x => x.Id_Pais == int.Parse(ddlPais.SelectedValue));
            ddlRegion.DataTextField = "Nombre";
            ddlRegion.DataValueField = "Id_Region";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0,new ListItem("Seleccione Región...","0"));
        }

        protected void btnAgregar_Click(object sender,EventArgs e) {
            ScriptManager.RegisterStartupScript(Page,Page.GetType(),"modal","$('#exampleModal2').modal();",true);
        }

        protected void btnFacturar_Click(object sender,EventArgs e) {
            try {
                if (gvDetalle.Rows.Count != 0) {
                    Factura factura = new Factura();
                    factura.FECHA_EMISION_FACTURA = DateTime.Today;
                    factura.NUMERO_ORDEN = short.Parse(txtOrden.Text);
                    factura.RUT_CLIENTE = int.Parse(txtRut.Text.Substring(0,txtRut.Text.Length - 2));
                    factura.RUT_EMPLEADO = SesionEmp.RUT_EMPLEADO;
                    if (string.IsNullOrEmpty(txtDescuento.Text))
                    {
                        factura.VALOR_DESC_FACTURA = null;
                    }
                    else
                    {
                        factura.VALOR_DESC_FACTURA = decimal.Parse(txtDescuento.Text);
                    }
                    
                    factura.VALOR_IVA_FACTURA = int.Parse(txtIva.Text);
                    factura.VALOR_NETO_FACTURA = int.Parse(txtNeto.Text);
                    factura.VALOR_TOTAL_FACTURA = int.Parse(txtTotal.Text);
                    if (rbCredito.Checked) {
                        factura.METODO_PAGO = rbCredito.Text;
                        if (factura.Crear()) {
                            bool flag = false;
                            foreach (GridViewRow row in gvDetalle.Rows) {
                                DetalleFactura detalles = new DetalleFactura();
                                detalles.CANTIDAD = int.Parse(row.Cells[0].Text);
                                detalles.DESCRIPCION_DETALLE = row.Cells[1].Text;
                                detalles.VALOR_DESC = long.Parse(row.Cells[2].Text);
                                detalles.VALOR_UNITARIO = int.Parse(row.Cells[3].Text);
                                detalles.VALOR_TOTAL = long.Parse(row.Cells[4].Text);
                                flag = detalles.Crear();
                            }

                            if (flag) {
                                if (!string.IsNullOrEmpty(txtOrden.Text))
                                {
                                    OrdenCompra orden = new OrdenCompra();
                                    orden.NUMERO_ORDEN = short.Parse(txtOrden.Text);
                                    orden.BuscarOrden();
                                    orden.ESTADO_ORDEN = Estado_Orden.Cerrado.ToString();
                                    orden.Update();
                                }
                                exito.Text = "Factura creada con éxito";
                                alerta.Visible = false;
                                alerta_exito.Visible = true;
                            }
                            else {
                                error.Text = "Error al crear detalle factura";
                                alerta.Visible = true;
                                alerta_exito.Visible = false;
                            }
                        }
                        else {
                            error.Text = "Error al crear factura";
                            alerta.Visible = true;
                            alerta_exito.Visible = false;
                        }
                    }
                    else if (rbDebito.Checked) {
                        factura.METODO_PAGO = rbDebito.Text;
                        if (factura.Crear()) {
                            bool flag = false;
                            foreach (GridViewRow row in gvDetalle.Rows) {
                                DetalleFactura detalles = new DetalleFactura();
                                detalles.CANTIDAD = int.Parse(row.Cells[0].Text);
                                detalles.DESCRIPCION_DETALLE = row.Cells[1].Text;
                                detalles.VALOR_DESC = long.Parse(row.Cells[2].Text);
                                detalles.VALOR_UNITARIO = int.Parse(row.Cells[3].Text);
                                detalles.VALOR_TOTAL = long.Parse(row.Cells[4].Text);
                                flag = detalles.Crear();
                            }

                            if (flag) {
                                if (!string.IsNullOrEmpty(txtOrden.Text))
                                {
                                    OrdenCompra orden = new OrdenCompra();
                                    orden.NUMERO_ORDEN = short.Parse(txtOrden.Text);
                                    orden.BuscarOrden();
                                    orden.ESTADO_ORDEN = Estado_Orden.Cerrado.ToString();
                                    orden.Update();
                                }
                                exito.Text = "Factura creada con éxito";
                                alerta.Visible = false;
                                alerta_exito.Visible = true;
                            }
                            else {
                                error.Text = "Error al crear detalle factura";
                                alerta.Visible = true;
                                alerta_exito.Visible = false;
                            }
                        }
                        else {
                            error.Text = "Error al crear factura";
                            alerta.Visible = true;
                            alerta_exito.Visible = false;
                        }
                    }
                    else if (rbEfectivo.Checked) {
                        factura.METODO_PAGO = rbEfectivo.Text;
                        if (factura.Crear()) {
                            bool flag = false;
                            foreach (GridViewRow row in gvDetalle.Rows) {
                                DetalleFactura detalles = new DetalleFactura();
                                detalles.CANTIDAD = int.Parse(row.Cells[0].Text);
                                detalles.DESCRIPCION_DETALLE = row.Cells[1].Text;
                                detalles.VALOR_DESC = long.Parse(row.Cells[2].Text);
                                detalles.VALOR_UNITARIO = int.Parse(row.Cells[3].Text);
                                detalles.VALOR_TOTAL = long.Parse(row.Cells[4].Text);
                                flag = detalles.Crear();
                            }

                            if (flag) {
                                if (!string.IsNullOrEmpty(txtOrden.Text))
                                {
                                    OrdenCompra orden = new OrdenCompra();
                                    orden.NUMERO_ORDEN = short.Parse(txtOrden.Text);
                                    orden.BuscarOrden();
                                    orden.ESTADO_ORDEN = Estado_Orden.Cerrado.ToString();
                                    orden.Update();
                                }
                                exito.Text = "Factura creada con éxito";
                                alerta.Visible = false;
                                alerta_exito.Visible = true;
                            }
                            else {
                                error.Text = "Error al crear detalle factura";
                                alerta.Visible = true;
                                alerta_exito.Visible = false;
                            }
                        }
                        else {
                            error.Text = "Error al crear factura";
                            alerta.Visible = true;
                            alerta_exito.Visible = false;
                        }
                    }
                    else {
                        error.Text = "Debe seleccionar una forma de pago";
                        alerta.Visible = true;
                        alerta_exito.Visible = false;
                    }
                }else {
                    error.Text = "Debe tener mínimo un elemento para cobrar";
                    alerta.Visible = true;
                    alerta_exito.Visible = false;
                }
            }
            catch (Exception ex) {
                error.Text = "Excepción: " + ex.Message;
                alerta.Visible = true;
            }
        }
    }
}