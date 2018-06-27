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

namespace Web.Empleado
{
    public partial class WebClientesAsignados : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (MiSesion != null)
                {
                    error.Text = "";
                    exito.Text = "";
                    alerta_exito.Visible = false;
                    alerta.Visible = false;

                    Service1 s = new Service1();
                    string datos = s.ListarReservaAsignada();
                    XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
                    StringReader reader = new StringReader(datos);

                    Modelo.OrdenCompraCollection listaOrden = (Modelo.OrdenCompraCollection)ser.Deserialize(reader);
                    reader.Close();
                    gvOrden.DataSource = listaOrden;
                    gvOrden.DataBind();
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
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

        public Modelo.Cliente MiSesionCl
        {
            get
            {
                if (Session["Cliente"] == null)
                {
                    Session["Cliente"] = new Modelo.Cliente();
                }
                return (Modelo.Cliente)Session["Cliente"];
            }
            set
            {
                Session["Cliente"] = value;
            }
        }

        public DetalleHabitacion misesionDH
        {
            get
            {
                if (Session["DetalleHabitacion"] == null)
                {
                    Session["DetalleHabitacion"] = new Modelo.DetalleHabitacion();
                }
                return (Modelo.DetalleHabitacion)Session["DetalleHabitacion"];
            }
            set
            {
                Session["DetalleHabitacion"] = value;
            }

        }

        protected void btnInfo_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                int rut_cliente = int.Parse(btn.CommandArgument);

                Modelo.Cliente cliente = new Modelo.Cliente();
                cliente.RUT_CLIENTE = rut_cliente;

                MiSesionCl = cliente;

                if (MiSesionCl.RUT_CLIENTE != 0)
                {
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Cliente));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, cliente);

                    if (s.ListaDetalleHabitacionCliente(writer.ToString()) != null)
                    {
                        string datos = s.ListaDetalleHabitacionCliente(writer.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetalleHabitacionCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.DetalleHabitacionCollection listaDetalle = (Modelo.DetalleHabitacionCollection)ser3.Deserialize(reader);
                        reader.Close();
                        gvDetalle.DataSource = listaDetalle;
                        gvDetalle.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Service1 s = new Service1();

            LinkButton btn = (LinkButton)(sender);
            short id_detalle_h = short.Parse(btn.CommandArgument);

            DetalleHabitacion detalle = new DetalleHabitacion();
            detalle.ID_DETALLE_H = id_detalle_h;

            misesionDH = detalle;

            DetalleHabitacion detalle2 = misesionDH;
            XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.DetalleHabitacion));
            StringWriter writer2 = new StringWriter();
            sr2.Serialize(writer2, detalle2);

            string habitacion2 = s.ListarHabitacionDisponible(writer2.ToString());
            XmlSerializer ser4 = new XmlSerializer(typeof(Modelo.HabitacionCollection));
            StringReader reader4 = new StringReader(habitacion2);
            Modelo.HabitacionCollection coleccionHabitacion = (Modelo.HabitacionCollection)ser4.Deserialize(reader4);
            reader4.Close();

            ddlHabitacion.DataSource = coleccionHabitacion;
            ddlHabitacion.DataTextField = "DatosHabitacion";
            ddlHabitacion.DataValueField = "NUMERO_HABITACION";
            ddlHabitacion.DataBind();

            UpdatePanel2.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal').modal();", true);
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            
        }
    }
}