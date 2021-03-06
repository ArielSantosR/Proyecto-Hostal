﻿using Modelo;
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
    public partial class WebVerPais : System.Web.UI.Page
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

        public Pais MiSesionPais
        {
            get
            {
                if (Session["Pais"] == null)
                {
                    Session["Pais"] = new Pais();
                }
                return (Pais)Session["Pais"];
            }
            set
            {
                Session["Pais"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Service1 service = new Service1();
            string datos = service.ListarPais();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PaisCollection));
            StringReader reader = new StringReader(datos);

            Modelo.PaisCollection listaPais = (Modelo.PaisCollection)ser.Deserialize(reader);
            reader.Close();
            gvPais.DataSource = listaPais;
            gvPais.DataBind();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short ID_PAIS = short.Parse(btn.CommandArgument);

            Pais pais = new Pais();
            pais.ID_PAIS = ID_PAIS;
            MiSesionPais = pais;

            Response.Redirect("../Administrador/WebEditarPais.aspx");
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short ID_PAIS = short.Parse(btn.CommandArgument);

            Pais pais = new Pais();
            pais.ID_PAIS = ID_PAIS;
            MiSesionPais = pais;

            Response.Redirect("../Administrador/WebEliminarPais.aspx");
        }
    }
}