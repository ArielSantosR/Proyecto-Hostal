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
    public partial class WebAsignacion : System.Web.UI.Page
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
                error.Text = "";
                exito.Text = "";
                alerta_exito.Visible = false;
                alerta.Visible = false;

                if (MiSesionOrden.NUMERO_ORDEN != 0)
                {
                    OrdenCompra orden = MiSesionOrden;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.OrdenCompra));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, orden);

                    if (s.ListaHuespedesNoAsignados(writer.ToString()) != null)
                    {
                        string datos = s.ListaHuespedesNoAsignados(writer.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetalleOrdenCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.DetalleOrdenCollection listaDetalle = (Modelo.DetalleOrdenCollection)ser3.Deserialize(reader);
                        reader.Close();
                        gvDetalle.DataSource = listaDetalle;
                        gvDetalle.DataBind();

                        MiSesionOrden = s.ObtenerReserva(writer.ToString());
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Necesita asignar una orden de Habitación');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
            catch (Exception ex)
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

        public DetalleOrden MiSesionDetalleO
        {
            get
            {
                if (Session["DetalleOrden"] == null)
                {
                    Session["DetalleOrden"] = new DetalleOrden();
                }
                return (DetalleOrden)Session["DetalleOrden"];
            }
            set
            {
                Session["DetalleOrden"] = value;
            }
        }

        //Al Presionar Asignar
        protected void btnInfo2_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                short ID_DETALLE = short.Parse(btn.CommandArgument);

                DetalleOrden detalle = new DetalleOrden();
                detalle.ID_DETALLE = ID_DETALLE;

                MiSesionDetalleO = detalle;

                if (MiSesionDetalleO.ID_DETALLE != 0)
                {
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.DetalleOrden));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, detalle);

                    if (s.ObtenerDetalleReserva(writer.ToString()) != null)
                    {
                        MiSesionDetalleO = s.ObtenerDetalleReserva(writer.ToString());

                        DetalleOrden detalle2 = MiSesionDetalleO;
                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.DetalleOrden));
                        StringWriter writer2 = new StringWriter();
                        sr.Serialize(writer2, detalle2);

                        if (s.ListarHabitacionDisponibleCategoria(writer2.ToString()) != null)
                        {
                            string habitacion = s.ListarHabitacionDisponibleCategoria(writer2.ToString());
                            XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.HabitacionCollection));
                            StringReader reader3 = new StringReader(habitacion);
                            Modelo.HabitacionCollection coleccionHabitacion = (Modelo.HabitacionCollection)ser3.Deserialize(reader3);
                            reader3.Close();

                            ddlHabitacion.DataSource = coleccionHabitacion;
                            ddlHabitacion.DataTextField = "DatosHabitacion";
                            ddlHabitacion.DataValueField = "NUMERO_HABITACION";
                            ddlHabitacion.DataBind();
                        }
                        else if (s.ListarHabitacionDisponible(writer2.ToString()) != null)
                        {
                            string habitacion = s.ListarHabitacionDisponible(writer2.ToString());
                            XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.HabitacionCollection));
                            StringReader reader3 = new StringReader(habitacion);
                            Modelo.HabitacionCollection coleccionHabitacion = (Modelo.HabitacionCollection)ser3.Deserialize(reader3);
                            reader3.Close();

                            ddlHabitacion.DataSource = coleccionHabitacion;
                            ddlHabitacion.DataTextField = "DatosHabitacion";
                            ddlHabitacion.DataValueField = "NUMERO_HABITACION";
                            ddlHabitacion.DataBind();

                            Response.Write("<script language=javascript>alert('No quedan habitaciones de esa categoría disponible. Tendrá que elegir otra categoría')</script>");
                        }
                        else
                        {
                            Response.Write("<script language=javascript>alert('No hay ninguna habitación disponible. tendrá que rechazar la reserva')</script>");
                        }

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
            }
        }

        //Al Rechazar
        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        //Al Asignar la habitación finalmente
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //1- Cambiando el estado de Habitación a Ocupado
                Habitacion habitacion = new Habitacion();
                habitacion.NUMERO_HABITACION = short.Parse(ddlHabitacion.SelectedValue);

                Service1 s = new Service1();
                XmlSerializer sr = new XmlSerializer(typeof(Modelo.Habitacion));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, habitacion);

                if (s.ObtenerHabitacion(writer.ToString()) != null)
                {
                    habitacion = s.ObtenerHabitacion(writer.ToString());
                    habitacion.ESTADO_HABITACION = "Ocupado";

                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Habitacion));
                    StringWriter writer2 = new StringWriter();
                    sr2.Serialize(writer2, habitacion);

                    if (s.ModificarHabitacion(writer2.ToString()))
                    {
                        //2- Agregando el detalle de habitación junto con la fecha de entrada y salida

                        //3- Agregando el detalle de pasajeros de la habitación

                        //4- Modificando el estado del detalle de orden a Asignado

                        //5- Modificando el estado de la orden de compra a Asignado
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "No se pudo modificar la habitación";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "No se pudo encontrar la habitación";
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
    }
}