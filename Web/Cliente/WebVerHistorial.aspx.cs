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
    public partial class WebVerHistorial : System.Web.UI.Page
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

                        //Si el ID de empleado es encontrado, pasar al siguiente paso
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

                        string datos = service.HistorialReserva(writer2.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.OrdenCompraCollection listaPedido = (Modelo.OrdenCompraCollection)ser3.Deserialize(reader);
                        reader.Close();
                        gvOrden.DataSource = listaPedido;
                        gvOrden.DataBind();
                    }
                    //Else si es el administrador deberia decir algo supongo
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

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal').modal();", true);
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
    }
}