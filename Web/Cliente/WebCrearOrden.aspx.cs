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

namespace Web.Cliente
{
    public partial class WebCrearOrden : System.Web.UI.Page
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
                    else if (MiSesion.TIPO_USUARIO.Equals("Cliente") &&
                    MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        MasterPageFile = "~/Cliente/ClienteM.Master";
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

            Service1 service = new Service1();

            Modelo.Cliente cliente = new Modelo.Cliente();

            try
            {
                if (MiSesion != null)
                {
                    if (MiSesion.TIPO_USUARIO.Equals("Cliente"))
                    {
                        cliente.ID_USUARIO = MiSesion.ID_USUARIO;

                        //Si el ID de Cliente es encontrado, pasar al siguiente paso
                        Service1 s = new Service1();
                        XmlSerializer sr = new XmlSerializer(typeof(Modelo.Cliente));
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, cliente);
                        writer.Close();

                        Modelo.Cliente cliente2 = s.buscarIDC(writer.ToString());
                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Cliente));
                        StringWriter writer2 = new StringWriter();
                        sr2.Serialize(writer2, cliente2);
                        writer2.Close();

                        string datos = service.ListarHuespedService(writer2.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.HuespedCollection));
                        StringReader reader = new StringReader(datos);
                        Modelo.HuespedCollection coleccionHuesped = (Modelo.HuespedCollection)ser3.Deserialize(reader);

                        string categoria_habitacion = service.ListarCategoriaHabitacion();
                        XmlSerializer ser1 = new XmlSerializer(typeof(Modelo.CategoriaHabitacionCollection));
                        StringReader reader1 = new StringReader(categoria_habitacion);
                        Modelo.CategoriaHabitacionCollection coleccionCategoria = (Modelo.CategoriaHabitacionCollection)ser1.Deserialize(reader1);
                        reader.Close();

                        if (!IsPostBack)
                        {
                            ddlRut.DataSource = coleccionHuesped;
                            ddlRut.DataValueField = "RUT_HUESPED";
                            ddlRut.DataTextField = "RutYNombre";
                            ddlRut.DataBind();

                            ddlCategoria.DataSource = coleccionCategoria;
                            ddlCategoria.DataTextField = "NOMBRE_CATEGORIA";
                            ddlCategoria.DataValueField = "ID_CATEGORIA_HABITACION";
                            ddlCategoria.DataBind();
                        }
                    }
                    //Else Pendiente
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
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
    }
}