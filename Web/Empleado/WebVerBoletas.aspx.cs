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
    public partial class WebVerBoletas : System.Web.UI.Page {

        Font titleFont = new Font(FontFamily.HELVETICA,15,Font.BOLD);
        Font font13 = new Font(FontFamily.HELVETICA,13,Font.NORMAL);
        Font font15 = new Font(FontFamily.HELVETICA,15,Font.NORMAL);
        Font font13I = new Font(FontFamily.HELVETICA,13,Font.ITALIC);
        Font font6 = new Font(FontFamily.HELVETICA,6);
        Font font6B = new Font(FontFamily.HELVETICA,6,Font.BOLD);
        Font font8 = new Font(FontFamily.HELVETICA,8);
        Font font8B = new Font(FontFamily.HELVETICA,8,Font.BOLD);
        private List<Boleta> boletas;

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

        protected void Page_Load(object sender,EventArgs e) {
            try {
                error.Text = "";
                exito.Text = "";
                alerta_exito.Visible = false;
                alerta.Visible = false;

                boletas = BoletaCollection.ListarBoletas();

                if (!IsPostBack) {
                    CargarGridBoletas(boletas);
                }
            } catch (Exception) {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
            }
        }

        private void CargarGridBoletas (List<Boleta> boletas) {
            Huesped huesped;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("Número Boleta", typeof(int)),
                new DataColumn("Nombre Huésped", typeof(string)),
                new DataColumn("Fecha Boleta",typeof(string)),
                new DataColumn("Monto Total",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (Boleta b in boletas) {
                huesped = new Huesped();
                huesped.RUT_HUESPED = b.RUT_HUESPED;
                huesped.BuscarHuesped();

                dt.Rows.Add(b.ID_BOLETA,huesped.PNOMBRE_HUESPED + " " + huesped.APP_PATERNO_HUESPED + " " + huesped.APP_MATERNO_HUESPED,b.FECHA_EMISION_BOLETA.ToShortDateString(),"$" + b.VALOR_TOTAL_BOLETA);
            }

            //Carga de GriedView
            gvBoletas.DataSource = dt;
            gvBoletas.DataBind();
        }

        protected void gvBoletas_SelectedIndexChanged (object sender,EventArgs e) {
            try {
                int nBoleta = int.Parse(gvBoletas.DataKeys[gvBoletas.SelectedIndex].Values["Número Boleta"].ToString());
                Boleta boleta = new Boleta();

                boleta.ID_BOLETA = nBoleta;
                boleta.BuscarBoleta();

                List<DetalleBoleta> detalles = DetalleBoletaCollection.ListarDetallesBoleta().Where(x => x.ID_BOLETA == boleta.ID_BOLETA).ToList();

                ImprimirBoleta(boleta,detalles);


            } catch (Exception ex) {
                error.Text = "Excepcion: " + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnLimpiar_Click (object sender,EventArgs e) {
            try {
                txtFechaInicio.Text = string.Empty;
                txtFechaFinal.Text = string.Empty;
                txtCantidadIn.Text = string.Empty;
                txtCantidadFin.Text = string.Empty;
                CargarGridBoletas(boletas);
            } catch (Exception ex) {
                error.Text = "Excepcion: " + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnFiltrarMonto_Click (object sender,EventArgs e) {
            try {
                txtFechaInicio.Text = string.Empty;
                txtFechaFinal.Text = string.Empty;
                rvCantidadIn.Validate();
                rvCantidadFin.Validate();
                if (rvCantidadIn.IsValid && rvCantidadFin.IsValid) {
                    if (!string.IsNullOrEmpty(txtCantidadIn.Text) && !string.IsNullOrEmpty(txtCantidadFin.Text)) {
                        int montoIn = int.Parse(txtCantidadIn.Text);
                        int montoFin = int.Parse(txtCantidadFin.Text);
                        if (montoFin >= montoIn) {
                            var lista = boletas.Where(x => x.VALOR_TOTAL_BOLETA >= montoIn
                                                          && x.VALOR_TOTAL_BOLETA <= montoFin).ToList();
                            CargarGridBoletas(lista);
                        } else {
                            error.Text = "El monto inicial no puede ser mayor al final";
                            alerta.Visible = true;
                        }
                    } else {
                        error.Text = "Los montos no pueden ser nulos o estar vacios";
                        alerta.Visible = true;
                    }
                } else {
                    error.Text = "Los montos no son correctos";
                    alerta.Visible = true;
                }

            } catch (Exception ex) {
                error.Text = "Excepcion: " + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnFiltrarFecha_Click (object sender,EventArgs e) {
            try {
                txtCantidadIn.Text = string.Empty;
                txtCantidadFin.Text = string.Empty;
                if (!string.IsNullOrEmpty(txtFechaInicio.Text) && !string.IsNullOrEmpty(txtFechaFinal.Text)) {
                    DateTime fechaIn = DateTime.Parse(txtFechaInicio.Text);
                    DateTime fechaFin = DateTime.Parse(txtFechaFinal.Text);
                    fechaFin = fechaFin.AddHours(23);
                    fechaFin = fechaFin.AddMinutes(59);
                    fechaFin = fechaFin.AddSeconds(59);
                    if (DateTime.Compare(fechaFin,fechaIn) >= 0) {
                        var lista = boletas.Where(x => x.FECHA_EMISION_BOLETA >= fechaIn
                                                           && x.FECHA_EMISION_BOLETA <= fechaFin).ToList();
                        CargarGridBoletas(lista);
                    } else {
                        error.Text = "Las fechas inicial no puede ser mayor a la final";
                        alerta.Visible = true;
                    }
                } else {
                    error.Text = "Las fechas no pueden ser nulas o estar vacias";
                    alerta.Visible = true;
                }
            } catch (Exception ex) {
                error.Text = "Excepcion: " + ex.Message;
                alerta.Visible = true;
            }
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

        private void ImprimirBoleta (Boleta boleta,List<DetalleBoleta> detalles) {
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
            cell = InformacionBoleta(boleta.ID_BOLETA);
            cell.Colspan = 3;
            table.AddCell(cell);
            //SALTO DE LINEA
            cell = SaltoLinea();
            cell.Colspan = 6;
            table.AddCell(cell);
            //INFORMACION CLIENTE
            InformacionCliente(table,boleta);
            table.AddCell(SaltoLinea());
            //DETALLE FACTURA

            DetalleFactura(table,boleta,detalles);
            table.AddCell(SaltoLinea());

            //FooterFactura
            FooterFactura(table);
            //TimbreElectronico(table);
            document.Add(table);
            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment; filename=boleta" + boleta.ID_BOLETA + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.Write(document);
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
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

        public PdfPCell InformacionBoleta (int numBoleta) {
            PdfPCell cell;
            Paragraph p;

            cell = new PdfPCell();
            p = new Paragraph("R.U.T.: 77.777.777-7",font13);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);

            p = new Paragraph(25,"BOLETA ELECTRONICA",font13);
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);

            p = new Paragraph(20,"N° " + numBoleta,font13);
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

        public void InformacionCliente (PdfPTable table,Boleta boleta) {
            Huesped huesped = new Huesped();
            huesped.RUT_HUESPED = boleta.RUT_HUESPED;
            huesped.BuscarHuesped();

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
            p = new Paragraph(": " + huesped.PNOMBRE_HUESPED + " " + huesped.APP_PATERNO_HUESPED + " " + huesped.APP_MATERNO_HUESPED,font6);
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
            p = new Paragraph(": " + boleta.FECHA_EMISION_BOLETA.ToShortDateString(),font6);
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

            p = new Paragraph(": PARTICULAR",font6);
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
            p = new Paragraph(": " + huesped.RUT_HUESPED + "-" + huesped.DV_HUESPED,font6);
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
            p = new Paragraph(": ",font6);
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
            p = new Paragraph(": ",font6);
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
            p = new Paragraph(": " + huesped.TELEFONO_HUESPED,font6);
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
            p = new Paragraph(": ",font6);
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
            p = new Paragraph(": ",font6);
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

        public void DetalleFactura (PdfPTable table,Boleta boleta,List<DetalleBoleta> detalles) {
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

            foreach (DetalleBoleta detalle in detalles) {
                p = new Paragraph("" + detalle.CANTIDAD,font6);
                p.Alignment = Element.ALIGN_RIGHT;
                cellCantidad.AddElement(p);

                p = new Paragraph("" + detalle.DESCRIPCION_DETALLE,font6);
                p.Alignment = Element.ALIGN_LEFT;
                cellDetalle.AddElement(p);

                p = new Paragraph(" ",font6);
                p.Alignment = Element.ALIGN_RIGHT;
                cellPrecio.AddElement(p);

                p = new Paragraph(" ",font6);
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

            p = new Paragraph("TOTAL DESCUENTO :",font6B);
            p.Alignment = Element.ALIGN_RIGHT;
            cellPrecio.AddElement(p);

            p = new Paragraph(" " + boleta.VALOR_DESC_BOLETA,font6B);
            p.Alignment = Element.ALIGN_RIGHT;
            cellTotales.AddElement(p);

            p = new Paragraph("TOTAL :",font6B);
            p.Alignment = Element.ALIGN_RIGHT;
            cellPrecio.AddElement(p);

            p = new Paragraph(" " + boleta.VALOR_TOTAL_BOLETA,font6B);
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

        protected void gvBoletas_PageIndexChanging (object sender,GridViewPageEventArgs e) {
            gvBoletas.PageIndex = e.NewPageIndex;
            gvBoletas.DataBind();
        }
    }
}