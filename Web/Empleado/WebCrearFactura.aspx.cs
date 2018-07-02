using iTextSharp.text;
using iTextSharp.text.pdf;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static iTextSharp.text.Font;

namespace Web.Empleado {
    public partial class WebCrearFactura : System.Web.UI.Page {
        Font titleFont = new Font(FontFamily.HELVETICA,15,Font.BOLD);
        Font font13 = new Font(FontFamily.HELVETICA,13,Font.NORMAL);
        Font font15 = new Font(FontFamily.HELVETICA,15,Font.NORMAL);
        Font font13I = new Font(FontFamily.HELVETICA,13,Font.ITALIC);
        Font font6 = new Font(FontFamily.HELVETICA,6);
        Font font6B = new Font(FontFamily.HELVETICA,6,Font.BOLD);
        Font font8 = new Font(FontFamily.HELVETICA,8);
        Font font8B = new Font(FontFamily.HELVETICA,8,Font.BOLD);
        private List<Comuna> coleccionComuna;
        private List<Region> coleccionRegion;
        void Page_PreInit (object sender,EventArgs e) {
            if (MiSesion != null) {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null) {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Administrador/AdminM.Master";
                        Modelo.Empleado emp = new Modelo.Empleado();
                        emp.BuscarEmpleado(MiSesion.ID_USUARIO);
                        SesionEmp = emp;
                    } else if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Empleado.ToString()) &&
                      MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        Modelo.Empleado emp = new Modelo.Empleado();
                        emp.BuscarEmpleado(MiSesion.ID_USUARIO);
                        SesionEmp = emp;
                        MasterPageFile = "~/Empleado/EmpleadoM.Master";
                    } else {
                        Response.Write("<script language='javascript'>window.alert('No Posee los permisos necesarios');window.location='../Hostal/WebLogin.aspx';</script>");
                    }
                } else {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
        }

        protected void Page_Load (object sender,EventArgs e) {
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
                    ddlGiro.Items.Insert(0,new System.Web.UI.WebControls.ListItem("Seleccione Giro...","0"));

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
                    ddlPais.Items.Insert(0,new System.Web.UI.WebControls.ListItem("Seleccione País...","0"));

                    ddlRegion.Items.Insert(0,new System.Web.UI.WebControls.ListItem("Seleccione Región...","0"));
                    ddlComuna.Items.Insert(0,new System.Web.UI.WebControls.ListItem("Seleccione Comuna...","0"));

                    ddlCategoria.DataSource = CategoriaHabitacionCollection.ListarCategorias();
                    ddlCategoria.DataTextField = "NombreYPrecio";
                    ddlCategoria.DataValueField = "ID_CATEGORIA_HABITACION";
                    ddlCategoria.DataBind();
                    ddlCategoria.Items.Insert(0,new System.Web.UI.WebControls.ListItem("Seleccione Categoría de Habitación...","0"));

                    ddlTipo.DataSource = TipoHabitacionCollection.ListarTipos();
                    ddlTipo.DataTextField = "NombreYPrecio";
                    ddlTipo.DataValueField = "ID_TIPO_HABITACION";
                    ddlTipo.DataBind();
                    ddlTipo.Items.Insert(0,new System.Web.UI.WebControls.ListItem("Seleccione Tipo de Habitación...","0"));

                    ddlPension.DataSource = PensionCollection.ListarPensiones();
                    ddlPension.DataTextField = "NombreYPrecio";
                    ddlPension.DataValueField = "ID_PENSION";
                    ddlPension.DataBind();
                    ddlPension.Items.Insert(0,new System.Web.UI.WebControls.ListItem("Seleccione Pensión...","0"));

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
            } catch (Exception) {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
            }

        }

        private void CargarGrid (OrdenCompra sesionOrden) {
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

            if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString())) {
                foreach (GridViewRow rowG in gvDetalle.Rows) {
                    TextBox descuento = (TextBox)rowG.Cells[2].FindControl("txtDescuentoG");
                    descuento.Enabled = true;
                }
            }

            SesionTable = dt;
        }

        private string GenerarServicioOrden (DetalleOrden d) {
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

        private string GenerarServicioPension (Pension pension) {
            return "Servicio de Pensión: " + pension.NOMBRE_PENSION;
        }

        private string GenerarServicioEstadia (TipoHabitacion tipo,CategoriaHabitacion cat) {
            return "Servicio de Estadía: Habitación " + tipo.NOMBRE_TIPO_HABITACION + "-" + cat.NOMBRE_CATEGORIA;
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

        public DataTable SesionTable {
            get {
                if (Session["Tabla"] == null) {
                    Session["Tabla"] = new DataTable();
                }
                return (DataTable)Session["Tabla"];
            }
            set {
                Session["Tabla"] = value;
            }
        }

        protected void ddlPais_SelectedIndexChanged (object sender,EventArgs e) {
            FiltrarRegion();
        }

        protected void ddlRegion_SelectedIndexChanged (object sender,EventArgs e) {
            FiltrarComuna();
        }

        private void FiltrarComuna () {
            ddlComuna.DataSource = coleccionComuna.Where(x => x.Id_Region == int.Parse(ddlRegion.SelectedValue));
            ddlComuna.DataTextField = "Nombre";
            ddlComuna.DataValueField = "Id_Comuna";
            ddlComuna.DataBind();
            ddlComuna.Items.Insert(0,new System.Web.UI.WebControls.ListItem("Selecione Comuna...","0"));
        }

        private void FiltrarRegion () {
            ddlRegion.DataSource = coleccionRegion.Where(x => x.Id_Pais == int.Parse(ddlPais.SelectedValue));
            ddlRegion.DataTextField = "Nombre";
            ddlRegion.DataValueField = "Id_Region";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0,new System.Web.UI.WebControls.ListItem("Seleccione Región...","0"));
        }

        protected void btnAgregar_Click (object sender,EventArgs e) {
            ScriptManager.RegisterStartupScript(Page,Page.GetType(),"modal","$('#exampleModal2').modal();",true);
        }

        protected void btnFacturar_Click (object sender,EventArgs e) {
            try {
                if (gvDetalle.Rows.Count != 0) {
                    Factura factura = new Factura();
                    factura.FECHA_EMISION_FACTURA = DateTime.Today;
                    factura.NUMERO_ORDEN = short.Parse(txtOrden.Text);
                    factura.RUT_CLIENTE = int.Parse(txtRut.Text.Substring(0,txtRut.Text.Length - 2));
                    factura.RUT_EMPLEADO = SesionEmp.RUT_EMPLEADO;
                    if (string.IsNullOrEmpty(txtDescuento.Text)) {
                        factura.VALOR_DESC_FACTURA = null;
                    } else {
                        factura.VALOR_DESC_FACTURA = decimal.Parse(txtDescuento.Text);
                    }

                    factura.VALOR_IVA_FACTURA = int.Parse(txtIva.Text);
                    factura.VALOR_NETO_FACTURA = int.Parse(txtNeto.Text);
                    factura.VALOR_TOTAL_FACTURA = int.Parse(txtTotal.Text);
                    if (rbCredito.Checked) {
                        factura.METODO_PAGO = rbCredito.Text;
                        if (factura.Crear()) {
                            bool flag = false;
                            TextBox numeros;
                            foreach (GridViewRow row in gvDetalle.Rows) {
                                DetalleFactura detalles = new DetalleFactura();
                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[0].FindControl("txtCantidad"));
                                detalles.CANTIDAD = int.Parse(numeros.Text);

                                detalles.DESCRIPCION_DETALLE = HttpUtility.HtmlDecode(row.Cells[1].Text);

                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[2].FindControl("txtDescuentoG"));
                                detalles.VALOR_DESC = long.Parse(numeros.Text);

                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[3].FindControl("txtValorUniG"));
                                detalles.VALOR_UNITARIO = int.Parse(numeros.Text);

                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[4].FindControl("txtValorTotal"));
                                detalles.VALOR_TOTAL = long.Parse(numeros.Text);
                                flag = detalles.Crear();
                            }

                            if (flag) {
                                if (!string.IsNullOrEmpty(txtOrden.Text)) {
                                    OrdenCompra orden = new OrdenCompra();
                                    orden.NUMERO_ORDEN = short.Parse(txtOrden.Text);
                                    orden.BuscarOrden();
                                    orden.ESTADO_ORDEN = Estado_Orden.Cerrado.ToString();
                                    orden.Update();
                                }
                                List<Factura> facturas = FacturaCollection.ListarFacturas().OrderBy(x => x.FECHA_EMISION_FACTURA).ToList();
                                factura = new Factura();
                                factura.ID_FACTURA = facturas[facturas.Count - 1].ID_FACTURA;
                                factura.BuscarFactura();

                                List<DetalleFactura> detalles = DetalleFacturaCollection.ListarDetalleFacturas().Where(x => x.ID_FACTURA == factura.ID_FACTURA).ToList();
                                ImprimirFactura(factura,detalles);

                                exito.Text = "Factura creada con éxito";
                                alerta.Visible = false;
                                alerta_exito.Visible = true;

                                Limpiar();
                            } else {
                                error.Text = "Error al crear detalle factura";
                                alerta.Visible = true;
                                alerta_exito.Visible = false;
                            }
                        } else {
                            error.Text = "Error al crear factura";
                            alerta.Visible = true;
                            alerta_exito.Visible = false;
                        }
                    } else if (rbDebito.Checked) {
                        factura.METODO_PAGO = rbDebito.Text;
                        if (factura.Crear()) {
                            bool flag = false;
                            TextBox numeros;
                            foreach (GridViewRow row in gvDetalle.Rows) {
                                DetalleFactura detalles = new DetalleFactura();
                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[0].FindControl("txtCantidad"));
                                detalles.CANTIDAD = int.Parse(numeros.Text);

                                detalles.DESCRIPCION_DETALLE = HttpUtility.HtmlDecode(row.Cells[1].Text);

                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[2].FindControl("txtDescuentoG"));
                                detalles.VALOR_DESC = long.Parse(numeros.Text);

                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[3].FindControl("txtValorUniG"));
                                detalles.VALOR_UNITARIO = int.Parse(numeros.Text);

                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[4].FindControl("txtValorTotal"));
                                detalles.VALOR_TOTAL = long.Parse(numeros.Text);
                                flag = detalles.Crear();
                            }

                            if (flag) {
                                if (!string.IsNullOrEmpty(txtOrden.Text)) {
                                    OrdenCompra orden = new OrdenCompra();
                                    orden.NUMERO_ORDEN = short.Parse(txtOrden.Text);
                                    orden.BuscarOrden();
                                    orden.ESTADO_ORDEN = Estado_Orden.Cerrado.ToString();
                                    orden.Update();
                                }
                                List<Factura> facturas = FacturaCollection.ListarFacturas().OrderBy(x => x.FECHA_EMISION_FACTURA).ToList();
                                factura = new Factura();
                                factura.ID_FACTURA = facturas[facturas.Count - 1].ID_FACTURA;
                                factura.BuscarFactura();

                                List<DetalleFactura> detalles = DetalleFacturaCollection.ListarDetalleFacturas().Where(x => x.ID_FACTURA == factura.ID_FACTURA).ToList();
                                ImprimirFactura(factura,detalles);

                                exito.Text = "Factura creada con éxito";
                                alerta.Visible = false;
                                alerta_exito.Visible = true;

                                Limpiar();
                            } else {
                                error.Text = "Error al crear detalle factura";
                                alerta.Visible = true;
                                alerta_exito.Visible = false;
                            }
                        } else {
                            error.Text = "Error al crear factura";
                            alerta.Visible = true;
                            alerta_exito.Visible = false;
                        }
                    } else if (rbEfectivo.Checked) {
                        factura.METODO_PAGO = rbEfectivo.Text;
                        if (factura.Crear()) {
                            bool flag = false;
                            TextBox numeros;
                            foreach (GridViewRow row in gvDetalle.Rows) {
                                DetalleFactura detalles = new DetalleFactura();
                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[0].FindControl("txtCantidad"));
                                detalles.CANTIDAD = int.Parse(numeros.Text);

                                detalles.DESCRIPCION_DETALLE = HttpUtility.HtmlDecode(row.Cells[1].Text);

                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[2].FindControl("txtDescuentoG"));
                                detalles.VALOR_DESC = long.Parse(numeros.Text);

                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[3].FindControl("txtValorUniG"));
                                detalles.VALOR_UNITARIO = int.Parse(numeros.Text);

                                numeros = new TextBox();
                                numeros = (TextBox)(row.Cells[4].FindControl("txtValorTotal"));
                                detalles.VALOR_TOTAL = long.Parse(numeros.Text);
                                flag = detalles.Crear();
                            }

                            if (flag) {
                                if (!string.IsNullOrEmpty(txtOrden.Text)) {
                                    OrdenCompra orden = new OrdenCompra();
                                    orden.NUMERO_ORDEN = short.Parse(txtOrden.Text);
                                    orden.BuscarOrden();
                                    orden.ESTADO_ORDEN = Estado_Orden.Cerrado.ToString();
                                    orden.Update();
                                }

                                List<Factura> facturas = FacturaCollection.ListarFacturas().OrderBy(x => x.FECHA_EMISION_FACTURA).ToList();
                                factura = new Factura();
                                factura.ID_FACTURA = facturas[facturas.Count - 1].ID_FACTURA;
                                factura.BuscarFactura();

                                List<DetalleFactura> detalles = DetalleFacturaCollection.ListarDetalleFacturas().Where(x => x.ID_FACTURA == factura.ID_FACTURA).ToList();
                                ImprimirFactura(factura,detalles);

                                exito.Text = "Factura creada con éxito";
                                alerta.Visible = false;
                                alerta_exito.Visible = true;

                                Limpiar();
                                
                            } else {
                                error.Text = "Error al crear detalle factura";
                                alerta.Visible = true;
                                alerta_exito.Visible = false;
                            }
                        } else {
                            error.Text = "Error al crear factura";
                            alerta.Visible = true;
                            alerta_exito.Visible = false;
                        }
                    } else {
                        error.Text = "Debe seleccionar una forma de pago";
                        alerta.Visible = true;
                        alerta_exito.Visible = false;
                    }
                } else {
                    error.Text = "Debe tener mínimo un elemento para cobrar";
                    alerta.Visible = true;
                    alerta_exito.Visible = false;
                }
                SesionOrden = null;
                SesionCli = null;
            } catch (Exception ex) {
                error.Text = "Excepción: " + ex.Message;
                alerta.Visible = true;
            }
        }

        private void ImprimirFactura (Factura factura,List<DetalleFactura> detalles) {
            PdfPTable table = new PdfPTable(6);
            table.TotalWidth = 530;
            table.SetWidths(new float[] { 1,2,1,1,1,1 });
            table.LockedWidth = true;

            Document document = new Document(PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document,Response.OutputStream);
            document.Open();
            PdfContentByte cb = writer.DirectContent;

            PdfPCell cell;
            //INFORMACION Hostal
            cell = InformacionHostal();
            cell.Colspan = 3;
            table.AddCell(cell);
            //INFORMACION FACTURA
            cell = InformacionFactura(factura.ID_FACTURA);
            cell.Colspan = 3;
            table.AddCell(cell);
            //SALTO DE LINEA
            cell = SaltoLinea();
            cell.Colspan = 6;
            table.AddCell(cell);
            //INFORMACION CLIENTE
            InformacionCliente(table,factura);
            table.AddCell(SaltoLinea());
            //DETALLE FACTURA

            DetalleFactura(table,factura,detalles);
            table.AddCell(SaltoLinea());

            //FooterFactura
            FooterFactura(table);
            //TimbreElectronico(table);
            document.Add(table);
            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment; filename=factura" + factura.ID_FACTURA + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.Write(document);
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private void Limpiar () {
            txtDescuento.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtIva.Text = string.Empty;
            txtNeto.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtOrden.Text = string.Empty;
            txtRut.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtTotal.Text = string.Empty;
            ddlCategoria.SelectedIndex = 0;
            ddlComuna.SelectedIndex = 0;
            ddlGiro.SelectedIndex = 0;
            ddlPais.SelectedIndex = 0;
            ddlPension.SelectedIndex = 0;
            ddlRegion.SelectedIndex = 0;
            ddlTipo.SelectedIndex = 0;
        }

        public PdfPCell InformacionHostal () {
            PdfPCell cell;
            Paragraph p;

            cell = new PdfPCell();

            p = new Paragraph("H O S T A L",titleFont);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);

            p = new Paragraph("D O Ñ A  C L A R I T A",titleFont);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);

            p = new Paragraph("Servicio de Hotelería y Comedor",font13I);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);

            p = new Paragraph(20,"Casa Matriz: Av. Pdte. Kennedy Lateral 4570, Vitacura",font6);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);

            p = new Paragraph(10,"Tel.: +562 22222222 Santiago - Chile",font6);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        public PdfPCell InformacionFactura (int numFactura) {
            PdfPCell cell;
            Paragraph p;

            cell = new PdfPCell();
            p = new Paragraph("R.U.T.: 77.777.777-7",font13);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);

            p = new Paragraph(25,"FACTURA ELECTRONICA",font13);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);

            p = new Paragraph(20,"N° " + numFactura,font13);
            p.Alignment = Element.ALIGN_CENTER;

            cell.BorderWidth = 2;
            cell.BorderColor = BaseColor.RED;
            cell.AddElement(p);

            return cell;
        }

        public PdfPCell SaltoLinea () {
            PdfPCell cell;

            cell = new PdfPCell(new Paragraph(30," "));
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 6;

            return cell;
        }

        public void InformacionCliente (PdfPTable table,Factura factura) {
            Modelo.Cliente cliente = new Modelo.Cliente();
            cliente.RUT_CLIENTE = factura.RUT_CLIENTE;
            cliente.BuscarCliente();

            Giro giro = new Giro();
            giro.ID_GIRO = cliente.ID_GIRO;
            giro.BuscarGiro();

            Comuna comuna = new Comuna();
            comuna.Id_Comuna = cliente.ID_COMUNA;
            comuna.BuscarComuna();

            PdfPCell cell = new PdfPCell();
            Paragraph p;

            //row 1 
            p = new Paragraph("NOMBRE",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph(": " + cliente.NOMBRE_CLIENTE,font6);
            p.Alignment = Element.ALIGN_LEFT;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("FECHA EMISIÓN",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph(": " + factura.FECHA_EMISION_FACTURA.ToShortDateString(),font6);
            p.Alignment = Element.ALIGN_LEFT;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            //row 2
            cell = new PdfPCell();
            p = new Paragraph("GIRO",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);
            cell = new PdfPCell();

            p = new Paragraph(": " + giro.NOMBRE_GIRO,font6);
            cell.Border = Rectangle.NO_BORDER;
            p.Alignment = Element.ALIGN_LEFT;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthRight = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("RUT",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.Border = Rectangle.NO_BORDER;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph(": " + cliente.RUT_CLIENTE + "-" + cliente.DV_CLIENTE,font6);
            p.Alignment = Element.ALIGN_LEFT;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            //row 3 
            cell = new PdfPCell();
            p = new Paragraph("DIRECCIÓN",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph(": " + cliente.DIRECCION_CLIENTE,font6);
            p.Alignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthRight = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("CIUDAD",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.Border = Rectangle.NO_BORDER;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph(": " + "SANTIAGO",font6);
            p.Alignment = Element.ALIGN_LEFT;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthRight = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            //row 4 
            cell = new PdfPCell();
            p = new Paragraph("Comuna",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph(": " + comuna.Nombre,font6);
            p.Alignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthRight = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("TELÉFONO",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.Border = Rectangle.NO_BORDER;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph(": " + cliente.TELEFONO_CLIENTE,font6);
            p.Alignment = Element.ALIGN_LEFT;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            //row 5
            cell = new PdfPCell();
            p = new Paragraph("O. COMPRA",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthRight = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph(": " + factura.NUMERO_ORDEN,font6);
            p.Alignment = Element.ALIGN_LEFT;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthRight = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("COND. PAGO",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph(": " + factura.METODO_PAGO,font6);
            p.Alignment = Element.ALIGN_LEFT;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("",font6B);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.AddElement(p);
            table.AddCell(cell);
        }

        public void DetalleFactura (PdfPTable table,Factura factura,List<DetalleFactura> detalles) {
            PdfPCell cell;
            cell = new PdfPCell();
            Paragraph p;

            //HEADER
            p = new Paragraph("CANTIDAD :",font6B);
            p.Alignment = Element.ALIGN_CENTER;
            cell.BorderWidthRight = 0;
            cell.AddElement(p);
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("DETALLE",font6B);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);
            cell.BorderWidthRight = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("P. UNITARIO :",font6B);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("DESCUENTO",font6B);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell();
            p = new Paragraph("TOTAL",font6B);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);
            table.AddCell(cell);

            PdfPCell cellCantidad = new PdfPCell();
            cellCantidad.BorderWidthTop = 0;
            cellCantidad.BorderWidthRight = 0;
            cellCantidad.MinimumHeight = 400;

            PdfPCell cellDetalle = new PdfPCell();
            cellDetalle.Colspan = 2;
            cellCantidad.BorderWidthTop = 0;
            cellCantidad.BorderWidthRight = 0;
            cellCantidad.MinimumHeight = 400;

            PdfPCell cellPrecio = new PdfPCell();
            cellCantidad.BorderWidthTop = 0;
            cellCantidad.BorderWidthRight = 0;
            cellCantidad.MinimumHeight = 400;

            PdfPCell cellDescuento = new PdfPCell();
            cellCantidad.BorderWidthTop = 0;
            cellCantidad.BorderWidthRight = 0;
            cellCantidad.MinimumHeight = 400;

            PdfPCell cellTotales = new PdfPCell();
            cellCantidad.BorderWidthTop = 0;
            cellCantidad.MinimumHeight = 400;

            foreach (DetalleFactura detalle in detalles) {
                p = new Paragraph("" + detalle.CANTIDAD,font6);
                p.Alignment = Element.ALIGN_RIGHT;
                cellCantidad.AddElement(p);

                p = new Paragraph("" + detalle.DESCRIPCION_DETALLE,font6);
                p.Alignment = Element.ALIGN_LEFT;
                cellDetalle.AddElement(p);

                p = new Paragraph("" + detalle.VALOR_UNITARIO,font6);
                p.Alignment = Element.ALIGN_RIGHT;
                cellPrecio.AddElement(p);

                p = new Paragraph("" + detalle.VALOR_DESC,font6);
                p.Alignment = Element.ALIGN_RIGHT;
                cellDescuento.AddElement(p);

                p = new Paragraph("" + detalle.VALOR_TOTAL,font6);
                p.Alignment = Element.ALIGN_RIGHT;
                cellTotales.AddElement(p);
            }


            table.AddCell(cellCantidad);
            table.AddCell(cellDetalle);
            table.AddCell(cellPrecio);
            table.AddCell(cellDescuento);
            table.AddCell(cellTotales);

            cellDescuento = new PdfPCell();
            cellDescuento.BorderWidthRight = 0;
            cellDescuento.BorderWidthTop = 0;
            cellDescuento.BorderWidthLeft = 0;
            cellDescuento.BorderWidthBottom = 0;
            cellDescuento.MinimumHeight = 40;
            cellDescuento.Colspan = 4;

            cellPrecio = new PdfPCell();
            cellPrecio.BorderWidthTop = 0;
            cellPrecio.BorderWidthRight = 0;
            cellPrecio.MinimumHeight = 40;

            cellTotales = new PdfPCell();
            cellTotales.BorderWidthTop = 0;
            cellTotales.BorderWidthLeft = 0;
            cellTotales.MinimumHeight = 40;

            p = new Paragraph("TOTAL NETO :",font6B);
            p.Alignment = Element.ALIGN_RIGHT;
            cellPrecio.AddElement(p);

            p = new Paragraph(" " + factura.VALOR_NETO_FACTURA,font6B);
            p.Alignment = Element.ALIGN_RIGHT;
            cellTotales.AddElement(p);

            p = new Paragraph("I.V.A.(19%) :",font6B);
            p.Alignment = Element.ALIGN_RIGHT;
            cellPrecio.AddElement(p);

            p = new Paragraph(" " + factura.VALOR_IVA_FACTURA,font6B);
            p.Alignment = Element.ALIGN_RIGHT;
            cellTotales.AddElement(p);

            p = new Paragraph("TOTAL :",font6B);
            p.Alignment = Element.ALIGN_RIGHT;
            cellPrecio.AddElement(p);

            p = new Paragraph(" " + factura.VALOR_TOTAL_FACTURA,font6B);
            p.Alignment = Element.ALIGN_RIGHT;
            cellTotales.AddElement(p);

            table.AddCell(cellDescuento);
            table.AddCell(cellPrecio);
            table.AddCell(cellTotales);
        }

        public void FooterFactura (PdfPTable table) {
            PdfPCell cell = new PdfPCell();
            cell.Colspan = 6;
            Paragraph p;

            p = new Paragraph(" ",font6);
            cell.AddElement(p);
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Colspan = 3;
            p = new Paragraph("     NOMBRE    _______________________________________________________________________",font6);
            cell.AddElement(p);
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Colspan = 3;
            p = new Paragraph("RUT     ____________________________________________",font6);
            cell.AddElement(p);
            cell.BorderWidthLeft = 0;
            cell.BorderWidthBottom = 0;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Colspan = 6;
            p = new Paragraph("     RECINTO    _______________________________________________________________________",font6);
            cell.AddElement(p);
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Colspan = 3;
            p = new Paragraph("     FECHA        _______________________________________________________________________",font6);
            cell.AddElement(p);
            cell.BorderWidthTop = 0;
            cell.BorderWidthRight = 0;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Colspan = 3;
            p = new Paragraph("FIRMA  ____________________________________________",font6);
            cell.AddElement(p);
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            table.AddCell(cell);
        }

        public void TimbreElectronico (PdfPTable table) {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("images/timbre.jpeg");
            PdfPCell cell = new PdfPCell(image,true);
            cell.Colspan = 3;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
        }

        protected void btnAgregarPension_Click (object sender,EventArgs e) {
            try {
                if (ddlPension.SelectedIndex != 0) {
                    
                    DataRow row = SesionTable.NewRow();
                    Pension pension = new Pension();
                    pension.ID_PENSION = short.Parse(ddlPension.SelectedValue);
                    pension.BuscarPension();

                    row[0] = 1;
                    row[1] = GenerarServicioPension(pension);
                    row[2] = 0;
                    row[3] = pension.VALOR_PENSION;
                    row[4] = pension.VALOR_PENSION;

                    SesionTable.Rows.Add(row);

                    gvDetalle.DataSource = SesionTable;
                    gvDetalle.DataBind();

                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString())) {
                        foreach (GridViewRow rowG in gvDetalle.Rows) {
                            TextBox descuentoT = (TextBox)rowG.Cells[2].FindControl("txtDescuentoG");
                            descuentoT.Enabled = true;
                        }
                    }

                    int total = 0;
                    int descuento = 0;
                    foreach (GridViewRow rowG in gvDetalle.Rows) {
                        TextBox totalT = (TextBox)rowG.Cells[4].FindControl("txtValorTotal");
                        TextBox desT = (TextBox)rowG.Cells[2].FindControl("txtDescuentoG");
                        total += int.Parse(totalT.Text);
                        descuento += int.Parse(desT.Text);
                    }

                    txtIva.Text = (total * 0.19).ToString();
                    txtNeto.Text = (total - (total * 0.19)).ToString();
                    txtDescuento.Text = descuento.ToString();
                    txtTotal.Text = total.ToString();
                } else {
                    error.Text = "Debe escoger una pensión válida";
                    alerta.Visible = true;
                    alerta_exito.Visible = false;
                }
            } catch (Exception ex) {
                error.Text = "Excepción: " + ex.Message;
                alerta.Visible = true;
            }

        }

        protected void btnAgregarEstadia_Click (object sender,EventArgs e) {
            try {
                if (ddlTipo.SelectedIndex != 0 && ddlCategoria.SelectedIndex != 0) {
                    
                    DataRow row = SesionTable.NewRow();

                    TipoHabitacion tipo = new TipoHabitacion();
                    tipo.ID_TIPO_HABITACION = short.Parse(ddlTipo.SelectedValue);
                    tipo.BuscarTipo();

                    CategoriaHabitacion cat = new CategoriaHabitacion();
                    cat.ID_CATEGORIA_HABITACION = short.Parse(ddlCategoria.SelectedValue);
                    cat.BuscarCategoria();

                    row[0] = 1;
                    row[1] = GenerarServicioEstadia(tipo,cat);
                    row[2] = 0;
                    row[3] = tipo.PRECIO_TIPO + cat.PRECIO_CATEGORIA;
                    row[4] = tipo.PRECIO_TIPO + cat.PRECIO_CATEGORIA;

                    SesionTable.Rows.Add(row);

                    gvDetalle.DataSource = SesionTable;
                    gvDetalle.DataBind();

                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString())) {
                        foreach (GridViewRow rowG in gvDetalle.Rows) {
                            TextBox descuentoG = (TextBox)rowG.Cells[2].FindControl("txtDescuentoG");
                            descuentoG.Enabled = true;
                        }
                    }

                    int total = 0;
                    int descuento = 0;
                    foreach (GridViewRow rowG in gvDetalle.Rows) {
                        TextBox totalT = (TextBox)rowG.Cells[4].FindControl("txtValorTotal");
                        TextBox desT = (TextBox)rowG.Cells[2].FindControl("txtDescuentoG");
                        total += int.Parse(totalT.Text);
                        descuento += int.Parse(desT.Text);
                    }

                    txtIva.Text = (total * 0.19).ToString();
                    txtNeto.Text = (total - (total * 0.19)).ToString();
                    txtDescuento.Text = descuento.ToString();
                    txtTotal.Text = total.ToString();
                } else {
                    error.Text = "Debe escoger una tipo o categoria válida";
                    alerta.Visible = true;
                    alerta_exito.Visible = false;
                }
            } catch (Exception ex) {
                error.Text = "Excepción: " + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void txtCantidad_TextChanged (object sender,EventArgs e) {
            try {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
                TextBox cantidad = (TextBox)currentRow.FindControl("txtCantidad");
                TextBox valorUnit = (TextBox)currentRow.FindControl("txtValorUniG");
                TextBox descuento = (TextBox)currentRow.FindControl("txtDescuentoG");
                TextBox total = (TextBox)currentRow.FindControl("txtValorTotal");

                DataRow rowEdit = SesionTable.Rows[currentRow.RowIndex];

                if (int.Parse(cantidad.Text) > 0) {
                    total.Text = ((int.Parse(cantidad.Text) * int.Parse(valorUnit.Text)) - int.Parse(descuento.Text)).ToString();
                    rowEdit[0] = int.Parse(cantidad.Text);
                    rowEdit[4] = int.Parse(total.Text);
                }else {
                    error.Text = "La cantidad no puede ser negativa ni 0";
                    alerta.Visible = true;
                    alerta_exito.Visible = false;
                }

                int totalN = 0;
                int descuentoN = 0;
                foreach (GridViewRow rowG in gvDetalle.Rows) {
                    TextBox totalT = (TextBox)rowG.Cells[4].FindControl("txtValorTotal");
                    TextBox desT = (TextBox)rowG.Cells[2].FindControl("txtDescuentoG");
                    totalN += int.Parse(totalT.Text);
                    descuentoN += int.Parse(desT.Text);
                }

                txtIva.Text = (totalN * 0.19).ToString();
                txtNeto.Text = (totalN - (totalN * 0.19)).ToString();
                txtDescuento.Text = descuentoN.ToString();
                txtTotal.Text = totalN.ToString();
            } catch (Exception ex) {
                error.Text = "Excepción: " + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void txtDescuento_TextChanged (object sender,EventArgs e) {
            try {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
                TextBox cantidad = (TextBox)currentRow.FindControl("txtCantidad");
                TextBox valorUnit = (TextBox)currentRow.FindControl("txtValorUniG");
                TextBox descuento = (TextBox)currentRow.FindControl("txtDescuentoG");
                TextBox total = (TextBox)currentRow.FindControl("txtValorTotal");

                DataRow rowEdit = SesionTable.Rows[currentRow.RowIndex];

                if (int.Parse(total.Text) > int.Parse(descuento.Text)) {
                    total.Text = ((int.Parse(cantidad.Text) * int.Parse(valorUnit.Text)) - int.Parse(descuento.Text)).ToString();
                    rowEdit[2] = int.Parse(descuento.Text);
                    rowEdit[4] = int.Parse(total.Text);
                } else {
                    error.Text = "El descuento no puede ser mayor que el total actual";
                    alerta.Visible = true;
                    alerta_exito.Visible = false;
                }

                int totalN = 0;
                int descuentoN = 0;
                foreach (GridViewRow rowG in gvDetalle.Rows) {
                    TextBox totalT = (TextBox)rowG.Cells[4].FindControl("txtValorTotal");
                    TextBox desT = (TextBox)rowG.Cells[2].FindControl("txtDescuentoG");
                    totalN += int.Parse(totalT.Text);
                    descuentoN += int.Parse(desT.Text);
                }

                txtIva.Text = (totalN * 0.19).ToString();
                txtNeto.Text = (totalN - (totalN * 0.19)).ToString();
                txtDescuento.Text = descuentoN.ToString();
                txtTotal.Text = totalN.ToString();

            } catch (Exception ex) {
                error.Text = "Excepción: " + ex.Message;
                alerta.Visible = true;
            }
        }
    }
}