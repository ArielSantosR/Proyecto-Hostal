using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Empleado
{
    public partial class WebCrearFactura : System.Web.UI.Page
    {
        private List<Comuna> coleccionComuna;
        private List<Region> coleccionRegion;
        void Page_PreInit(object sender, EventArgs e)
        {
            if (MiSesion != null)
            {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null)
                {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString()))
                    {
                        MasterPageFile = "~/Administrador/AdminM.Master";
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Empleado.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString()))
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
            try {
                error.Text = "";
                exito.Text = "";
                alerta_exito.Visible = false;
                alerta.Visible = false;

                coleccionComuna = ComunaCollection.ListaComuna();
                coleccionRegion = RegionCollection.ListaRegion();

                if (!IsPostBack) {
                    ddlGiro.DataSource = GiroCollection.ListaGiro();
                    ddlGiro.DataTextField = "NOMBRE_GIRO";
                    ddlGiro.DataValueField = "ID_GIRO";
                    ddlGiro.DataBind();
                    ddlGiro.Items.Insert(0,new ListItem("Seleccione Giro...","0"));

                    Comuna com = new Comuna();
                    com.Id_Comuna = SesionCli.ID_COMUNA;
                    com.BuscarComuna();

                    Region reg = new Region();
                    reg.Id_Region = com.Id_Region;
                    reg.BuscarRegion();

                    Pais pais = new Pais();
                    pais.ID_PAIS = reg.Id_Pais;
                    pais.BuscarPais();

                    ddlPais.DataSource = PaisCollection.ListaPais();
                    ddlPais.DataTextField = "NOMBRE_PAIS";
                    ddlPais.DataValueField = "ID_PAIS";
                    ddlPais.DataBind();
                    ddlPais.Items.Insert(0,new ListItem("Seleccione País...","0"));

                    ddlRegion.Items.Insert(0,new ListItem("Seleccione Región...","0"));
                    ddlComuna.Items.Insert(0,new ListItem("Seleccione Comuna...","0"));

                    if (SesionOrden.NUMERO_ORDEN != 0) {
                        txtRut.Text = SesionCli.RUT_CLIENTE + "-" + SesionCli.DV_CLIENTE;
                        txtDireccion.Text = SesionCli.DIRECCION_CLIENTE;
                        txtNombre.Text = SesionCli.NOMBRE_CLIENTE;
                        txtOrden.Text = SesionOrden.NUMERO_ORDEN.ToString();
                        txtTelefono.Text = SesionCli.TELEFONO_CLIENTE.ToString();
                        ddlPais.SelectedValue = pais.ID_PAIS.ToString();
                        FiltrarRegion();
                        ddlRegion.SelectedValue = reg.Id_Region.ToString();
                        FiltrarComuna();
                        ddlComuna.SelectedValue = com.Id_Comuna.ToString();
                        ddlGiro.SelectedValue = SesionCli.ID_GIRO.ToString();
                    }

                    CargarGrid(SesionOrden);
                }
            }
            catch (Exception) {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
            }

        }

        private void CargarGrid(OrdenCompra sesionOrden) {
            List<DetalleOrden> detalles = DetalleOrdenCollection.ListarDetalles().Where(x => x.NUMERO_ORDEN == sesionOrden.NUMERO_ORDEN).ToList();

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("Cantidad", typeof(int)),
                new DataColumn("Descripcion", typeof(string)),
                new DataColumn("Descuento",typeof(int)),
                new DataColumn("ValorUni",typeof(int)),
                new DataColumn("Total",typeof(int))
            });

            //Carga de datos en DataTable
            foreach (DetalleOrden d in detalles) {
                //dt.Rows.Add(GenerarCantidad(),d.NOMBRE_CLIENTE,o.FECHA_LLEGADA.ToShortDateString(),o.FECHA_SALIDA.ToShortDateString(),"$" + o.MONTO_TOTAL);
            }

            //Carga de GriedView
            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
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

        public Modelo.Cliente SesionCli {
            get {
                if (Session["Cliente"] == null) {
                    Session["Cliente"] = new Modelo.Cliente();
                }
                return (Modelo.Cliente)Session["Cliente"];
            }
            set {
                Session["Cliente"] = value;
            }
        }

        public OrdenCompra SesionOrden {
            get {
                if (Session["Orden"] == null) {
                    Session["Orden"] = new OrdenCompra();
                }
                return (OrdenCompra)Session["Orden"];
            }
            set {
                Session["Orden"] = value;
            }
        }

        protected void ddlPais_SelectedIndexChanged(object sender,EventArgs e) {
            FiltrarRegion();
        }

        protected void ddlRegion_SelectedIndexChanged(object sender,EventArgs e) {
            FiltrarComuna();
        }

        private void FiltrarComuna() {
            ddlComuna.DataSource = coleccionComuna.Where(x => x.Id_Region == int.Parse(ddlRegion.SelectedValue));
            ddlComuna.DataTextField = "Nombre";
            ddlComuna.DataValueField = "Id_Comuna";
            ddlComuna.DataBind();
            ddlComuna.Items.Insert(0,new ListItem("Selecione Comuna...","0"));
        }

        private void FiltrarRegion() {
            ddlRegion.DataSource = coleccionRegion.Where(x => x.Id_Pais == int.Parse(ddlPais.SelectedValue));
            ddlRegion.DataTextField = "Nombre";
            ddlRegion.DataValueField = "Id_Region";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0,new ListItem("Seleccione Región...","0"));
        }
    }
}