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
    public partial class WebVerTipoPlato : System.Web.UI.Page
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

        public TipoPlato MiSesionPlato
        {
            get
            {
                if (Session["Plato"] == null)
                {
                    Session["Plato"] = new Plato();
                }
                return (TipoPlato)Session["Plato"];
            }
            set
            {
                Session["Plato"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Service1 service = new Service1();
            string datos = service.ListarTipoPlato();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlatoCollection));
            StringReader reader = new StringReader(datos);

            Modelo.TipoPlatoCollection listaTipoPlato = (Modelo.TipoPlatoCollection)ser.Deserialize(reader);
            reader.Close();
            gvTipoPlato.DataSource = listaTipoPlato;
            gvTipoPlato.DataBind();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short ID_TIPO_PLATO = short.Parse(btn.CommandArgument);

            TipoPlato tipoPlato = new TipoPlato();
            tipoPlato.ID_TIPO_PLATO = ID_TIPO_PLATO;
            MiSesionPlato = tipoPlato;

            Response.Redirect("../Administrador/WebEditarTipoPlato.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short ID_TIPO_PLATO = short.Parse(btn.CommandArgument);

            TipoPlato tipoPlato = new TipoPlato();
            tipoPlato.ID_TIPO_PLATO = ID_TIPO_PLATO;
            MiSesionPlato = tipoPlato;

            Response.Redirect("../Administrador/WebEliminarTipoPlato.aspx");
        }
    }
}