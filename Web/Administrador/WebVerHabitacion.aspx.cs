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

namespace Web.Administrador
{
    public partial class WebHabitaciones : System.Web.UI.Page
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
            Service1 service = new Service1();
            string datos = service.ListarHabitacion();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.HabitacionCollection));
            StringReader reader = new StringReader(datos);

            Modelo.HabitacionCollection listaHabitacion = (Modelo.HabitacionCollection)ser.Deserialize(reader);
            reader.Close();
            CargarGrid(listaHabitacion);
        }

        private void CargarGrid (HabitacionCollection listaHabitacion) {
            TipoHabitacion tipo;
            CategoriaHabitacion cat;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("NUMERO_HABITACION", typeof(int)),
                new DataColumn("ESTADO_HABITACION", typeof(string)),
                new DataColumn("TIPO",typeof(string)),
                new DataColumn("CATEGORIA",typeof(string))
            });

            foreach (Habitacion h in listaHabitacion) {
                tipo = new TipoHabitacion();
                tipo.ID_TIPO_HABITACION = h.ID_TIPO_HABITACION;
                tipo.BuscarTipo();

                cat = new CategoriaHabitacion();
                cat.ID_CATEGORIA_HABITACION = h.ID_CATEGORIA_HABITACION;
                cat.BuscarCategoria();

                dt.Rows.Add(h.NUMERO_HABITACION,h.ESTADO_HABITACION,tipo.NOMBRE_TIPO_HABITACION,cat.NOMBRE_CATEGORIA);
            }

            //Carga de GriedView
            gvHabitacion.DataSource = dt;
            gvHabitacion.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short NUMERO_HABITACION = short.Parse(btn.CommandArgument);

            Habitacion habitacion = new Habitacion();
            habitacion.NUMERO_HABITACION = NUMERO_HABITACION;
            MiSesionH = habitacion;

            Response.Redirect("../Administrador/WebEditarHabitacion.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short NUMERO_HABITACION = short.Parse(btn.CommandArgument);

            Habitacion habitacion = new Habitacion();
            habitacion.NUMERO_HABITACION = NUMERO_HABITACION;
            MiSesionH = habitacion;

            Response.Redirect("../Administrador/WebEliminarHabitacion.aspx");
        }

        protected void gvHabitacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHabitacion.PageIndex = e.NewPageIndex;
            gvHabitacion.DataBind();
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

        public Habitacion MiSesionH
        {
            get
            {
                if (Session["Habitacion"] == null)
                {
                    Session["Habitacion"] = new Habitacion();
                }
                return (Habitacion)Session["Habitacion"];
            }
            set
            {
                Session["Habitacion"] = value;
            }
        }
    }
}