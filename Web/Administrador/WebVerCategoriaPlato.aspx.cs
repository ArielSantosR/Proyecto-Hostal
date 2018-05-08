using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using WcfNegocio;

namespace Web.Administrador
{
    public partial class WebVerCategoriaPlato : System.Web.UI.Page
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

        public Categoria MiSesionCategoria
        {
            get
            {
                if (Session["Categoria"] == null)
                {
                    Session["Categoria"] = new Categoria();
                }
                return (Categoria)Session["Categoria"];
            }
            set
            {
                Session["Categoria"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Service1 service = new Service1();
            string datos = service.ListarCategoria();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.CategoriaCollection));
            StringReader reader = new StringReader(datos);

            Modelo.CategoriaCollection listaCategoria = (Modelo.CategoriaCollection)ser.Deserialize(reader);
            reader.Close();
            gvCategoriaPlato.DataSource = listaCategoria;
            gvCategoriaPlato.DataBind();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short ID_CATEGORIA = short.Parse(btn.CommandArgument);

            Categoria categoria = new Categoria();
            categoria.ID_CATEGORIA = ID_CATEGORIA;
            MiSesionCategoria = categoria;

            Response.Redirect("../Administrador/WebEditarCategoriaPlato.aspx");
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short ID_CATEGORIA = short.Parse(btn.CommandArgument);

            Categoria categoria = new Categoria();
            categoria.ID_CATEGORIA = ID_CATEGORIA;
            MiSesionCategoria = categoria;

            Response.Redirect("../Administrador/WebEliminarCategoriaPlato.aspx");
        }
    }
}