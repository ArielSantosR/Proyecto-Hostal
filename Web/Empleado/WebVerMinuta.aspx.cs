using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfNegocio;
using Modelo;
using System.Xml.Serialization;
using System.IO;
using System.Data;

namespace Web.Empleado
{
    public partial class WebVerMinuta : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;

            Service1 service = new Service1();
            string datos = service.ListarMinuta();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.MinutaCollection));
            StringReader reader = new StringReader(datos);

            Modelo.MinutaCollection listaMinuta = (Modelo.MinutaCollection)ser.Deserialize(reader);
            reader.Close();
            gvMinuta.DataSource = listaMinuta;
            gvMinuta.DataBind();
        }
        protected void gvMinuta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMinuta.PageIndex = e.NewPageIndex;
            gvMinuta.DataBind();
        }
        protected void gvDetalleMinuta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalleMinuta.PageIndex = e.NewPageIndex;
            gvDetalleMinuta.DataBind();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            short ID_PENSION = short.Parse(btn.CommandArgument);

            Modelo.Minuta minuta = new Modelo.Minuta();
            minuta.ID_PENSION = ID_PENSION;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Minuta));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, minuta);

            string datos = s.ListarDetalleMinuta(writer.ToString());
            XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.DetallePlatoCollection));
            StringReader reader = new StringReader(datos);

            Modelo.DetallePlatoCollection listaDetalle = (Modelo.DetallePlatoCollection)sr2.Deserialize(reader);
            reader.Close();

            if (listaDetalle.Count > 0)
            {
                foreach (DetallePlato d in listaDetalle)
                {
                    XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetallePlato));
                    StringWriter writer2 = new StringWriter();
                    sr3.Serialize(writer2, d);

                    s.EliminarDetallePlatos(writer2.ToString());
                }
            }

            if (s.ModificarMinuta(writer.ToString()))
            {
                Response.Write("<script language='javascript'>window.alert('La minuta ha sido Eliminada con éxito');window.location='../Empleado/WebVerMinuta.aspx';</script>");
                alerta.Visible = false;
            }
        }

        //inicio info
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            short id_pension = short.Parse(btn.CommandArgument);

            Minuta minuta = new Minuta();
            minuta.ID_PENSION = id_pension;

            if (minuta.ID_PENSION != 0)
            {

                Service1 s = new Service1();
                XmlSerializer sr = new XmlSerializer(typeof(Modelo.Minuta));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, minuta);

                if (s.ListarDetalleMinuta(writer.ToString()) != null)
                {
                    string datos = s.ListarDetalleMinuta(writer.ToString());
                    XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetallePlatoCollection));
                    StringReader reader = new StringReader(datos);

                    Modelo.DetallePlatoCollection listaMinuta = (Modelo.DetallePlatoCollection)ser3.Deserialize(reader);
                    reader.Close();
                    CargarTablaMinuta(listaMinuta);

                    exampleModal2Label.InnerText = "Detalle Minuta " + minuta.ID_PENSION;
                }
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
        }
        //fin info



        private void CargarTablaMinuta(List<DetallePlato> lista)
        {
            Plato plato;
            Categoria cat;
            TipoPlato tipo;

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID Plato", typeof(long)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Categoria",typeof(string)),
                new DataColumn("Tipo",typeof(string)),
                new DataColumn("Precio",typeof(string))
            });

            foreach (DetallePlato item in lista)
            {
                plato = new Plato();
                plato.ID_PLATO = item.ID_PLATO;
                plato.BuscarPlato();

                cat = new Categoria();
                cat.ID_CATEGORIA = plato.ID_CATEGORIA;
                cat.BuscarCategoria();

                tipo = new TipoPlato();
                tipo.ID_TIPO_PLATO = plato.ID_TIPO_PLATO;
                tipo.BuscarTipo();

                dt.Rows.Add(plato.ID_PLATO,plato.NOMBRE_PLATO,cat.NOMBRE_CATEGORIA,tipo.NOMBRE_TIPO_PLATO,"$" + plato.PRECIO_PLATO);
            }
            
            gvDetalleMinuta.DataSource = dt;
            gvDetalleMinuta.DataBind();
        }
    }
}