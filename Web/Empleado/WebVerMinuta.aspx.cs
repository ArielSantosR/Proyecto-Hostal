using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfNegocio;
using Modelo;
using System.Xml.Serialization;
using System.IO;

namespace Web.Empleado
{
    public partial class WebVerMinuta : System.Web.UI.Page
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
       

        protected void Page_Load(object sender, EventArgs e)
        {
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;

            Service1 service = new Service1();
            string datos = service.ListarMinuta();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.MinutaCollection));
            StringReader reader = new StringReader(datos);
            
            Modelo.MinutaCollection listaMinuta = (Modelo.MinutaCollection)ser.Deserialize(reader);
            reader.Close();
            gvMinuta.DataSource = listaMinuta;
            gvMinuta.DataBind();
        }
        protected void gvMinuta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMinuta.PageIndex = e.NewPageIndex;
            gvMinuta.DataBind();
        }
        
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short id_pension = short.Parse(btn.CommandArgument);

            Minuta minuta = new Minuta();
            minuta.ID_PENSION = id_pension;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Minuta));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, minuta);

            /*
            if (s.ObtenerMinuta(writer.ToString()) == null)
            {
                alerta_exito.Visible = false;
                error.Text = "No se ha encontrado la Minuta";
                alerta.Visible = true;
            }
            else {
                    alerta_exito.Visible = false;
                    error.Text = "La modificación de Minuta ha fallado";
                    alerta.Visible = true;
                }
                */
        }


        //fin editar

        //eliminar
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            short id_pension = short.Parse(btn.CommandArgument);

            Minuta minuta = new Minuta();
            minuta.ID_PENSION = id_pension;

            //MiSesionMinuta = minuta;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal').modal();", true);
        }

        //fin eliminar
        /*
        //btn modal
         protected void btnModal_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtComentario.Text != string.Empty)
                {
                    Modelo.Pedido pedido = new Modelo.Pedido();
                    pedido.NUMERO_PEDIDO = MiSesionPedido.NUMERO_PEDIDO;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Pedido));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, pedido);

                    if (s.ObtenerPedido(writer.ToString()) != null)
                    {
                        pedido = s.ObtenerPedido(writer.ToString());
                        pedido.COMENTARIO = txtComentario.Text;
                        pedido.ESTADO_PEDIDO = "No Recepcionado";

                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                        StringWriter writer2 = new StringWriter();
                        sr2.Serialize(writer2, pedido);

                        if (s.EditarEstadoPedido(writer2.ToString()))
                        {
                            MiSesionPedido = null;

                            Response.Write("<script language='javascript'>window.alert('Ha Rechazado el pedido');window.location='../Administrador/WebVerPedido.aspx';</script>");

                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "No se ha podido modificar el Estado de Producto";
                            alerta.Visible = true;
                        }
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "Pedido no encontrado";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Antes de rechazar el pedido debe mencionar las razones";
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
        //fin btn modal

        //inicio info
         protected void btnInfo_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            short id_pension = short.Parse(btn.CommandArgument);

            Minuta minuta = new Minuta();
            minuta.ID_PENSION = id_pension;

            MiSesionMinuta = minuta;

            if (MiSesionMinuta.ID_PENSION != 0)
            {

                Service1 s = new Service1();
                XmlSerializer sr = new XmlSerializer(typeof(Modelo.Minuta));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, minuta);

                if (s.ListarDetalleMinuta(writer.ToString()) != null)
                {
                    string datos = s.ListarDetalleMinuta(writer.ToString());
                    XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetalleMinutaCollection));
                    StringReader reader = new StringReader(datos);

                    Modelo.DetalleMinutaCollection listaMinuta = (Modelo.DetalleMinutaCollection)ser3.Deserialize(reader);
                    reader.Close();
                    CargarTablaMinuta(listaMinuta);
                }
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
        }
        //fin info
        */
         
        /*
        private void CargarTablaMinuta(List<DetalleMinuta> lista) {
            Minuta minuta = new Minuta();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("Código", typeof(long)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Descripción",typeof(string)),
                new DataColumn("Unidad Medida",typeof(string)),
                new DataColumn("Cantidad Pedida",typeof(int))
            });
            foreach (DetalleMinuta item in lista) {
                minuta = new Minuta();
                minuta.ID_PENSION = item.ID_PENSION;
                minuta.Read();

                dt.Rows.Add(producto.ID_PRODUCTO,producto.NOMBRE_PRODUCTO,producto.DESCRIPCION_PRODUCTO,producto.UNIDAD_MEDIDA,item.CANTIDAD);
            }


            gvDetalleMinuta.DataSource = dt;
            gvDetalleMinuta.DataBind();
        }

        //fin tabla historial
        */

        
         



    }
}