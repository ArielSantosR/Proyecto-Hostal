using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using WcfNegocio;

namespace Web.Administrador
{
    public partial class WebUsuarios : System.Web.UI.Page
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
            CargarGriedView();
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


        
        private void CargarGriedView() {
            //Carga listas con datos
            List<Usuario> users = UsuarioCollection.ListaUsuarios();
            List<Modelo.Empleado> empleados = EmpleadoCollection.ListaEmpleados();
            List<Modelo.Cliente> clientes = ClienteCollection.ListaClientes();
            List<Modelo.Proveedor> proveedores = ProveedorCollection.ListaProveedores();

            Usuario user;
            
            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn("ID", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Usuario",typeof(string)),
                new DataColumn("Tipo",typeof(string)),
                new DataColumn("Estado",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (Modelo.Cliente c in clientes) {
                user = new Usuario();
                var list = users.Where(x => x.ID_USUARIO == c.ID_USUARIO).ToList();
                foreach (var item in list) {
                    user.NOMBRE_USUARIO = item.NOMBRE_USUARIO;
                    user.TIPO_USUARIO = item.TIPO_USUARIO;
                    user.ESTADO = item.ESTADO;
                }
                dt.Rows.Add(c.ID_USUARIO,c.NOMBRE_CLIENTE,user.NOMBRE_USUARIO,user.TIPO_USUARIO,user.ESTADO);
            }

            foreach (Modelo.Empleado e in empleados) {
                user = new Usuario();
                var list = users.Where(x => x.ID_USUARIO == e.ID_USUARIO).ToList();
                foreach (var item in list) {
                    user.NOMBRE_USUARIO = item.NOMBRE_USUARIO;
                    user.TIPO_USUARIO = item.TIPO_USUARIO;
                    user.ESTADO = item.ESTADO;
                }
                dt.Rows.Add(e.ID_USUARIO,e.PNOMBRE_EMPLEADO+ " " + e.APP_PATERNO_EMPLEADO + " " + e.APP_MATERNO_EMPLEADO,user.NOMBRE_USUARIO,user.TIPO_USUARIO,user.ESTADO);
            }

            foreach (Modelo.Proveedor p in proveedores) {
                user = new Usuario();
                var list = users.Where(x => x.ID_USUARIO == p.ID_USUARIO).ToList();
                foreach (var item in list) {
                    user.NOMBRE_USUARIO = item.NOMBRE_USUARIO;
                    user.TIPO_USUARIO = item.TIPO_USUARIO;
                    user.ESTADO = item.ESTADO;
                }
                dt.Rows.Add(p.ID_USUARIO,p.PNOMBRE_PROVEEDOR + " " + p.APP_PATERNO_PROVEEDOR + " " + p.APP_MATERNO_PROVEEDOR,user.NOMBRE_USUARIO,user.TIPO_USUARIO,user.ESTADO);
            }

            //Carga de GriedView
            gvUsuario.DataSource = dt;
            gvUsuario.DataBind();
        }

        protected void gvUsuario_RowEditing(object sender,GridViewEditEventArgs e) {
            string tipo = gvUsuario.DataKeys[e.NewEditIndex].Value.ToString();
            if (tipo.Equals(Tipo_Usuario.Administrador.ToString()) || tipo.Equals(Tipo_Usuario.Empleado.ToString())) {
                Response.Redirect("../Empleado/WebEditarEmpleado.aspx");
            }
            if (tipo.Equals(Tipo_Usuario.Administrador.ToString()) || tipo.Equals(Tipo_Usuario.Empleado.ToString())) {
                Response.Redirect("../Cliente/WebEditarCliente.aspx");
            }
            if (tipo.Equals(Tipo_Usuario.Administrador.ToString()) || tipo.Equals(Tipo_Usuario.Empleado.ToString())) {
                Response.Redirect("../Proveedor/WebEditarProveedor.aspx");
            }
        }
    }
}