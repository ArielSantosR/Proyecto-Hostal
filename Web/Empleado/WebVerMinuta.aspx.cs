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

        public Minuta MiSesionMinuta
        {
            get
            {
                if (Session["Minuta"] == null)
                {
                    Session["Minuta"] = new Minuta();
                }
                return (Minuta)Session["Minuta"];
            }
            set
            {
                Session["Minuta"] = value;
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
            Modelo.Minuta minuta = new Modelo.Minuta();
            minuta.ID_PENSION = MiSesionMinuta.ID_PENSION;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Minuta));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, minuta);

<<<<<<< HEAD
            /*
            if (s.ObtenerMinuta(writer.ToString()) == null)
            {
                alerta_exito.Visible = false;
                error.Text = "No se ha encontrado la Minuta";
                alerta.Visible = true;
            }
            else {
                    alerta_exito.Visible = false;
                    error.Text = "La modificación de Minuta ha fallado";
                    alerta.Visible = true;
                }
                */
        }


        //fin editar

        //eliminar
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            short id_pension = short.Parse(btn.CommandArgument);

            Minuta minuta = new Minuta();
            minuta.ID_PENSION = id_pension;

            //MiSesionMinuta = minuta;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal').modal();", true);
        }

        //fin eliminar
        /*
        //btn modal
         protected void btnModal_Click(object sender, EventArgs e)
        {

            try
=======
            if (s.EliminarMinuta(writer.ToString()) && s.EliminarDetallePlatos(writer.ToString()))
>>>>>>> 1f45958544927ca42e2107b7f622aea0a57325e6
            {
                MiSesionMinuta = null;
                Response.Write("<script language='javascript'>window.alert('La minuta ha sido Eliminada con éxito');window.location='../Administrador/WebVerMinuta.aspx';</script>");
                alerta.Visible = false;
            }
            else
            {
                error.Text = "No se ha podido Eliminar";
                alerta.Visible = true;
            }
        }
        
         //inicio info
          protected void btnInfo_Click(object sender, EventArgs e)
         {
             LinkButton btn = (LinkButton)(sender);
             short id_pension = short.Parse(btn.CommandArgument);

             Minuta minuta = new Minuta();
             minuta.ID_PENSION = id_pension;

             MiSesionMinuta = minuta;

             if (MiSesionMinuta.ID_PENSION != 0)
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
                 }
             }

             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
         }
         //fin info
         


        private void CargarTablaMinuta(List<DetallePlato> lista) {
            Minuta minuta = new Minuta();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID", typeof(long)),
                new DataColumn("Nombre", typeof(string)),
                
                new DataColumn("Precio",typeof(int))
            });
            foreach (DetallePlato item in lista) {
                minuta = new Minuta();
                minuta.ID_PENSION = item.ID_PENSION;
                //minuta.Read();

                dt.Rows.Add(minuta.ID_PENSION,minuta.NOMBRE_PENSION,minuta.VALOR_PENSION);
            }


            gvDetalleMinuta.DataSource = dt;
            gvDetalleMinuta.DataBind();
        }

        //fin tabla historial
        

        
         



    }
}