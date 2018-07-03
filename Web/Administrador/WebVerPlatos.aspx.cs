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

namespace Web
{
    public partial class WebVerPlatos : System.Web.UI.Page
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
            string datos = service.ListarPlato();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PlatoCollection));
            StringReader reader = new StringReader(datos);

            Modelo.PlatoCollection listaPlato = (Modelo.PlatoCollection)ser.Deserialize(reader);
            reader.Close();
            CargarGrid(listaPlato);
        }

        private void CargarGrid (PlatoCollection listaPlato) {
            TipoPlato tipo;
            Categoria cat;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID_PLATO", typeof(int)),
                new DataColumn("NOMBRE_PLATO", typeof(string)),
                new DataColumn("PRECIO_PLATO",typeof(string)),
                new DataColumn("CATEGORIA",typeof(string)),
                new DataColumn("TIPO_PLATO",typeof(string))
            });

            foreach (Plato p in listaPlato) {
                tipo = new TipoPlato();
                tipo.ID_TIPO_PLATO = p.ID_TIPO_PLATO;
                tipo.BuscarTipo();

                cat = new Categoria();
                cat.ID_CATEGORIA = p.ID_CATEGORIA;
                cat.BuscarCategoria();

                dt.Rows.Add(p.ID_PLATO,p.NOMBRE_PLATO,"$" + p.PRECIO_PLATO,tipo.NOMBRE_TIPO_PLATO,cat.NOMBRE_CATEGORIA);
            }

            //Carga de GriedView
            gvPlato.DataSource = dt;
            gvPlato.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short ID_PLATO = short.Parse(btn.CommandArgument);

            Plato plato = new Plato();
            plato.ID_PLATO = ID_PLATO;
            MiSesionPlato = plato;

            Response.Redirect("../Administrador/WebEditarPlato.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short ID_PLATO = short.Parse(btn.CommandArgument);

            Plato plato = new Plato();
            plato.ID_PLATO = ID_PLATO;
            MiSesionPlato = plato;

            Response.Redirect("../Administrador/WebEliminarPlato.aspx");
        }

        protected void gvPlato_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlato.PageIndex = e.NewPageIndex;
            gvPlato.DataBind();
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

        public Plato MiSesionPlato
        {
            get
            {
                if (Session["Plato"] == null)
                {
                    Session["Plato"] = new Plato();
                }
                return (Plato)Session["Plato"];
            }
            set
            {
                Session["Plato"] = value;
            }
        }
    }
}