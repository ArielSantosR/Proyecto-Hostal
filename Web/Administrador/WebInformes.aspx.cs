using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Modelo;
using Modelo.Modelo_Vistas;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Administrador {
    public partial class WebInformes : System.Web.UI.Page {
        void Page_PreInit(object sender,EventArgs e) {
            if (MiSesion != null) {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null) {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Administrador/AdminM.Master";
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
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;
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

        public DataTable SesionTabla {
            get {
                if (Session["TablaInforme"] == null) {
                    Session["TablaInforme"] = new DataTable();
                }
                return (DataTable)Session["TablaInforme"];
            }
            set {
                Session["TablaInforme"] = value;
            }
        }

        public string SesionNombre {
            get {
                if (Session["Nombre"] == null) {
                    Session["Nombre"] = string.Empty;
                }
                return Session["Nombre"].ToString();
            }
            set {
                Session["Nombre"] = value;
            }
        }

        protected void btnClienteOrd_Click(object sender,EventArgs e) {
            try {
                List<V_Cliente_Ordenes> lista = V_Cliente_Ordenes_Collection.Listar();

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] {
                    new DataColumn("RUT", typeof(string)),
                    new DataColumn("Nombre Cliente", typeof(string)),
                    new DataColumn("Giro",typeof(string)),
                    new DataColumn("Ordenes Pendientes",typeof(decimal)),
                    new DataColumn("Ordenes Aceptadas",typeof(decimal)),
                    new DataColumn("Ordenes Asignadas",typeof(decimal)),
                    new DataColumn("Ordenes Rechazadas",typeof(decimal)),
                    new DataColumn("Cantidad Canceladas",typeof(decimal)),
                    new DataColumn("Cantidad Ordenes",typeof(decimal))
                 });

                foreach (V_Cliente_Ordenes c in lista) {
                    dt.Rows.Add(c.RUT,c.NOMBRE_CLIENTE,c.GIRO,c.ORDENES_PENDIENTES,c.ORDENES_ACEPTADAS,c.ORDENES_ASIGNADAS,c.ORDENES_RECHAZADAS,c.ORDENES_CANCELADAS,c.CANTIDAD_ORDENES);
                }

                SesionTabla = dt;
                SesionNombre = "Informe_Ordenes_Clientes";
                gvDetalle.DataSource = dt;
                gvDetalle.DataBind();
                ScriptManager.RegisterStartupScript(Page,Page.GetType(),"modal","$('#modalPrev').modal();",true);

            }
            catch (Exception ex) {
                error.Text = "Exception:" + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnClienteFrec_Click(object sender,EventArgs e) {
            try {
                List<V_Cliente_Frecuente> lista = V_Cliente_Frecuente_Collection.Listar();

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] {
                    new DataColumn("RUT", typeof(string)),
                    new DataColumn("Nombre Cliente", typeof(string)),
                    new DataColumn("Giro",typeof(string)),
                    new DataColumn("Cantidad Ordenes",typeof(decimal))
                });

                foreach (V_Cliente_Frecuente c in lista) {
                    dt.Rows.Add(c.RUT,c.NOMBRE_CLIENTE,c.GIRO,c.CANTIDAD_ORDENES);
                }

                SesionNombre = "Informe_Clientes_Frecuentes";
                SesionTabla = dt;
                gvDetalle.DataSource = dt;
                gvDetalle.DataBind();
                ScriptManager.RegisterStartupScript(Page,Page.GetType(),"modal","$('#modalPrev').modal();",true);

            }
            catch (Exception ex) {
                error.Text = "Exception:" + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnClienteTop_Click(object sender,EventArgs e) {
            try {
                List<V_Cliente_Ordenes_Top_10> lista = V_Cliente_Ordenes_Top_10_Collection.Listar();

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] {
                    new DataColumn("RUT", typeof(string)),
                    new DataColumn("Nombre Cliente", typeof(string)),
                    new DataColumn("Giro",typeof(string)),
                    new DataColumn("Ordenes Pendientes",typeof(decimal)),
                    new DataColumn("Ordenes Aceptadas",typeof(decimal)),
                    new DataColumn("Ordenes Asignadas",typeof(decimal)),
                    new DataColumn("Ordenes Rechazadas",typeof(decimal)),
                    new DataColumn("Cantidad Canceladas",typeof(decimal)),
                    new DataColumn("Cantidad Ordenes",typeof(decimal))
                });

                foreach (V_Cliente_Ordenes_Top_10 c in lista) {
                    dt.Rows.Add(c.RUT,c.NOMBRE_CLIENTE,c.GIRO,c.ORDENES_PENDIENTES,c.ORDENES_ACEPTADAS,c.ORDENES_ASIGNADAS,c.ORDENES_RECHAZADAS,c.ORDENES_CANCELADAS,c.CANTIDAD_ORDENES);
                }

                SesionNombre = "Informe_Clientes_Top_10";
                SesionTabla = dt;
                gvDetalle.DataSource = dt;
                gvDetalle.DataBind();
                ScriptManager.RegisterStartupScript(Page,Page.GetType(),"modal","$('#modalPrev').modal();",true);

            }
            catch (Exception ex) {
                error.Text = "Exception:" + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnDiasUso_Click(object sender,EventArgs e) {
            try {
                List<V_Dias_Usos> lista = V_Dias_Usos_Collection.Listar();

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] {
                    new DataColumn("Numero Habitacion",typeof(short)),
                    new DataColumn("Dias de Uso",typeof(decimal))
                });

                foreach (V_Dias_Usos h in lista) {
                    dt.Rows.Add(h.NUMERO_HABITACION,h.DIAS_USO);
                }

                SesionTabla = dt;
                SesionNombre = "Informe_Dias_Uso_Habitacion";
                gvDetalle.DataSource = dt;
                gvDetalle.DataBind();
                ScriptManager.RegisterStartupScript(Page,Page.GetType(),"modal","$('#modalPrev').modal();",true);

            }
            catch (Exception ex) {
                error.Text = "Exception:" + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnCantidadUsos_Click(object sender,EventArgs e) {
            try {
                List<V_Habitacion_Usos> lista = V_Habitacion_Usos_Collection.Listar();

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] {
                    new DataColumn("Numero Habitacion",typeof(short)),
                    new DataColumn("Numero de Uso",typeof(decimal))
                });

                foreach (V_Habitacion_Usos h in lista) {
                    dt.Rows.Add(h.NUMERO_HABITACION,h.NUMERO_USOS);
                }

                SesionNombre = "Informe_Cantidad_Uso_Habitacion";
                SesionTabla = dt;
                gvDetalle.DataSource = dt;
                gvDetalle.DataBind();
                ScriptManager.RegisterStartupScript(Page,Page.GetType(),"modal","$('#modalPrev').modal();",true);

            }
            catch (Exception ex) {
                error.Text = "Exception:" + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnCantidadUsosTop_Click(object sender,EventArgs e) {
            try {
                List<V_Habitacion_Usos_Top_10> lista = V_Habitacion_Usos_Top_10_Collection.Listar();

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] {
                    new DataColumn("Numero Habitacion",typeof(short)),
                    new DataColumn("Numero de Uso",typeof(decimal))
                });

                foreach (V_Habitacion_Usos_Top_10 h in lista) {
                    dt.Rows.Add(h.NUMERO_HABITACION,h.NUMERO_USOS);
                }

                SesionNombre = "Informe_Cantidad_Uso_Habitacion_Top_10";
                SesionTabla = dt;
                gvDetalle.DataSource = dt;
                gvDetalle.DataBind();
                ScriptManager.RegisterStartupScript(Page,Page.GetType(),"modal","$('#modalPrev').modal();",true);

            }
            catch (Exception ex) {
                error.Text = "Exception:" + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnHabitacionPref_Click(object sender,EventArgs e) {
            try {
                List<V_Habitacion_Preferida> lista = V_Habitacion_Preferida_Collection.Listar();

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] {
                    new DataColumn("Numero Habitacion",typeof(short)),
                    new DataColumn("Numero de Uso",typeof(decimal))
                });

                foreach (V_Habitacion_Preferida h in lista) {
                    dt.Rows.Add(h.NUMERO_HABITACION,h.NUMERO_USOS);
                }

                SesionNombre = "Informe_Habitaciones_Preferidas";
                SesionTabla = dt;
                gvDetalle.DataSource = dt;
                gvDetalle.DataBind();
                ScriptManager.RegisterStartupScript(Page,Page.GetType(),"modal","$('#modalPrev').modal();",true);

            }
            catch (Exception ex) {
                error.Text = "Exception:" + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnExcel_Click(object sender,EventArgs e) {
            try {
                Response.Clear();
                Response.Charset = "";
                Response.Buffer = true;
                Response.AddHeader("content-disposition","attachment; filename=" + SesionNombre + ".xls");
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvDetalle.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex) {
                error.Text = "Exception:" + ex.Message;
                alerta.Visible = true;
            }

        }

        protected void btnWord_Click(object sender,EventArgs e) {
            try {
                Response.Clear();
                Response.Charset = "";
                Response.Buffer = true;
                Response.AddHeader("content-disposition","attachment; filename=" + SesionNombre + ".doc");
                Response.ContentType = "application/ms-word";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvDetalle.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex) {
                error.Text = "Exception:" + ex.Message;
                alerta.Visible = true;
            }
        }

        protected void btnPdf_Click(object sender,EventArgs e) {
            try {
                PdfPTable pdfTable = new PdfPTable(gvDetalle.HeaderRow.Cells.Count);
                foreach (TableCell cell in gvDetalle.HeaderRow.Cells) {
                    Font font = new Font();
                    font.Color = new BaseColor(gvDetalle.HeaderStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(cell.Text,font));
                    pdfCell.BackgroundColor = new BaseColor(gvDetalle.HeaderStyle.BackColor);
                    pdfTable.AddCell(pdfCell);
                }
                foreach (GridViewRow row in gvDetalle.Rows) {
                    foreach (TableCell cell in row.Cells) {
                        Font font = new Font();
                        font.Color = new BaseColor(gvDetalle.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(cell.Text));
                        pdfCell.BackgroundColor = new BaseColor(gvDetalle.RowStyle.BackColor);
                        pdfTable.AddCell(pdfCell);
                    }
                }
                Document pdfDoc = new Document(PageSize.A4,10f,10f,10f,10f);

                PdfWriter.GetInstance(pdfDoc,Response.OutputStream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();



                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition","attachment; filename=" + SesionNombre + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                Response.Write(pdfDoc);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex) {
                error.Text = "Exception:" + ex.Message;
                alerta.Visible = true;
            }
        }

        public override void VerifyRenderingInServerForm(Control control) {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */

        }
    }
}   