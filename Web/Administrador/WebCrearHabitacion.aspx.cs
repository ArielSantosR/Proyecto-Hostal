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
    public partial class WebCrearHabitacion : System.Web.UI.Page
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
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;

            //Cargando DDL Tipo Plato
            Service1 service = new Service1();
            string tipo_habitacion = service.ListarTipoHabitacion();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoHabitacionCollection));
            StringReader reader = new StringReader(tipo_habitacion);
            Modelo.TipoHabitacionCollection coleccionTipoHabitacion = (Modelo.TipoHabitacionCollection)ser.Deserialize(reader);
            reader.Close();


            if (!IsPostBack)
            {
                ddlTipo.DataSource = coleccionTipoHabitacion;
                ddlTipo.DataTextField = "NOMBRE_TIPO_HABITACION";
                ddlTipo.DataValueField = "ID_TIPO_HABITACION";
                ddlTipo.DataBind();

                ddlEstado.Items.Add("Disponible");
                ddlEstado.Items.Add("Ocupada");
                ddlEstado.Items.Add("Mantenimiento");
            }

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

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                short numero = 0;
                int precio = 0;

                if (txtNumero.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    if (short.TryParse(txtNumero.Text, out numero) && int.TryParse(txtPrecio.Text, out precio))
                    {
                        Modelo.Habitacion habitacion = new Modelo.Habitacion();
                        habitacion.NUMERO_HABITACION = numero;
                        habitacion.PRECIO_HABITACION = precio;
                        habitacion.ESTADO_HABITACION = ddlEstado.SelectedValue;
                        habitacion.ID_TIPO_HABITACION = short.Parse(ddlTipo.SelectedValue);

                        Service1 s = new Service1();
                        XmlSerializer sr = new XmlSerializer(typeof(Modelo.Habitacion));
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, habitacion);

                        if (!s.ExisteHabitacion(writer.ToString()))
                        {
                            if (s.AgregarHabitacion(writer.ToString()))
                            {
                                exito.Text = "La habitación ha sido creada con éxito";
                                alerta_exito.Visible = true;
                                alerta.Visible = false;
                            }
                            else
                            {
                                alerta_exito.Visible = false;
                                error.Text = "No se ha podido agregar";
                                alerta.Visible = true;
                            }
                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "Datos Ingresados incorrectamente, verifique que ha ingresado numeros correctamente";
                            alerta.Visible = true;
                        }
                        
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "Datos Ingresados incorrectamente, verifique que ha ingresado numeros correctamente";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Debe llenar todos los datos";
                    alerta.Visible = true;
                }

            } 
            catch (Exception)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepcion";
                alerta.Visible = true;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNumero.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            ddlEstado.SelectedIndex = 0;
            ddlTipo.SelectedIndex = 0;
        }
    }
}