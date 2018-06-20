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
    public partial class WebRegistrarOrden : System.Web.UI.Page
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
                    string datos = s.ListarReservaAdmin();
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            short numero_orden = short.Parse(btn.CommandArgument);

            OrdenCompra orden = new OrdenCompra();
            orden.NUMERO_ORDEN = numero_orden;

            MiSesionOrden = orden;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal').modal();", true);
        }

        protected void btnInfo2_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                short numero_orden = short.Parse(btn.CommandArgument);

                OrdenCompra orden = new OrdenCompra();
                orden.NUMERO_ORDEN = numero_orden;

                MiSesionOrden = orden;

                if (MiSesionOrden.NUMERO_ORDEN != 0)
                {
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.OrdenCompra));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, orden);

                    if (s.ListarDetalleReserva(writer.ToString()) != null)
                    {
                        string datos = s.ListarDetalleReserva(writer.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetalleOrdenCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.DetalleOrdenCollection listaDetalle = (Modelo.DetalleOrdenCollection)ser3.Deserialize(reader);
                        reader.Close();
                        gvDetalle.DataSource = listaDetalle;
                        gvDetalle.DataBind();

                        MiSesionOrden = s.ObtenerReserva(writer.ToString());

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
            try
            {
                LinkButton btn = (LinkButton)(sender);
                short numero_orden = short.Parse(btn.CommandArgument);

                OrdenCompra orden = new OrdenCompra();
                orden.NUMERO_ORDEN = numero_orden;

                MiSesionOrden = orden;

                Service1 s = new Service1();
                XmlSerializer sr = new XmlSerializer(typeof(Modelo.OrdenCompra));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, orden);

                if (s.ObtenerReserva(writer.ToString()) != null)
                {
                    orden = s.ObtenerReserva(writer.ToString());
                    orden.ESTADO_ORDEN = "Aceptado";

                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.OrdenCompra));
                    StringWriter writer2 = new StringWriter();
                    sr2.Serialize(writer2, orden);

                    if (s.EditarEstadoReserva(writer2.ToString()))
                    {
                        MiSesionOrden = null;
                        Response.Write("<script language='javascript'>window.alert('Ha Aceptado la Reserva');window.location='../Empleado/WebAsignarHabitacion.aspx';</script>");
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "No se ha podido llevar a cabo la operacion";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Orden no encontrada";
                    alerta.Visible = true;
                }
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnModal_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtComentario.Text != string.Empty)
                {
                    Modelo.OrdenCompra orden = new Modelo.OrdenCompra();
                    orden.NUMERO_ORDEN = MiSesionOrden.NUMERO_ORDEN;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.OrdenCompra));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, orden);

                    if (s.ObtenerReserva(writer.ToString()) != null)
                    {
                        orden = s.ObtenerReserva(writer.ToString());
                        orden.COMENTARIO = txtComentario.Text;
                        orden.ESTADO_ORDEN = "Rechazado";

                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.OrdenCompra));
                        StringWriter writer2 = new StringWriter();
                        sr2.Serialize(writer2, orden);

                        if (s.EditarEstadoReserva(writer2.ToString()))
                        {
                            MiSesionOrden = null;

                            Response.Write("<script language='javascript'>window.alert('Ha Rechazado la Reserva');window.location='../Empleado/WebReservasPendientes.aspx';</script>");

                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "No se ha podido llevar a cabo la operacion";
                            alerta.Visible = true;
                        }
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "Orden no encontrada";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Antes de rechazar la reserva debe mencionar las razones";
                    alerta.Visible = true;
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

        public OrdenCompra MiSesionOrden
        {
            get
            {
                if (Session["OrdenCompra"] == null)
                {
                    Session["OrdenCompra"] = new OrdenCompra();
                }
                return (OrdenCompra)Session["OrdenCompra"];
            }
            set
            {
                Session["OrdenCompra"] = value;
            }
        }
    }
}