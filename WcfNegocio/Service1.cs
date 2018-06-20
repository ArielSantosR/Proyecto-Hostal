using Datos;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Web;
using System.Web.Hosting;

namespace WcfNegocio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código y en el archivo de configuración a la vez.
    public class Service1 : IService1
    {
        #region Usuario

        //CRUD Usuario
        public bool Login(string user)
        {
            //El Xml Lee los datos pasados del login Web y procede a leerlos con reader
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario usuario = (Modelo.Usuario)ser.Deserialize(reader);

            //Agregado Datos y Modelo a las clases para saber de donde proceden
            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.NOMBRE_USUARIO = usuario.NOMBRE_USUARIO;
            uDatos.PASSWORD = usuario.PASSWORD;
            ServicioLogin servicio = new ServicioLogin();
            return servicio.Login(uDatos);
        }

        //Registro Usuario
        public Usuario GetUsuario(string user)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario usuario = (Modelo.Usuario)ser.Deserialize(reader);

            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.NOMBRE_USUARIO = usuario.NOMBRE_USUARIO;
            uDatos.PASSWORD = usuario.PASSWORD;

            ServicioUsuario serv = new ServicioUsuario();
            Datos.USUARIO uDatos2 = serv.EncontrarUsuario(uDatos);
            usuario.NOMBRE_USUARIO = uDatos2.NOMBRE_USUARIO;
            usuario.PASSWORD = uDatos2.PASSWORD;
            usuario.TIPO_USUARIO = uDatos2.TIPO_USUARIO;
            usuario.ESTADO = uDatos2.ESTADO;
            usuario.ID_USUARIO = uDatos2.ID_USUARIO;

            return usuario;
        }

        public bool ExisteUsuario(string user)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario usuario = (Modelo.Usuario)ser.Deserialize(reader);
            ServicioUsuario serv = new ServicioUsuario();
            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.NOMBRE_USUARIO = usuario.NOMBRE_USUARIO;
            if (!serv.ExisteUsuario(uDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RegistroUsuario(string user)
        {
            //Datos Usuario
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario usuario = (Modelo.Usuario)ser.Deserialize(reader);
            ServicioUsuario serv = new ServicioUsuario();
            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.NOMBRE_USUARIO = usuario.NOMBRE_USUARIO;
            uDatos.PASSWORD = usuario.PASSWORD;
            uDatos.TIPO_USUARIO = usuario.TIPO_USUARIO;
            uDatos.ESTADO = usuario.ESTADO;

            return serv.RegistrarUsuario(uDatos);
        }

        public bool EliminarUsuario(string user)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario usuario = (Modelo.Usuario)ser.Deserialize(reader);
            ServicioUsuario serv = new ServicioUsuario();
            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.ID_USUARIO = usuario.ID_USUARIO;

            return serv.RegistrarUsuario(uDatos);
        }

        public string ListarUsuarios()
        {
            ServicioUsuario servicio = new ServicioUsuario();
            List<Datos.USUARIO> usuario = servicio.ListarUsuarios();
            Modelo.UsuarioCollection listaUsuario = new Modelo.UsuarioCollection();

            foreach (Datos.USUARIO user in usuario)
            {
                Modelo.Usuario u = new Modelo.Usuario();
                u.ID_USUARIO = user.ID_USUARIO;
                u.NOMBRE_USUARIO = user.NOMBRE_USUARIO;
                u.PASSWORD = user.PASSWORD;
                u.TIPO_USUARIO = user.TIPO_USUARIO;
                u.ESTADO = user.ESTADO;

                listaUsuario.Add(u);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.UsuarioCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaUsuario);
            writer.Close();
            return writer.ToString();
        }

        public bool ModificarUsuario(string user)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario u = (Modelo.Usuario)ser.Deserialize(reader);
            Datos.ServicioUsuario servicio = new Datos.ServicioUsuario();
            Datos.USUARIO usuario = new Datos.USUARIO();
            //Datos de Usuario
            usuario.ID_USUARIO = u.ID_USUARIO;
            usuario.NOMBRE_USUARIO = u.NOMBRE_USUARIO;
            usuario.PASSWORD = u.PASSWORD;
            usuario.TIPO_USUARIO = u.TIPO_USUARIO;
            usuario.ESTADO = u.ESTADO;

            if (servicio.ModificarUsuario(usuario))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Huesped

        //  CRUD HUESPED
        public bool ExisteHuesped(string user)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Huesped));
            StringReader reader = new StringReader(user);
            Modelo.Huesped huesped = (Modelo.Huesped)ser.Deserialize(reader);
            ServicioHuesped serv = new ServicioHuesped();
            Datos.HUESPED uDatos = new Datos.HUESPED();
            uDatos.RUT_HUESPED = huesped.RUT_HUESPED;
            if (!serv.ExisteHuesped(uDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RegistroHuesped(string huesped)
        {
            //Datos huesped
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Huesped));
            StringReader reader = new StringReader(huesped);
            Modelo.Huesped hp = (Modelo.Huesped)ser.Deserialize(reader);
            ServicioHuesped serv = new ServicioHuesped();
            Datos.HUESPED uDatos = new Datos.HUESPED();
            uDatos.APP_MATERNO_HUESPED = hp.APP_MATERNO_HUESPED;
            uDatos.APP_PATERNO_HUESPED = hp.APP_PATERNO_HUESPED;
            uDatos.DV_HUESPED = hp.DV_HUESPED;
            uDatos.PNOMBRE_HUESPED = hp.PNOMBRE_HUESPED;
            uDatos.REGISTRADO = hp.REGISTRADO;
            uDatos.RUT_CLIENTE = hp.RUT_CLIENTE;
            uDatos.RUT_HUESPED = hp.RUT_HUESPED;
            uDatos.SNOMBRE_HUESPED = hp.SNOMBRE_HUESPED;
            uDatos.TELEFONO_HUESPED = hp.TELEFONO_HUESPED;

            return serv.RegistrarHuesped(uDatos);
        }

        public bool EliminarHuesped(string huesped)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Huesped));
            StringReader reader = new StringReader(huesped);
            Modelo.Huesped hp = (Modelo.Huesped)ser.Deserialize(reader);
            ServicioHuesped serv = new ServicioHuesped();
            Datos.HUESPED uDatos = new Datos.HUESPED();
            uDatos.RUT_HUESPED = hp.RUT_HUESPED;

            return serv.EliminarHuesped(uDatos);
        }

        public string ListarHuesped()
        {
            ServicioHuesped servicio = new ServicioHuesped();
            List<Datos.HUESPED> huesped = servicio.ListarHuesped();
            Modelo.HuespedCollection listaHuesped = new Modelo.HuespedCollection();

            foreach (Datos.HUESPED hp in huesped)
            {
                Modelo.Huesped h = new Modelo.Huesped();
                h.RUT_HUESPED = hp.RUT_HUESPED;
                h.DV_HUESPED = hp.DV_HUESPED;
                h.PNOMBRE_HUESPED = hp.PNOMBRE_HUESPED;
                h.SNOMBRE_HUESPED = hp.SNOMBRE_HUESPED;
                h.TELEFONO_HUESPED = hp.TELEFONO_HUESPED;
                h.APP_MATERNO_HUESPED = hp.APP_MATERNO_HUESPED;
                h.APP_PATERNO_HUESPED = hp.APP_PATERNO_HUESPED;
                h.REGISTRADO = hp.REGISTRADO;
                h.RUT_CLIENTE = hp.RUT_CLIENTE;

                listaHuesped.Add(h);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.HuespedCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaHuesped);
            writer.Close();
            return writer.ToString();
        }

        public bool ModificarHuesped(string huesped)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Huesped));
            StringReader reader = new StringReader(huesped);
            Modelo.Huesped hp = (Modelo.Huesped)ser.Deserialize(reader);
            Datos.ServicioHuesped servicio = new Datos.ServicioHuesped();
            Datos.HUESPED hues = new Datos.HUESPED();
            //Datos de Huesped
            hues.RUT_HUESPED = hp.RUT_HUESPED;
            hues.DV_HUESPED = hp.DV_HUESPED;
            hues.APP_PATERNO_HUESPED = hp.APP_PATERNO_HUESPED;
            hues.APP_MATERNO_HUESPED = hp.APP_MATERNO_HUESPED;
            hues.PNOMBRE_HUESPED = hp.PNOMBRE_HUESPED;
            hues.SNOMBRE_HUESPED = hp.SNOMBRE_HUESPED;
            hues.TELEFONO_HUESPED = hp.TELEFONO_HUESPED;
            hues.REGISTRADO = hp.REGISTRADO;
            hues.RUT_CLIENTE = hp.RUT_CLIENTE;


            if (servicio.ModificarHuesped(hues))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Cliente
        //CRUD Cliente
        public bool ExisteRutC(string cliente)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Cliente));
            StringReader reader = new StringReader(cliente);
            Modelo.Cliente c = (Modelo.Cliente)ser.Deserialize(reader);
            ServicioCliente serv = new ServicioCliente();
            Datos.CLIENTE cDatos = new Datos.CLIENTE();
            cDatos.RUT_CLIENTE = c.RUT_CLIENTE;

            if (!serv.ExisteRut(cDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RegistroCliente(string cliente)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Cliente));
            StringReader reader = new StringReader(cliente);
            Modelo.Cliente c = (Modelo.Cliente)ser.Deserialize(reader);
            ServicioCliente servicio = new ServicioCliente();

            Datos.CLIENTE cDatos = new Datos.CLIENTE();
            //Datos Cliente
            cDatos.RUT_CLIENTE = c.RUT_CLIENTE;
            cDatos.DV_CLIENTE = c.DV_CLIENTE;
            cDatos.DIRECCION_CLIENTE = c.DIRECCION_CLIENTE;
            cDatos.CORREO_CLIENTE = c.CORREO_CLIENTE;
            cDatos.TELEFONO_CLIENTE = c.TELEFONO_CLIENTE;
            cDatos.ID_COMUNA = c.ID_COMUNA;
            cDatos.ID_USUARIO = 0;
            cDatos.NOMBRE_CLIENTE = c.NOMBRE_CLIENTE;
            cDatos.ID_GIRO = c.ID_GIRO;

            return servicio.AgregarCliente(cDatos);
        }

        public Cliente buscarIDC(string cliente) {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Cliente));
            StringReader reader = new StringReader(cliente);
            Modelo.Cliente c = (Modelo.Cliente)ser.Deserialize(reader);
            ServicioCliente serv = new ServicioCliente();
            Datos.CLIENTE cDatos = new Datos.CLIENTE();
            cDatos.ID_USUARIO = c.ID_USUARIO;

            if (serv.BuscarIDC(cDatos) == null)
            {
                return null;
            }
            else
            {
                Datos.CLIENTE cDatos2 = serv.BuscarIDC(cDatos);
                c.RUT_CLIENTE = cDatos2.RUT_CLIENTE;
                return c;
            }
        }
        #endregion

        #region Empleado
        //CRUD Empleado
        public bool ExisteRutE(string empleado)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Empleado));
            StringReader reader = new StringReader(empleado);
            Modelo.Empleado e = (Modelo.Empleado)ser.Deserialize(reader);
            ServicioEmpleado serv = new ServicioEmpleado();
            Datos.EMPLEADO eDatos = new Datos.EMPLEADO();
            eDatos.RUT_EMPLEADO = e.RUT_EMPLEADO;

            if (!serv.ExisteRut(eDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RegistroEmpleado(string empleado)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Empleado));
            StringReader reader = new StringReader(empleado);
            Modelo.Empleado e = (Modelo.Empleado)ser.Deserialize(reader);
            ServicioEmpleado servicio = new ServicioEmpleado();

            Datos.EMPLEADO eDatos = new Datos.EMPLEADO();
            //Datos Empleado
            eDatos.RUT_EMPLEADO = e.RUT_EMPLEADO;
            eDatos.DV_EMPLEADO = e.DV_EMPLEADO;
            eDatos.PNOMBRE_EMPLEADO = e.PNOMBRE_EMPLEADO;
            eDatos.SNOMBRE_EMPLEADO = e.SNOMBRE_EMPLEADO;
            eDatos.APP_PATERNO_EMPLEADO = e.APP_PATERNO_EMPLEADO;
            eDatos.APP_MATERNO_EMPLEADO = e.APP_MATERNO_EMPLEADO;
            eDatos.ID_USUARIO = 0;

            return servicio.AgregarEmpleado(eDatos);
        }

        public Empleado buscarIDE(string empleado)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Empleado));
            StringReader reader = new StringReader(empleado);
            Modelo.Empleado e = (Modelo.Empleado)ser.Deserialize(reader);
            ServicioEmpleado serv = new ServicioEmpleado();
            Datos.EMPLEADO eDatos = new Datos.EMPLEADO();
            eDatos.ID_USUARIO = e.ID_USUARIO;

            if (serv.BuscarIDE(eDatos) == null)
            {
                return null;
            }
            else
            {
                Datos.EMPLEADO eDatos2 = serv.BuscarIDE(eDatos);
                e.RUT_EMPLEADO = eDatos2.RUT_EMPLEADO;
                return e;
            }
        }
        #endregion

        #region Proveedor
        //CRUD Proveedor
        public bool ExisteRutP(string proveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Proveedor));
            StringReader reader = new StringReader(proveedor);
            Modelo.Proveedor p = (Modelo.Proveedor)ser.Deserialize(reader);
            ServicioProveedor serv = new ServicioProveedor();
            Datos.PROVEEDOR pDatos = new Datos.PROVEEDOR();
            pDatos.RUT_PROVEEDOR = p.RUT_PROVEEDOR;

            if (!serv.ExisteRut(pDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RegistroProveedor(string proveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Proveedor));
            StringReader reader = new StringReader(proveedor);
            Modelo.Proveedor p = (Modelo.Proveedor)ser.Deserialize(reader);
            ServicioProveedor servicio = new ServicioProveedor();

            Datos.PROVEEDOR pDatos = new Datos.PROVEEDOR();
            //Datos Proveedor
            pDatos.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
            pDatos.DV_PROVEEDOR = p.DV_PROVEEDOR;
            pDatos.PNOMBRE_PROVEEDOR = p.PNOMBRE_PROVEEDOR;
            pDatos.SNOMBRE_PROVEEDOR = p.SNOMBRE_PROVEEDOR;
            pDatos.APP_PATERNO_PROVEEDOR = p.APP_PATERNO_PROVEEDOR;
            pDatos.APP_MATERNO_PROVEEDOR = p.APP_MATERNO_PROVEEDOR;
            pDatos.ID_TIPO_PROVEEDOR = p.ID_TIPO_PROVEEDOR;
            pDatos.ID_USUARIO = 0;

            return servicio.AgregarProveedor(pDatos);
        }

        public string ListarProveedor()
        {
            ServicioProveedor servicio = new ServicioProveedor();
            List<Datos.PROVEEDOR> proveedor = servicio.ListarProveedor();
            Modelo.ProveedorCollection2 listaProveedor = new Modelo.ProveedorCollection2();

            foreach (Datos.PROVEEDOR p in proveedor)
            {
                Modelo.Proveedor pModelo = new Modelo.Proveedor();
                pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
                pModelo.DV_PROVEEDOR = p.DV_PROVEEDOR;
                pModelo.PNOMBRE_PROVEEDOR = p.PNOMBRE_PROVEEDOR;
                pModelo.SNOMBRE_PROVEEDOR = p.SNOMBRE_PROVEEDOR;
                pModelo.APP_MATERNO_PROVEEDOR = p.APP_MATERNO_PROVEEDOR;
                pModelo.APP_PATERNO_PROVEEDOR = p.APP_PATERNO_PROVEEDOR;
                pModelo.ID_TIPO_PROVEEDOR = p.ID_TIPO_PROVEEDOR;

                listaProveedor.Add(pModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.ProveedorCollection2));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaProveedor);
            writer.Close();
            return writer.ToString();
        }

        public Proveedor buscarIDP(string proveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Proveedor));
            StringReader reader = new StringReader(proveedor);
            Modelo.Proveedor p = (Modelo.Proveedor)ser.Deserialize(reader);
            ServicioProveedor serv = new ServicioProveedor();
            Datos.PROVEEDOR pDatos = new Datos.PROVEEDOR();
            pDatos.ID_USUARIO = p.ID_USUARIO;

            if (serv.BuscarIDP(pDatos) == null)
            {
                return null;
            }
            else
            {
                Datos.PROVEEDOR pDatos2 = serv.BuscarIDP(pDatos);
                p.RUT_PROVEEDOR = pDatos2.RUT_PROVEEDOR;
                return p;
            }
        }

        //Listar Datos
        public string ListarTipoProveedor()
        {
            ServicioProveedor servicio = new ServicioProveedor();
            List<Datos.TIPO_PROVEEDOR> tipo = servicio.ListarTipoProveedor();
            Modelo.TipoProveedorCollection listaTipoProveedor = new Modelo.TipoProveedorCollection();
            foreach (Datos.TIPO_PROVEEDOR t in tipo)
            {
                Modelo.TipoProveedor tModelo = new Modelo.TipoProveedor();
                tModelo.ID_TIPO_PROVEEDOR = t.ID_TIPO_PROVEEDOR;
                tModelo.NOMBRE_TIPO = t.NOMBRE_TIPO;
                listaTipoProveedor.Add(tModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoProveedorCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaTipoProveedor);
            return writer.ToString();
        }

        public string ListarProductosProveedor(string proveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Proveedor));
            StringReader reader = new StringReader(proveedor);
            Modelo.Proveedor p = (Modelo.Proveedor)ser.Deserialize(reader);
            ServicioProducto serv = new ServicioProducto();

            Datos.PROVEEDOR pDatos = new Datos.PROVEEDOR();
            pDatos.RUT_PROVEEDOR = p.RUT_PROVEEDOR;

            List<Datos.PRODUCTO> listaProducto = serv.ListarProveedorProducto(pDatos);

            if (listaProducto == null)
            {
                return null;
            }
            else
            {
                XmlSerializer servicio = new XmlSerializer(typeof(Modelo.Producto));
                Modelo.ProductoCollection listaProducto2 = new Modelo.ProductoCollection();

                foreach (Datos.PRODUCTO pr in listaProducto)
                {
                    Modelo.Producto pModelo = new Modelo.Producto();
                    pModelo.ID_PRODUCTO = pr.ID_PRODUCTO;
                    pModelo.NOMBRE_PRODUCTO = pr.NOMBRE_PRODUCTO;
                    pModelo.PRECIO_PRODUCTO = pr.PRECIO_PRODUCTO;
                    pModelo.STOCK_PRODUCTO = pr.STOCK_PRODUCTO;
                    pModelo.STOCK_CRITICO_PRODUCTO = pr.STOCK_CRITICO_PRODUCTO;
                    pModelo.RUT_PROVEEDOR = pr.RUT_PROVEEDOR;
                    pModelo.UNIDAD_MEDIDA = pr.UNIDAD_MEDIDA;

                    listaProducto2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.ProductoCollection));
                StringWriter writer = new StringWriter();
                ser2.Serialize(writer, listaProducto2);
                writer.Close();
                return writer.ToString();
            }
        }
        #endregion

        #region Habitacion
        //CRUD Habitacion
        public bool AgregarHabitacion(string habitacion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Habitacion));
            StringReader reader = new StringReader(habitacion);
            Modelo.Habitacion h = (Modelo.Habitacion)ser.Deserialize(reader);
            ServicioHabitacion servicio = new ServicioHabitacion();

            Datos.HABITACION hDatos = new Datos.HABITACION();
            //Datos Proveedor
            hDatos.NUMERO_HABITACION = h.NUMERO_HABITACION;
            hDatos.ESTADO_HABITACION = h.ESTADO_HABITACION;
            hDatos.ID_TIPO_HABITACION = h.ID_TIPO_HABITACION;
            hDatos.ID_CATEGORIA_HABITACION = h.ID_CATEGORIA_HABITACION;

            return servicio.AgregarHabitacion(hDatos);
        }

        public string ListarHabitacion()
        {
            ServicioHabitacion servicio = new ServicioHabitacion();
            List<Datos.HABITACION> habitacion = servicio.listarHabitacion();
            Modelo.HabitacionCollection listaHabitacion = new Modelo.HabitacionCollection();

            foreach (Datos.HABITACION h in habitacion) {
                Modelo.Habitacion hModelo = new Modelo.Habitacion();
                hModelo.NUMERO_HABITACION = h.NUMERO_HABITACION;
                hModelo.ESTADO_HABITACION = h.ESTADO_HABITACION;
                hModelo.ID_TIPO_HABITACION = h.ID_TIPO_HABITACION;
                hModelo.ID_CATEGORIA_HABITACION = h.ID_CATEGORIA_HABITACION;

                listaHabitacion.Add(hModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.HabitacionCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaHabitacion);
            writer.Close();
            return writer.ToString();
        }

        public bool ExisteHabitacion(string habitacion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Habitacion));
            StringReader reader = new StringReader(habitacion);
            Modelo.Habitacion h = (Modelo.Habitacion)ser.Deserialize(reader);
            ServicioHabitacion serv = new ServicioHabitacion();
            Datos.HABITACION hDatos = new Datos.HABITACION();
            hDatos.NUMERO_HABITACION = h.NUMERO_HABITACION;

            if (!serv.ExisteHabitacion(hDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Habitacion ObtenerHabitacion(string habitacion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Habitacion));
            StringReader reader = new StringReader(habitacion);
            Modelo.Habitacion h = (Modelo.Habitacion)ser.Deserialize(reader);
            ServicioHabitacion serv = new ServicioHabitacion();
            Datos.HABITACION hDatos = new Datos.HABITACION();
            hDatos.NUMERO_HABITACION = h.NUMERO_HABITACION;

            if (!serv.ExisteHabitacion(hDatos))
            {
                return null;
            }
            else
            {
                Datos.HABITACION hDatos2 = serv.obtenerHabitacion(hDatos);
                h.NUMERO_HABITACION = hDatos2.NUMERO_HABITACION;
                h.ESTADO_HABITACION = hDatos2.ESTADO_HABITACION;
                h.ID_TIPO_HABITACION = hDatos2.ID_TIPO_HABITACION;
                h.ID_CATEGORIA_HABITACION = hDatos2.ID_CATEGORIA_HABITACION;

                return h;
            }
        }

        public bool ModificarHabitacion(string habitacion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Habitacion));
            StringReader reader = new StringReader(habitacion);
            Modelo.Habitacion h = (Modelo.Habitacion)ser.Deserialize(reader);
            ServicioHabitacion serv = new ServicioHabitacion();
            Datos.HABITACION hDatos = new Datos.HABITACION();

            hDatos.NUMERO_HABITACION = h.NUMERO_HABITACION;
            hDatos.ID_TIPO_HABITACION = h.ID_TIPO_HABITACION;
            hDatos.ESTADO_HABITACION = h.ESTADO_HABITACION;
            hDatos.ID_CATEGORIA_HABITACION = h.ID_CATEGORIA_HABITACION;

            return serv.EditarHabitacion(hDatos);
        }

        public bool EliminarHabitacion(string habitacion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Habitacion));
            StringReader reader = new StringReader(habitacion);
            Modelo.Habitacion h = (Modelo.Habitacion)ser.Deserialize(reader);
            ServicioHabitacion serv = new ServicioHabitacion();
            Datos.HABITACION hDatos = new Datos.HABITACION();

            hDatos.NUMERO_HABITACION = h.NUMERO_HABITACION;

            return serv.EliminarHabitacion(hDatos);
        }
        #endregion

        #region Producto
        //CRUD Producto
        public bool AgregarProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProducto servicio = new ServicioProducto();

            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();
            //Datos Producto
            pDatos.DESCRIPCION_PRODUCTO = p.DESCRIPCION_PRODUCTO;
            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;
            pDatos.NOMBRE_PRODUCTO = p.NOMBRE_PRODUCTO;
            pDatos.FECHA_VENCIMIENTO_PRODUCTO = p.FECHA_VENCIMIENTO_PRODUCTO;
            pDatos.STOCK_PRODUCTO = p.STOCK_PRODUCTO;
            pDatos.STOCK_CRITICO_PRODUCTO = p.STOCK_CRITICO_PRODUCTO;
            pDatos.PRECIO_PRODUCTO = p.PRECIO_PRODUCTO;
            pDatos.ID_FAMILIA = p.ID_FAMILIA;
            pDatos.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
            pDatos.ID_PRODUCTO_SEQ = p.ID_PRODUCTO_SEQ;
            pDatos.UNIDAD_MEDIDA = p.UNIDAD_MEDIDA;

            return servicio.AgregarProducto(pDatos);
        }
        public bool ModificarProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProducto serv = new ServicioProducto();
            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();

            pDatos.DESCRIPCION_PRODUCTO = p.DESCRIPCION_PRODUCTO;
            pDatos.FECHA_VENCIMIENTO_PRODUCTO = p.FECHA_VENCIMIENTO_PRODUCTO;
            pDatos.ID_FAMILIA = p.ID_FAMILIA;
            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;
            pDatos.NOMBRE_PRODUCTO = p.NOMBRE_PRODUCTO;
            pDatos.PRECIO_PRODUCTO = p.PRECIO_PRODUCTO;
            pDatos.STOCK_CRITICO_PRODUCTO = p.STOCK_CRITICO_PRODUCTO;
            pDatos.STOCK_PRODUCTO = p.STOCK_PRODUCTO;
            pDatos.UNIDAD_MEDIDA = p.UNIDAD_MEDIDA;

            return serv.EditarProducto(pDatos);
        }

        public bool ExisteProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProducto serv = new ServicioProducto();
            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();
            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;

            if (!serv.ExisteProducto(pDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Producto ObtenerProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProducto serv = new ServicioProducto();
            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();
            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;

            if (!serv.ExisteProducto(pDatos))
            {
                return null;
            }
            else
            {
                Datos.PRODUCTO pDatos2 = serv.obtenerProducto(pDatos);
                p.FECHA_VENCIMIENTO_PRODUCTO = pDatos2.FECHA_VENCIMIENTO_PRODUCTO;
                p.DESCRIPCION_PRODUCTO = pDatos2.DESCRIPCION_PRODUCTO;
                p.ID_FAMILIA = pDatos2.ID_FAMILIA;
                p.ID_PRODUCTO = pDatos2.ID_PRODUCTO;
                p.NOMBRE_PRODUCTO = pDatos2.NOMBRE_PRODUCTO;
                p.PRECIO_PRODUCTO = pDatos2.PRECIO_PRODUCTO;
                p.STOCK_CRITICO_PRODUCTO = pDatos2.STOCK_CRITICO_PRODUCTO;
                p.STOCK_PRODUCTO = pDatos2.STOCK_PRODUCTO;
                p.RUT_PROVEEDOR = pDatos2.RUT_PROVEEDOR;
                p.UNIDAD_MEDIDA = pDatos2.UNIDAD_MEDIDA;

                return p;
            }
        }

        public string ListarProducto()
        {
            ServicioProducto servicio = new ServicioProducto();
            List<Datos.PRODUCTO> producto = servicio.ListarProducto();
            Modelo.ProductoCollection listaProducto = new Modelo.ProductoCollection();

            foreach (Datos.PRODUCTO p in producto)
            {
                Modelo.Producto pModelo = new Modelo.Producto();
                pModelo.ID_PRODUCTO = p.ID_PRODUCTO;
                pModelo.NOMBRE_PRODUCTO = p.NOMBRE_PRODUCTO;
                pModelo.PRECIO_PRODUCTO = p.PRECIO_PRODUCTO;
                pModelo.STOCK_CRITICO_PRODUCTO = p.STOCK_CRITICO_PRODUCTO;
                pModelo.STOCK_PRODUCTO = p.STOCK_PRODUCTO;
                pModelo.ID_FAMILIA = p.ID_FAMILIA;
                pModelo.FECHA_VENCIMIENTO_PRODUCTO = p.FECHA_VENCIMIENTO_PRODUCTO;
                pModelo.DESCRIPCION_PRODUCTO = p.DESCRIPCION_PRODUCTO;
                pModelo.UNIDAD_MEDIDA = p.UNIDAD_MEDIDA;

                listaProducto.Add(pModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.ProductoCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaProducto);
            writer.Close();
            return writer.ToString();
        }

        public bool EliminarProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProducto serv = new ServicioProducto();
            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();

            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;

            return serv.EliminarProducto(pDatos);
        }

        public string ListarProveedorProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProveedor serv = new ServicioProveedor();

            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();
            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;
            pDatos.RUT_PROVEEDOR = p.RUT_PROVEEDOR;

            List<Datos.PROVEEDOR> listaProveedor = serv.ListarProveedorProducto(pDatos);

            if (listaProveedor == null)
            {
                return null;
            }
            else
            {
                XmlSerializer servicio = new XmlSerializer(typeof(Modelo.Proveedor));
                Modelo.ProveedorCollection2 listaProveedor2 = new Modelo.ProveedorCollection2();

                foreach (Datos.PROVEEDOR pr in listaProveedor)
                {
                    Modelo.Proveedor pModelo = new Modelo.Proveedor();
                    pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;

                    listaProveedor2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.ProveedorCollection2));
                StringWriter writer = new StringWriter();
                ser2.Serialize(writer, listaProveedor2);
                writer.Close();
                return writer.ToString();
            }
        }
        #endregion

        #region Plato
        //CRUD Plato
        public bool AgregarPlato(string plato)
        {
            //Datos Plato
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(plato);
            Modelo.Plato p = (Modelo.Plato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.PLATO pDatos = new Datos.PLATO();
            pDatos.ID_PLATO = p.ID_PLATO;
            pDatos.NOMBRE_PLATO = p.NOMBRE_PLATO;
            pDatos.PRECIO_PLATO = p.PRECIO_PLATO;
            pDatos.ID_CATEGORIA = p.ID_CATEGORIA;
            pDatos.ID_TIPO_PLATO = p.ID_TIPO_PLATO;

            return serv.AgregarPlato(pDatos);
        }

        public bool ExistePlato(string plato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(plato);
            Modelo.Plato p = (Modelo.Plato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.PLATO pDatos = new Datos.PLATO();
            pDatos.NOMBRE_PLATO = p.NOMBRE_PLATO;

            if (!serv.ExistePlato(pDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public Plato ObtenerPlato(string plato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(plato);
            Modelo.Plato p = (Modelo.Plato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.PLATO pDatos = new Datos.PLATO();
            pDatos.ID_PLATO = p.ID_PLATO;

            if (!serv.ExistePlatoID(pDatos))
            {
                return null;
            }
            else
            {
                Datos.PLATO pDatos2 = serv.obtenerPlato(pDatos);

                p.ID_PLATO = pDatos2.ID_PLATO;
                p.NOMBRE_PLATO = pDatos2.NOMBRE_PLATO;
                p.PRECIO_PLATO = pDatos2.PRECIO_PLATO;
                p.ID_CATEGORIA = pDatos2.ID_CATEGORIA;
                p.ID_TIPO_PLATO = pDatos2.ID_TIPO_PLATO;

                return p;
            }
        }

        public bool ModificarPlato(string plato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(plato);
            Modelo.Plato p = (Modelo.Plato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.PLATO pDatos = new Datos.PLATO();

            pDatos.ID_TIPO_PLATO = p.ID_TIPO_PLATO;
            pDatos.ID_PLATO = p.ID_PLATO;
            pDatos.NOMBRE_PLATO = p.NOMBRE_PLATO;
            pDatos.PRECIO_PLATO = p.PRECIO_PLATO;
            pDatos.ID_CATEGORIA = p.ID_CATEGORIA;

            return serv.EditarPlato(pDatos);
        }

        public string ListarPlato()
        {
            ServicioPlato servicio = new ServicioPlato();
            List<Datos.PLATO> plato = servicio.ListarPlato();
            Modelo.PlatoCollection listaPlato = new Modelo.PlatoCollection();

            foreach (Datos.PLATO p in plato)
            {
                Modelo.Plato pModelo = new Modelo.Plato();
                pModelo.ID_PLATO = p.ID_PLATO;
                pModelo.NOMBRE_PLATO = p.NOMBRE_PLATO;
                pModelo.PRECIO_PLATO = p.PRECIO_PLATO;
                pModelo.ID_CATEGORIA = p.ID_CATEGORIA;
                pModelo.ID_TIPO_PLATO = p.ID_TIPO_PLATO;

                listaPlato.Add(pModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PlatoCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaPlato);
            writer.Close();
            return writer.ToString();
        }

        public bool EliminarPlato(string plato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(plato);
            Modelo.Plato p = (Modelo.Plato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.PLATO pDatos = new Datos.PLATO();

            pDatos.ID_PLATO = p.ID_PLATO;

            return serv.EliminarPlato(pDatos);
        }

        public string ListarPlatoPorCategoria(string categoria)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Categoria));
            StringReader reader = new StringReader(categoria);
            Modelo.Categoria c = (Modelo.Categoria)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();

            Datos.CATEGORIA cDatos = new Datos.CATEGORIA();
            cDatos.ID_CATEGORIA = c.ID_CATEGORIA;

            List<Datos.PLATO> listaPlato = serv.listaPlatoCategoria(cDatos);

            if (listaPlato == null)
            {
                return null;
            }
            else
            {
                XmlSerializer servicio = new XmlSerializer(typeof(Modelo.Plato));
                Modelo.PlatoCollection listaPlato2 = new Modelo.PlatoCollection();

                foreach (Datos.PLATO pl in listaPlato)
                {
                    Modelo.Plato pModelo = new Modelo.Plato();
                    pModelo.ID_PLATO = pl.ID_PLATO;
                    pModelo.NOMBRE_PLATO = pl.NOMBRE_PLATO;
                    pModelo.PRECIO_PLATO = pl.PRECIO_PLATO;
                    pModelo.ID_CATEGORIA = pl.ID_CATEGORIA;
                    pModelo.ID_TIPO_PLATO = pl.ID_TIPO_PLATO;

                    listaPlato2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.PlatoCollection));
                StringWriter writer = new StringWriter();
                ser2.Serialize(writer, listaPlato2);
                writer.Close();
                return writer.ToString();
            }
        }
        #endregion

        #region Tipo Plato
        //TIPO PLATO
        public bool AgregarTipoPlato(string tipoPlato)
        {
            //Datos tipo Plato
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlato));
            StringReader reader = new StringReader(tipoPlato);
            Modelo.TipoPlato p = (Modelo.TipoPlato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.TIPO_PLATO tpDatos = new Datos.TIPO_PLATO();
            tpDatos.ID_TIPO_PLATO = p.ID_TIPO_PLATO;
            tpDatos.NOMBRE_TIPO_PLATO = p.NOMBRE_TIPO_PLATO;

            return serv.AgregarTipoPlato(tpDatos);
        }

        public bool ExisteTipoPlato(string tipoPlato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlato));
            StringReader reader = new StringReader(tipoPlato);
            Modelo.TipoPlato tp = (Modelo.TipoPlato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.TIPO_PLATO tpDatos = new Datos.TIPO_PLATO();
            tpDatos.NOMBRE_TIPO_PLATO = tp.NOMBRE_TIPO_PLATO;

            if (!serv.ExisteTipoPlato(tpDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public TipoPlato ObtenerTipoPlato(string tipoPlato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlato));
            StringReader reader = new StringReader(tipoPlato);
            Modelo.TipoPlato p = (Modelo.TipoPlato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.TIPO_PLATO tpDatos = new Datos.TIPO_PLATO();
            tpDatos.ID_TIPO_PLATO = p.ID_TIPO_PLATO;

            if (!serv.ExisteTipoPlatoID(tpDatos))
            {
                return null;
            }
            else
            {
                Datos.TIPO_PLATO pDatos2 = serv.obtenerTipoPlato(tpDatos);

                p.ID_TIPO_PLATO = pDatos2.ID_TIPO_PLATO;
                p.NOMBRE_TIPO_PLATO = pDatos2.NOMBRE_TIPO_PLATO;

                return p;
            }
        }

        public bool ModificarTipoPlato(string tipoPlato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlato));
            StringReader reader = new StringReader(tipoPlato);
            Modelo.TipoPlato p = (Modelo.TipoPlato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.TIPO_PLATO tpDatos = new Datos.TIPO_PLATO();

            tpDatos.ID_TIPO_PLATO = p.ID_TIPO_PLATO;
            tpDatos.NOMBRE_TIPO_PLATO = p.NOMBRE_TIPO_PLATO;

            return serv.EditarTipoPlato(tpDatos);
        }

        public bool EliminarTipoPlato(string tipoPlato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlato));
            StringReader reader = new StringReader(tipoPlato);
            Modelo.TipoPlato tp = (Modelo.TipoPlato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.TIPO_PLATO tpDatos = new Datos.TIPO_PLATO();

            tpDatos.ID_TIPO_PLATO = tp.ID_TIPO_PLATO;

            return serv.EliminarTipoPlato(tpDatos);
        }
        #endregion

        #region Tipo Proveedor
        // tipo proveedor
        public bool AgregarTipoProveedor(string tipoProveedor)
        {
            //Datos tipo PROVEEDOR
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoProveedor));
            StringReader reader = new StringReader(tipoProveedor);
            Modelo.TipoProveedor p = (Modelo.TipoProveedor)ser.Deserialize(reader);
            ServicioProveedor serv = new ServicioProveedor();
            Datos.TIPO_PROVEEDOR tpDatos = new Datos.TIPO_PROVEEDOR();
            tpDatos.ID_TIPO_PROVEEDOR = p.ID_TIPO_PROVEEDOR;
            tpDatos.NOMBRE_TIPO = p.NOMBRE_TIPO;

            return serv.AgregarTipoProveedor(tpDatos);
        }

        public bool ExisteTipoProveedor(string tipoProveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoProveedor));
            StringReader reader = new StringReader(tipoProveedor);
            Modelo.TipoProveedor tp = (Modelo.TipoProveedor)ser.Deserialize(reader);
            ServicioProveedor serv = new ServicioProveedor();
            Datos.TIPO_PROVEEDOR tpDatos = new Datos.TIPO_PROVEEDOR();
            tpDatos.NOMBRE_TIPO = tp.NOMBRE_TIPO;

            if (!serv.ExisteTipoProveedor(tpDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public TipoProveedor ObtenerTipoProveedor(string tipoProveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoProveedor));
            StringReader reader = new StringReader(tipoProveedor);
            Modelo.TipoProveedor p = (Modelo.TipoProveedor)ser.Deserialize(reader);
            ServicioProveedor serv = new ServicioProveedor();
            Datos.TIPO_PROVEEDOR tpDatos = new Datos.TIPO_PROVEEDOR();
            tpDatos.ID_TIPO_PROVEEDOR = p.ID_TIPO_PROVEEDOR;

            if (!serv.ExisteTipoProveedorID(tpDatos))
            {
                return null;
            }
            else
            {
                Datos.TIPO_PROVEEDOR pDatos2 = serv.obtenerTipoProveedor(tpDatos);

                p.ID_TIPO_PROVEEDOR = pDatos2.ID_TIPO_PROVEEDOR;
                p.NOMBRE_TIPO = pDatos2.NOMBRE_TIPO;

                return p;
            }
        }

        public bool ModificarTipoProveedor(string tipoProveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoProveedor));
            StringReader reader = new StringReader(tipoProveedor);
            Modelo.TipoProveedor p = (Modelo.TipoProveedor)ser.Deserialize(reader);
            ServicioProveedor serv = new ServicioProveedor();
            Datos.TIPO_PROVEEDOR tpDatos = new Datos.TIPO_PROVEEDOR();

            tpDatos.ID_TIPO_PROVEEDOR = p.ID_TIPO_PROVEEDOR;
            tpDatos.NOMBRE_TIPO = p.NOMBRE_TIPO;

            return serv.EditarTipoProveedor(tpDatos);
        }

        public bool EliminarTipoProveedor(string tipoProveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoProveedor));
            StringReader reader = new StringReader(tipoProveedor);
            Modelo.TipoProveedor tp = (Modelo.TipoProveedor)ser.Deserialize(reader);
            ServicioProveedor serv = new ServicioProveedor();
            Datos.TIPO_PROVEEDOR tpDatos = new Datos.TIPO_PROVEEDOR();

            tpDatos.ID_TIPO_PROVEEDOR = tp.ID_TIPO_PROVEEDOR;

            return serv.EliminarTipoProveedor(tpDatos);
        }
        #endregion

        #region Categoria Plato
        //Categoria plato
        public bool AgregarCategoria(string categoria)
        {
            //Datos tipo PROVEEDOR
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Categoria));
            StringReader reader = new StringReader(categoria);
            Modelo.Categoria c = (Modelo.Categoria)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.CATEGORIA Datos = new Datos.CATEGORIA();
            Datos.ID_CATEGORIA = c.ID_CATEGORIA;
            Datos.NOMBRE_CATEGORIA = c.NOMBRE_CATEGORIA;

            return serv.AgregarCategoria(Datos);
        }

        public bool ExisteCategoria(string categoria)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Categoria));
            StringReader reader = new StringReader(categoria);
            Modelo.Categoria c = (Modelo.Categoria)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.CATEGORIA Datos = new Datos.CATEGORIA();
            Datos.NOMBRE_CATEGORIA = c.NOMBRE_CATEGORIA;

            if (!serv.ExisteCategoria(Datos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public Categoria ObtenerCategoria(string categoria)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Categoria));
            StringReader reader = new StringReader(categoria);
            Modelo.Categoria c = (Modelo.Categoria)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.CATEGORIA Datos = new Datos.CATEGORIA();
            Datos.ID_CATEGORIA = c.ID_CATEGORIA;

            if (!serv.ExisteCategoriaID(Datos))
            {
                return null;
            }
            else
            {
                Datos.CATEGORIA Datos2 = serv.obtenerCategoria(Datos);

                c.ID_CATEGORIA = Datos2.ID_CATEGORIA;
                c.NOMBRE_CATEGORIA = Datos2.NOMBRE_CATEGORIA;

                return c;
            }
        }

        public bool ModificarCategoria(string categoria)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Categoria));
            StringReader reader = new StringReader(categoria);
            Modelo.Categoria c = (Modelo.Categoria)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.CATEGORIA Datos = new Datos.CATEGORIA();

            Datos.ID_CATEGORIA = c.ID_CATEGORIA;
            Datos.NOMBRE_CATEGORIA = c.NOMBRE_CATEGORIA;

            return serv.EditarCategoria(Datos);
        }

        public bool EliminarCategoria(string categoria)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Categoria));
            StringReader reader = new StringReader(categoria);
            Modelo.Categoria c = (Modelo.Categoria)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.CATEGORIA Datos = new Datos.CATEGORIA();

            Datos.ID_CATEGORIA = c.ID_CATEGORIA;

            return serv.EliminarCategoria(Datos);
        }
        #endregion

        #region Pais
        //PAIS
        public bool AgregarPais(string pais)
        {
            //Datos tipo pais
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Pais));
            StringReader reader = new StringReader(pais);
            Modelo.Pais p = (Modelo.Pais)ser.Deserialize(reader);
            ServicioDireccion serv = new ServicioDireccion();
            Datos.PAIS Datos = new Datos.PAIS();

            Datos.ID_PAIS = p.ID_PAIS;
            Datos.NOMBRE_PAIS = p.NOMBRE_PAIS;

            return serv.AgregarPais(Datos);
        }

        public bool ExistePais(string pais)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Pais));
            StringReader reader = new StringReader(pais);
            Modelo.Pais p = (Modelo.Pais)ser.Deserialize(reader);
            ServicioDireccion serv = new ServicioDireccion();
            Datos.PAIS Datos = new Datos.PAIS();
            Datos.NOMBRE_PAIS = p.NOMBRE_PAIS;

            if (!serv.ExistePais(Datos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public Pais ObtenerPais(string pais)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Pais));
            StringReader reader = new StringReader(pais);
            Modelo.Pais p = (Modelo.Pais)ser.Deserialize(reader);
            ServicioDireccion serv = new ServicioDireccion();
            Datos.PAIS Datos = new Datos.PAIS();
            Datos.ID_PAIS = p.ID_PAIS;

            if (!serv.ExistePaisID(Datos))
            {
                return null;
            }
            else
            {
                Datos.PAIS Datos2 = serv.obtenerPais(Datos);

                p.ID_PAIS = Datos2.ID_PAIS;
                p.NOMBRE_PAIS = Datos2.NOMBRE_PAIS;

                return p;
            }
        }

        public bool ModificarPais(string pais)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Pais));
            StringReader reader = new StringReader(pais);
            Modelo.Pais p = (Modelo.Pais)ser.Deserialize(reader);
            ServicioDireccion serv = new ServicioDireccion();
            Datos.PAIS Datos = new Datos.PAIS();

            Datos.ID_PAIS = p.ID_PAIS;
            Datos.NOMBRE_PAIS = p.NOMBRE_PAIS;

            return serv.EditarPais(Datos);
        }

        public bool EliminarPais(string pais)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Pais));
            StringReader reader = new StringReader(pais);
            Modelo.Pais p = (Modelo.Pais)ser.Deserialize(reader);
            ServicioDireccion serv = new ServicioDireccion();
            Datos.PAIS Datos = new Datos.PAIS();

            Datos.ID_PAIS = p.ID_PAIS;

            return serv.EliminarPais(Datos);
        }

        #endregion

        #region Pedido
        //CRUD Pedido
        public bool AgregarPedido(string pedido)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Pedido));
            StringReader reader = new StringReader(pedido);
            Modelo.Pedido p = (Modelo.Pedido)ser.Deserialize(reader);
            ServicioOrden servicio = new ServicioOrden();

            Datos.PEDIDO pDatos = new Datos.PEDIDO();
            //Datos Pedido
            pDatos.FECHA_PEDIDO = p.FECHA_PEDIDO;
            pDatos.ESTADO_PEDIDO = p.ESTADO_PEDIDO;
            pDatos.RUT_EMPLEADO = p.RUT_EMPLEADO;
            pDatos.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
            pDatos.ESTADO_DESPACHO = p.ESTADO_DESPACHO;

            return servicio.AgregarPedido(pDatos);
        }

        public bool AgregarDetallePedido(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetallePedido));
            StringReader reader = new StringReader(detalle);
            Modelo.DetallePedido d = (Modelo.DetallePedido)ser.Deserialize(reader);
            ServicioOrden servicio = new ServicioOrden();

            Datos.DETALLE_PEDIDO dDatos = new Datos.DETALLE_PEDIDO();
            //Datos Pedido
            dDatos.CANTIDAD = d.CANTIDAD;
            dDatos.ID_PRODUCTO = d.ID_PRODUCTO;
            dDatos.NUMERO_PEDIDO = d.NUMERO_PEDIDO;
            return servicio.AgregarDetallePedido(dDatos);
        }

        public string ListarPedidoAdmin()
        {

            ServicioOrden servicio = new ServicioOrden();
            List<Datos.PEDIDO> pedido = servicio.ListarPedidoAdmin();
            Modelo.PedidoCollection listaPedido = new Modelo.PedidoCollection();

            foreach (Datos.PEDIDO p in pedido)
            {
                Modelo.Pedido pModelo = new Modelo.Pedido();
                pModelo.NUMERO_PEDIDO = p.NUMERO_PEDIDO;
                pModelo.ESTADO_PEDIDO = p.ESTADO_PEDIDO;
                pModelo.FECHA_PEDIDO = p.FECHA_PEDIDO;
                pModelo.RUT_EMPLEADO = p.RUT_EMPLEADO;
                pModelo.NUMERO_RECEPCION = p.NUMERO_RECEPCION;
                pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
                pModelo.ESTADO_DESPACHO = p.ESTADO_DESPACHO;
                pModelo.COMENTARIO = p.COMENTARIO;

                listaPedido.Add(pModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PedidoCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaPedido);
            writer.Close();
            return writer.ToString();
        }

        public string ListarPedidoEmpleadoPendiente(string empleado)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Empleado));
            StringReader reader = new StringReader(empleado);
            Modelo.Empleado pr = (Modelo.Empleado)ser.Deserialize(reader);
            ServicioOrden serv = new ServicioOrden();

            Datos.EMPLEADO pDatos = new Datos.EMPLEADO();
            pDatos.RUT_EMPLEADO = pr.RUT_EMPLEADO;

            List<Datos.PEDIDO> listaPedido = serv.ListarPedidoEmpleadoPendiente(pDatos);

            if (listaPedido == null)
            {
                return null;
            }
            else
            {
                Modelo.PedidoCollection listaPedido2 = new PedidoCollection();

                foreach (Datos.PEDIDO p in listaPedido)
                {
                    Modelo.Pedido pModelo = new Modelo.Pedido();
                    pModelo.NUMERO_PEDIDO = p.NUMERO_PEDIDO;
                    pModelo.ESTADO_PEDIDO = p.ESTADO_PEDIDO;
                    pModelo.FECHA_PEDIDO = p.FECHA_PEDIDO;
                    pModelo.RUT_EMPLEADO = p.RUT_EMPLEADO;
                    pModelo.NUMERO_RECEPCION = p.NUMERO_RECEPCION;
                    pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
                    pModelo.ESTADO_DESPACHO = p.ESTADO_DESPACHO;
                    pModelo.COMENTARIO = p.COMENTARIO;

                    listaPedido2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaPedido2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public string ListarPedidoEmpleadoListo(string empleado)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Empleado));
            StringReader reader = new StringReader(empleado);
            Modelo.Empleado pr = (Modelo.Empleado)ser.Deserialize(reader);
            ServicioOrden serv = new ServicioOrden();

            Datos.EMPLEADO pDatos = new Datos.EMPLEADO();
            pDatos.RUT_EMPLEADO = pr.RUT_EMPLEADO;

            List<Datos.PEDIDO> listaPedido = serv.ListarPedidoEmpleadoListo(pDatos);

            if (listaPedido == null)
            {
                return null;
            }
            else
            {
                Modelo.PedidoCollection listaPedido2 = new PedidoCollection();

                foreach (Datos.PEDIDO p in listaPedido)
                {
                    Modelo.Pedido pModelo = new Modelo.Pedido();
                    pModelo.NUMERO_PEDIDO = p.NUMERO_PEDIDO;
                    pModelo.ESTADO_PEDIDO = p.ESTADO_PEDIDO;
                    pModelo.FECHA_PEDIDO = p.FECHA_PEDIDO;
                    pModelo.RUT_EMPLEADO = p.RUT_EMPLEADO;
                    pModelo.NUMERO_RECEPCION = p.NUMERO_RECEPCION;
                    pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
                    pModelo.ESTADO_DESPACHO = p.ESTADO_DESPACHO;
                    pModelo.COMENTARIO = p.COMENTARIO;

                    listaPedido2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaPedido2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public string ListarPedidoProveedor(string proveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Proveedor));
            StringReader reader = new StringReader(proveedor);
            Modelo.Proveedor pr = (Modelo.Proveedor)ser.Deserialize(reader);
            ServicioOrden serv = new ServicioOrden();

            Datos.PROVEEDOR pDatos = new Datos.PROVEEDOR();
            pDatos.RUT_PROVEEDOR = pr.RUT_PROVEEDOR;

            List<Datos.PEDIDO> listaPedido = serv.ListarPedidoProveedor(pDatos);

            if (listaPedido == null)
            {
                return null;
            }
            else
            {
                Modelo.PedidoCollection listaPedido2 = new PedidoCollection();

                foreach (Datos.PEDIDO p in listaPedido)
                {
                    Modelo.Pedido pModelo = new Modelo.Pedido();
                    pModelo.NUMERO_PEDIDO = p.NUMERO_PEDIDO;
                    pModelo.ESTADO_PEDIDO = p.ESTADO_PEDIDO;
                    pModelo.FECHA_PEDIDO = p.FECHA_PEDIDO;
                    pModelo.RUT_EMPLEADO = p.RUT_EMPLEADO;
                    pModelo.NUMERO_RECEPCION = p.NUMERO_RECEPCION;
                    pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
                    pModelo.ESTADO_DESPACHO = p.ESTADO_DESPACHO;
                    pModelo.COMENTARIO = p.COMENTARIO;

                    listaPedido2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaPedido2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public bool EditarEstadoPedido(string pedido)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Pedido));
            StringReader reader = new StringReader(pedido);
            Modelo.Pedido p = (Modelo.Pedido)ser.Deserialize(reader);
            ServicioOrden servicio = new ServicioOrden();

            Datos.PEDIDO pDatos = new Datos.PEDIDO();

            pDatos.NUMERO_PEDIDO = p.NUMERO_PEDIDO;
            pDatos.COMENTARIO = p.COMENTARIO;
            pDatos.ESTADO_DESPACHO = p.ESTADO_DESPACHO;
            pDatos.ESTADO_PEDIDO = p.ESTADO_PEDIDO;
            pDatos.FECHA_PEDIDO = p.FECHA_PEDIDO;
            pDatos.RUT_EMPLEADO = p.RUT_EMPLEADO;
            pDatos.RUT_PROVEEDOR = p.RUT_PROVEEDOR;

            return servicio.EditarEstadoPedido(pDatos);
        }

        public bool EditarDetallePedido(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetallePedido));
            StringReader reader = new StringReader(detalle);
            Modelo.DetallePedido d = (Modelo.DetallePedido)ser.Deserialize(reader);
            ServicioOrden servicio = new ServicioOrden();

            Datos.DETALLE_PEDIDO dDatos = new Datos.DETALLE_PEDIDO();

            dDatos.NUMERO_PEDIDO = d.NUMERO_PEDIDO;
            dDatos.ID_DETALLE_PEDIDO = d.ID_DETALLE_PEDIDO;
            dDatos.ID_PRODUCTO = d.ID_PRODUCTO;
            dDatos.CANTIDAD = d.CANTIDAD;


            return servicio.EditarDetallePedido(dDatos);
        }

        public Pedido ObtenerPedido(string pedido)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Pedido));
            StringReader reader = new StringReader(pedido);
            Modelo.Pedido p = (Modelo.Pedido)ser.Deserialize(reader);
            ServicioOrden serv = new ServicioOrden();
            Datos.PEDIDO Datos = new Datos.PEDIDO();
            Datos.NUMERO_PEDIDO = p.NUMERO_PEDIDO;

            if (serv.ObtenerPedido(Datos) == null)
            {
                return null;
            }
            else
            {
                Datos.PEDIDO Datos2 = serv.ObtenerPedido(Datos);

                p.NUMERO_PEDIDO = Datos2.NUMERO_PEDIDO;
                p.COMENTARIO = Datos2.COMENTARIO;
                p.ESTADO_DESPACHO = Datos2.ESTADO_DESPACHO;
                p.ESTADO_PEDIDO = Datos2.ESTADO_PEDIDO;
                p.FECHA_PEDIDO = Datos2.FECHA_PEDIDO;
                p.RUT_EMPLEADO = Datos2.RUT_EMPLEADO;
                p.RUT_PROVEEDOR = Datos2.RUT_PROVEEDOR;

                return p;
            }
        }

        public DetallePedido obtenerDetallePedido(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetallePedido));
            StringReader reader = new StringReader(detalle);
            Modelo.DetallePedido d = (Modelo.DetallePedido)ser.Deserialize(reader);
            ServicioOrden serv = new ServicioOrden();
            Datos.DETALLE_PEDIDO Datos = new Datos.DETALLE_PEDIDO();
            Datos.ID_DETALLE_PEDIDO = d.ID_DETALLE_PEDIDO;

            if (serv.obtenerDetallePedido(Datos) == null)
            {
                return null;
            }
            else
            {
                Datos.DETALLE_PEDIDO Datos2 = serv.obtenerDetallePedido(Datos);

                d.NUMERO_PEDIDO = Datos2.NUMERO_PEDIDO;
                d.ID_DETALLE_PEDIDO = Datos2.ID_DETALLE_PEDIDO;
                d.ID_PRODUCTO = Datos2.ID_PRODUCTO;
                d.CANTIDAD = Datos2.CANTIDAD;

                return d;
            }
        }

        public string ListarHistorialProveedor(string proveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Proveedor));
            StringReader reader = new StringReader(proveedor);
            Modelo.Proveedor pr = (Modelo.Proveedor)ser.Deserialize(reader);
            ServicioOrden serv = new ServicioOrden();

            Datos.PROVEEDOR pDatos = new Datos.PROVEEDOR();
            pDatos.RUT_PROVEEDOR = pr.RUT_PROVEEDOR;

            List<Datos.PEDIDO> listaPedido = serv.ListarHistorialProveedor(pDatos);

            if (listaPedido == null)
            {
                return null;
            }
            else
            {
                Modelo.PedidoCollection listaPedido2 = new PedidoCollection();

                foreach (Datos.PEDIDO p in listaPedido)
                {
                    Modelo.Pedido pModelo = new Modelo.Pedido();
                    pModelo.NUMERO_PEDIDO = p.NUMERO_PEDIDO;
                    pModelo.ESTADO_PEDIDO = p.ESTADO_PEDIDO;
                    pModelo.FECHA_PEDIDO = p.FECHA_PEDIDO;
                    pModelo.RUT_EMPLEADO = p.RUT_EMPLEADO;
                    pModelo.NUMERO_RECEPCION = p.NUMERO_RECEPCION;
                    pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
                    pModelo.ESTADO_DESPACHO = p.ESTADO_DESPACHO;
                    pModelo.COMENTARIO = p.COMENTARIO;

                    listaPedido2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaPedido2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public string ListarPedidoDespacho(string proveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Proveedor));
            StringReader reader = new StringReader(proveedor);
            Modelo.Proveedor pr = (Modelo.Proveedor)ser.Deserialize(reader);
            ServicioOrden serv = new ServicioOrden();

            Datos.PROVEEDOR pDatos = new Datos.PROVEEDOR();
            pDatos.RUT_PROVEEDOR = pr.RUT_PROVEEDOR;

            List<Datos.PEDIDO> listaPedido = serv.ListarPedidoDespacho(pDatos);

            if (listaPedido == null)
            {
                return null;
            }
            else
            {
                Modelo.PedidoCollection listaPedido2 = new PedidoCollection();

                foreach (Datos.PEDIDO p in listaPedido)
                {
                    Modelo.Pedido pModelo = new Modelo.Pedido();
                    pModelo.NUMERO_PEDIDO = p.NUMERO_PEDIDO;
                    pModelo.ESTADO_PEDIDO = p.ESTADO_PEDIDO;
                    pModelo.FECHA_PEDIDO = p.FECHA_PEDIDO;
                    pModelo.RUT_EMPLEADO = p.RUT_EMPLEADO;
                    pModelo.NUMERO_RECEPCION = p.NUMERO_RECEPCION;
                    pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
                    pModelo.ESTADO_DESPACHO = p.ESTADO_DESPACHO;
                    pModelo.COMENTARIO = p.COMENTARIO;

                    listaPedido2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaPedido2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public string ListarPedidosporDespachar(string proveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Proveedor));
            StringReader reader = new StringReader(proveedor);
            Modelo.Proveedor pr = (Modelo.Proveedor)ser.Deserialize(reader);
            ServicioOrden serv = new ServicioOrden();

            Datos.PROVEEDOR pDatos = new Datos.PROVEEDOR();
            pDatos.RUT_PROVEEDOR = pr.RUT_PROVEEDOR;

            List<Datos.PEDIDO> listaPedido = serv.ListarPedidosPorDespachar(pDatos);

            if (listaPedido == null)
            {
                return null;
            }
            else
            {
                Modelo.PedidoCollection listaPedido2 = new PedidoCollection();

                foreach (Datos.PEDIDO p in listaPedido)
                {
                    Modelo.Pedido pModelo = new Modelo.Pedido();
                    pModelo.NUMERO_PEDIDO = p.NUMERO_PEDIDO;
                    pModelo.ESTADO_PEDIDO = p.ESTADO_PEDIDO;
                    pModelo.FECHA_PEDIDO = p.FECHA_PEDIDO;
                    pModelo.RUT_EMPLEADO = p.RUT_EMPLEADO;
                    pModelo.NUMERO_RECEPCION = p.NUMERO_RECEPCION;
                    pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
                    pModelo.ESTADO_DESPACHO = p.ESTADO_DESPACHO;
                    pModelo.COMENTARIO = p.COMENTARIO;

                    listaPedido2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaPedido2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public string ListarDetallePedido(string pedido)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Pedido));
            StringReader reader = new StringReader(pedido);
            Modelo.Pedido pr = (Modelo.Pedido)ser.Deserialize(reader);
            ServicioOrden serv = new ServicioOrden();

            Datos.PEDIDO pDatos = new Datos.PEDIDO();
            pDatos.NUMERO_PEDIDO = pr.NUMERO_PEDIDO;

            List<Datos.DETALLE_PEDIDO> listaDetalle = serv.ListaDetallePedido(pDatos);

            if (listaDetalle == null)
            {
                return null;
            }
            else
            {
                Modelo.DetallePedidoCollection listaDetalle2 = new DetallePedidoCollection();

                foreach (Datos.DETALLE_PEDIDO p in listaDetalle)
                {
                    Modelo.DetallePedido pModelo = new Modelo.DetallePedido();
                    pModelo.NUMERO_PEDIDO = p.NUMERO_PEDIDO;
                    pModelo.ID_PRODUCTO = p.ID_PRODUCTO;
                    pModelo.ID_DETALLE_PEDIDO = p.ID_DETALLE_PEDIDO;
                    pModelo.CANTIDAD = p.CANTIDAD;

                    listaDetalle2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.DetallePedidoCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaDetalle2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public string ListarPedidoRecepcion()
        {
            ServicioOrden servicio = new ServicioOrden();
            List<Datos.PEDIDO> pedido = servicio.ListarPedidoRecepcion();
            Modelo.PedidoCollection listaPedido = new Modelo.PedidoCollection();

            foreach (Datos.PEDIDO p in pedido)
            {
                Modelo.Pedido pModelo = new Modelo.Pedido();
                pModelo.NUMERO_PEDIDO = p.NUMERO_PEDIDO;
                pModelo.ESTADO_PEDIDO = p.ESTADO_PEDIDO;
                pModelo.FECHA_PEDIDO = p.FECHA_PEDIDO;
                pModelo.RUT_EMPLEADO = p.RUT_EMPLEADO;
                pModelo.NUMERO_RECEPCION = p.NUMERO_RECEPCION;
                pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
                pModelo.ESTADO_DESPACHO = p.ESTADO_DESPACHO;
                pModelo.COMENTARIO = p.COMENTARIO;

                listaPedido.Add(pModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PedidoCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaPedido);
            writer.Close();
            return writer.ToString();
        }

        public bool EliminarDetallePedido(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetallePedido));
            StringReader reader = new StringReader(detalle);
            Modelo.DetallePedido d = (Modelo.DetallePedido)ser.Deserialize(reader);
            ServicioOrden servicio = new ServicioOrden();

            Datos.DETALLE_PEDIDO dDatos = new Datos.DETALLE_PEDIDO();
            //Datos Detalle
            dDatos.ID_DETALLE_PEDIDO = d.ID_DETALLE_PEDIDO;

            return servicio.EliminarDetallePedido(dDatos);
        }
        #endregion

        #region Notificacion
        //CRUD Notificacion
        public string listaNotificacion(string usuario)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(usuario);
            Modelo.Usuario u = (Modelo.Usuario)ser.Deserialize(reader);
            ServicioNotificacion serv = new ServicioNotificacion();

            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.ID_USUARIO = u.ID_USUARIO;
            uDatos.TIPO_USUARIO = u.TIPO_USUARIO;

            List<Datos.NOTIFICACION> listaNotificacion = serv.ListaNotificacion(uDatos);

            if (listaNotificacion == null)
            {
                return null;
            }
            else
            {
                XmlSerializer servicio = new XmlSerializer(typeof(Modelo.Notificacion));
                Modelo.NotificacionCollection listaNotificacion2 = new Modelo.NotificacionCollection();

                foreach (Datos.NOTIFICACION n in listaNotificacion)
                {
                    Modelo.Notificacion nModelo = new Modelo.Notificacion();
                    nModelo.ID_NOTIFICACION = n.ID_NOTIFICACION;
                    nModelo.MENSAJE = n.MENSAJE;
                    nModelo.ESTADO_NOTIFICACION = n.ESTADO_NOTIFICACION;
                    nModelo.URL = n.URL;
                    nModelo.NUMERO_ORDEN = n.NUMERO_ORDEN;
                    nModelo.ID_PRODUCTO = n.ID_PRODUCTO;
                    nModelo.NUMERO_PEDIDO = n.NUMERO_PEDIDO;

                    listaNotificacion2.Add(nModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.NotificacionCollection));
                StringWriter writer = new StringWriter();
                ser2.Serialize(writer, listaNotificacion2);
                writer.Close();
                return writer.ToString();
            }
        }

        public string HistorialNotificacion(string usuario)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(usuario);
            Modelo.Usuario u = (Modelo.Usuario)ser.Deserialize(reader);
            ServicioNotificacion serv = new ServicioNotificacion();

            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.ID_USUARIO = u.ID_USUARIO;

            List<Datos.NOTIFICACION> listaNotificacion = serv.HistorialNotificacion(uDatos);

            if (listaNotificacion == null)
            {
                return null;
            }
            else
            {
                XmlSerializer servicio = new XmlSerializer(typeof(Modelo.Notificacion));
                Modelo.NotificacionCollection listaNotificacion2 = new Modelo.NotificacionCollection();

                foreach (Datos.NOTIFICACION n in listaNotificacion)
                {
                    Modelo.Notificacion nModelo = new Modelo.Notificacion();
                    nModelo.ID_NOTIFICACION = n.ID_NOTIFICACION;
                    nModelo.MENSAJE = n.MENSAJE;
                    nModelo.ESTADO_NOTIFICACION = n.ESTADO_NOTIFICACION;
                    nModelo.URL = n.URL;
                    nModelo.NUMERO_ORDEN = n.NUMERO_ORDEN;
                    nModelo.ID_PRODUCTO = n.ID_PRODUCTO;
                    nModelo.NUMERO_PEDIDO = n.NUMERO_PEDIDO;

                    listaNotificacion2.Add(nModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.NotificacionCollection));
                StringWriter writer = new StringWriter();
                ser2.Serialize(writer, listaNotificacion2);
                writer.Close();
                return writer.ToString();
            }
        }
        #endregion

        #region Recepcion
        //CRUD Recepcion
        public bool AgregarRecepcion(string recepcion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Recepcion));
            StringReader reader = new StringReader(recepcion);
            Modelo.Recepcion r = (Modelo.Recepcion)ser.Deserialize(reader);
            ServicioRecepcion serv = new ServicioRecepcion();
            Datos.RECEPCION rDatos = new Datos.RECEPCION();
            rDatos.RUT_EMPLEADO = r.RUT_EMPLEADO;
            rDatos.RUT_PROVEEDOR = r.RUT_PROVEEDOR;
            rDatos.FECHA_RECEPCION = r.FECHA_RECEPCION;

            return serv.AgregarRecepcion(rDatos);
        }

        public bool AgregarDetalleRecepcion(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetalleRecepcion));
            StringReader reader = new StringReader(detalle);
            Modelo.DetalleRecepcion d = (Modelo.DetalleRecepcion)ser.Deserialize(reader);
            ServicioRecepcion serv = new ServicioRecepcion();
            Datos.DETALLE_RECEPCION dDatos = new Datos.DETALLE_RECEPCION();
            dDatos.ID_PRODUCTO = d.ID_PRODUCTO;
            dDatos.CANTIDAD = d.CANTIDAD;

            return serv.AgregarDetalleRecepcion(dDatos);
        }

        #endregion

        #region Reserva
        //CRUD Reserva
        public bool AgregarReserva(string orden)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompra));
            StringReader reader = new StringReader(orden);
            Modelo.OrdenCompra o = (Modelo.OrdenCompra)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();
            Datos.ORDEN_COMPRA oDatos = new Datos.ORDEN_COMPRA();
            oDatos.RUT_CLIENTE = o.RUT_CLIENTE;
            oDatos.FECHA_LLEGADA = o.FECHA_LLEGADA;
            oDatos.CANTIDAD_HUESPEDES = o.CANTIDAD_HUESPEDES;
            oDatos.ESTADO_ORDEN = o.ESTADO_ORDEN;
            oDatos.FECHA_SALIDA = o.FECHA_SALIDA;

            return serv.AgregarOrdenCompra(oDatos);
        }

        public bool AgregarDetalleReserva(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetalleOrden));
            StringReader reader = new StringReader(detalle);
            Modelo.DetalleOrden d = (Modelo.DetalleOrden)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();
            Datos.DETALLE_ORDEN dDatos = new Datos.DETALLE_ORDEN();
            dDatos.RUT_HUESPED = d.RUT_HUESPED;
            dDatos.ID_PENSION = d.ID_PENSION;
            dDatos.ID_CATEGORIA_HABITACION = d.ID_CATEGORIA_HABITACION;
            dDatos.ESTADO = d.ESTADO;

            return serv.AgregarDetalleOrden(dDatos);
        }

        public string HistorialReserva(string cliente)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Cliente));
            StringReader reader = new StringReader(cliente);
            Modelo.Cliente pr = (Modelo.Cliente)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();

            Datos.CLIENTE cDatos = new Datos.CLIENTE();
            cDatos.RUT_CLIENTE = pr.RUT_CLIENTE;

            List<Datos.ORDEN_COMPRA> listaOrden = serv.HistorialOrdenCompra(cDatos);

            if (listaOrden == null)
            {
                return null;
            }
            else
            {
                Modelo.OrdenCompraCollection listaOrden2 = new OrdenCompraCollection();

                foreach (Datos.ORDEN_COMPRA o in listaOrden)
                {
                    Modelo.OrdenCompra oModelo = new Modelo.OrdenCompra();
                    oModelo.NUMERO_ORDEN = o.NUMERO_ORDEN;
                    oModelo.CANTIDAD_HUESPEDES = o.CANTIDAD_HUESPEDES;
                    oModelo.FECHA_LLEGADA = o.FECHA_LLEGADA;
                    oModelo.FECHA_SALIDA = o.FECHA_SALIDA;
                    oModelo.RUT_EMPLEADO = o.RUT_EMPLEADO;
                    oModelo.RUT_CLIENTE = o.RUT_CLIENTE;
                    oModelo.ESTADO_ORDEN = o.ESTADO_ORDEN;
                    oModelo.COMENTARIO = o.COMENTARIO;

                    listaOrden2.Add(oModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaOrden2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public string HistorialReservaPendiente(string cliente)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Cliente));
            StringReader reader = new StringReader(cliente);
            Modelo.Cliente pr = (Modelo.Cliente)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();

            Datos.CLIENTE cDatos = new Datos.CLIENTE();
            cDatos.RUT_CLIENTE = pr.RUT_CLIENTE;

            List<Datos.ORDEN_COMPRA> listaOrden = serv.HistorialOrdenCompraPendiente(cDatos);

            if (listaOrden == null)
            {
                return null;
            }
            else
            {
                Modelo.OrdenCompraCollection listaOrden2 = new OrdenCompraCollection();

                foreach (Datos.ORDEN_COMPRA o in listaOrden)
                {
                    Modelo.OrdenCompra oModelo = new Modelo.OrdenCompra();
                    oModelo.NUMERO_ORDEN = o.NUMERO_ORDEN;
                    oModelo.CANTIDAD_HUESPEDES = o.CANTIDAD_HUESPEDES;
                    oModelo.FECHA_LLEGADA = o.FECHA_LLEGADA;
                    oModelo.FECHA_SALIDA = o.FECHA_SALIDA;
                    oModelo.RUT_EMPLEADO = o.RUT_EMPLEADO;
                    oModelo.RUT_CLIENTE = o.RUT_CLIENTE;
                    oModelo.ESTADO_ORDEN = o.ESTADO_ORDEN;
                    oModelo.COMENTARIO = o.COMENTARIO;

                    listaOrden2.Add(oModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaOrden2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public OrdenCompra ObtenerReserva(string reserva)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompra));
            StringReader reader = new StringReader(reserva);
            Modelo.OrdenCompra o = (Modelo.OrdenCompra)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();
            Datos.ORDEN_COMPRA Datos = new Datos.ORDEN_COMPRA();
            Datos.NUMERO_ORDEN = o.NUMERO_ORDEN;

            if (serv.ObtenerReserva(Datos) == null)
            {
                return null;
            }
            else
            {
                Datos.ORDEN_COMPRA Datos2 = serv.ObtenerReserva(Datos);

                o.NUMERO_ORDEN = Datos2.NUMERO_ORDEN;
                o.CANTIDAD_HUESPEDES = Datos2.CANTIDAD_HUESPEDES;
                o.FECHA_LLEGADA = Datos2.FECHA_LLEGADA;
                o.FECHA_SALIDA = Datos2.FECHA_SALIDA;
                o.RUT_EMPLEADO = Datos2.RUT_EMPLEADO;
                o.RUT_CLIENTE = Datos2.RUT_CLIENTE;
                o.ESTADO_ORDEN = Datos2.ESTADO_ORDEN;
                o.COMENTARIO = Datos2.COMENTARIO;

                return o;
            }
        }

        public string ListarDetalleReserva(string orden)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompra));
            StringReader reader = new StringReader(orden);
            Modelo.OrdenCompra or = (Modelo.OrdenCompra)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();

            Datos.ORDEN_COMPRA oDatos = new Datos.ORDEN_COMPRA();
            oDatos.NUMERO_ORDEN = or.NUMERO_ORDEN;

            List<Datos.DETALLE_ORDEN> listaDetalle = serv.ListaDetalleReserva(oDatos);

            if (listaDetalle == null)
            {
                return null;
            }
            else
            {
                Modelo.DetalleOrdenCollection listaDetalle2 = new DetalleOrdenCollection();

                foreach (Datos.DETALLE_ORDEN o in listaDetalle)
                {
                    Modelo.DetalleOrden oModelo = new Modelo.DetalleOrden();
                    oModelo.NUMERO_ORDEN = o.NUMERO_ORDEN;
                    oModelo.ID_PENSION = o.ID_PENSION;
                    oModelo.RUT_HUESPED = o.RUT_HUESPED;
                    oModelo.ID_CATEGORIA_HABITACION = o.ID_CATEGORIA_HABITACION;
                    oModelo.ID_DETALLE = o.ID_DETALLE;
                    oModelo.ESTADO = o.ESTADO;

                    listaDetalle2.Add(oModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.DetalleOrdenCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaDetalle2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public string ListarReservaAdmin()
        {

            ServicioReserva servicio = new ServicioReserva();
            List<Datos.ORDEN_COMPRA> orden = servicio.ListarReservaAdmin();
            Modelo.OrdenCompraCollection listaOrden = new Modelo.OrdenCompraCollection();

            foreach (Datos.ORDEN_COMPRA o in orden)
            {
                Modelo.OrdenCompra oModelo = new Modelo.OrdenCompra();
                oModelo.NUMERO_ORDEN = o.NUMERO_ORDEN;
                oModelo.CANTIDAD_HUESPEDES = o.CANTIDAD_HUESPEDES;
                oModelo.FECHA_LLEGADA = o.FECHA_LLEGADA;
                oModelo.FECHA_SALIDA = o.FECHA_SALIDA;
                oModelo.RUT_EMPLEADO = o.RUT_EMPLEADO;
                oModelo.RUT_CLIENTE = o.RUT_CLIENTE;
                oModelo.ESTADO_ORDEN = o.ESTADO_ORDEN;
                oModelo.COMENTARIO = o.COMENTARIO;

                listaOrden.Add(oModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaOrden);
            writer.Close();
            return writer.ToString();
        }

        public bool EditarEstadoReserva(string orden)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompra));
            StringReader reader = new StringReader(orden);
            Modelo.OrdenCompra o = (Modelo.OrdenCompra)ser.Deserialize(reader);
            ServicioReserva servicio = new ServicioReserva();

            Datos.ORDEN_COMPRA oDatos = new Datos.ORDEN_COMPRA();

            oDatos.NUMERO_ORDEN = o.NUMERO_ORDEN;
            oDatos.FECHA_LLEGADA = o.FECHA_LLEGADA;
            oDatos.FECHA_SALIDA = o.FECHA_SALIDA;
            oDatos.COMENTARIO = o.COMENTARIO;
            oDatos.ESTADO_ORDEN = o.ESTADO_ORDEN;
            oDatos.CANTIDAD_HUESPEDES = o.CANTIDAD_HUESPEDES;
            oDatos.RUT_CLIENTE = o.RUT_CLIENTE;
            oDatos.RUT_EMPLEADO = o.RUT_EMPLEADO;

            return servicio.EditarEstadoReserva(oDatos);
        }

        public string ListarReservaAceptada()
        {

            ServicioReserva servicio = new ServicioReserva();
            List<Datos.ORDEN_COMPRA> orden = servicio.ListarReservaAceptada();
            Modelo.OrdenCompraCollection listaOrden = new Modelo.OrdenCompraCollection();

            foreach (Datos.ORDEN_COMPRA o in orden)
            {
                Modelo.OrdenCompra oModelo = new Modelo.OrdenCompra();
                oModelo.NUMERO_ORDEN = o.NUMERO_ORDEN;
                oModelo.CANTIDAD_HUESPEDES = o.CANTIDAD_HUESPEDES;
                oModelo.FECHA_LLEGADA = o.FECHA_LLEGADA;
                oModelo.FECHA_SALIDA = o.FECHA_SALIDA;
                oModelo.RUT_EMPLEADO = o.RUT_EMPLEADO;
                oModelo.RUT_CLIENTE = o.RUT_CLIENTE;
                oModelo.ESTADO_ORDEN = o.ESTADO_ORDEN;
                oModelo.COMENTARIO = o.COMENTARIO;

                listaOrden.Add(oModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaOrden);
            writer.Close();
            return writer.ToString();
        }

        public string ListaHuespedesNoAsignados(string orden)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompra));
            StringReader reader = new StringReader(orden);
            Modelo.OrdenCompra or = (Modelo.OrdenCompra)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();

            Datos.ORDEN_COMPRA oDatos = new Datos.ORDEN_COMPRA();
            oDatos.NUMERO_ORDEN = or.NUMERO_ORDEN;

            List<Datos.DETALLE_ORDEN> listaDetalle = serv.ListaHuespedesNoAsignados(oDatos);

            if (listaDetalle == null)
            {
                return null;
            }
            else
            {
                Modelo.DetalleOrdenCollection listaDetalle2 = new DetalleOrdenCollection();

                foreach (Datos.DETALLE_ORDEN o in listaDetalle)
                {
                    Modelo.DetalleOrden oModelo = new Modelo.DetalleOrden();
                    oModelo.NUMERO_ORDEN = o.NUMERO_ORDEN;
                    oModelo.ID_PENSION = o.ID_PENSION;
                    oModelo.RUT_HUESPED = o.RUT_HUESPED;
                    oModelo.ID_CATEGORIA_HABITACION = o.ID_CATEGORIA_HABITACION;
                    oModelo.ID_DETALLE = o.ID_DETALLE;
                    oModelo.ESTADO = o.ESTADO;

                    listaDetalle2.Add(oModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.DetalleOrdenCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaDetalle2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public string ListaHuespedesAsignados(string orden)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompra));
            StringReader reader = new StringReader(orden);
            Modelo.OrdenCompra or = (Modelo.OrdenCompra)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();

            Datos.ORDEN_COMPRA oDatos = new Datos.ORDEN_COMPRA();
            oDatos.NUMERO_ORDEN = or.NUMERO_ORDEN;

            List<Datos.DETALLE_ORDEN> listaDetalle = serv.ListaHuespedesAsignados(oDatos);

            if (listaDetalle == null)
            {
                return null;
            }
            else
            {
                Modelo.DetalleOrdenCollection listaDetalle2 = new DetalleOrdenCollection();

                foreach (Datos.DETALLE_ORDEN o in listaDetalle)
                {
                    Modelo.DetalleOrden oModelo = new Modelo.DetalleOrden();
                    oModelo.NUMERO_ORDEN = o.NUMERO_ORDEN;
                    oModelo.ID_PENSION = o.ID_PENSION;
                    oModelo.RUT_HUESPED = o.RUT_HUESPED;
                    oModelo.ID_CATEGORIA_HABITACION = o.ID_CATEGORIA_HABITACION;
                    oModelo.ID_DETALLE = o.ID_DETALLE;
                    oModelo.ESTADO = o.ESTADO;

                    listaDetalle2.Add(oModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.DetalleOrdenCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaDetalle2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public DetalleOrden ObtenerDetalleReserva(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetalleOrden));
            StringReader reader = new StringReader(detalle);
            Modelo.DetalleOrden d = (Modelo.DetalleOrden)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();
            Datos.DETALLE_ORDEN Datos = new Datos.DETALLE_ORDEN();
            Datos.ID_DETALLE = d.ID_DETALLE;

            if (serv.ObtenerDetalleReserva(Datos) == null)
            {
                return null;
            }
            else
            {
                Datos.DETALLE_ORDEN Datos2 = serv.ObtenerDetalleReserva(Datos);

                d.ID_DETALLE = Datos2.ID_DETALLE;
                d.NUMERO_ORDEN = Datos2.NUMERO_ORDEN;
                d.ID_PENSION = Datos2.ID_PENSION;
                d.RUT_HUESPED = Datos2.RUT_HUESPED;
                d.ID_CATEGORIA_HABITACION = Datos2.ID_CATEGORIA_HABITACION;
                d.ESTADO = Datos2.ESTADO;

                return d;
            }
        }

        public bool EditarEstadoDetalleReserva(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetalleOrden));
            StringReader reader = new StringReader(detalle);
            Modelo.DetalleOrden d = (Modelo.DetalleOrden)ser.Deserialize(reader);
            ServicioReserva servicio = new ServicioReserva();

            Datos.DETALLE_ORDEN dDatos = new Datos.DETALLE_ORDEN();

            dDatos.ID_DETALLE = d.ID_DETALLE;
            dDatos.ID_PENSION = d.ID_PENSION;
            dDatos.ID_CATEGORIA_HABITACION = d.ID_CATEGORIA_HABITACION;
            dDatos.ESTADO = d.ESTADO;
            dDatos.RUT_HUESPED = d.RUT_HUESPED;
            dDatos.NUMERO_ORDEN = d.NUMERO_ORDEN;

            return servicio.EditarDetalleReserva(dDatos);
        }
        #endregion

        #region Detalle Habitacion
        public bool AgregarDetalleHabitacion(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetalleHabitacion));
            StringReader reader = new StringReader(detalle);
            Modelo.DetalleHabitacion d = (Modelo.DetalleHabitacion)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();
            Datos.DETALLE_HABITACION dDatos = new Datos.DETALLE_HABITACION();
            dDatos.NUMERO_HABITACION = d.NUMERO_HABITACION;
            dDatos.RUT_CLIENTE = d.RUT_CLIENTE;
            dDatos.RUT_HUESPED = d.RUT_HUESPED;
            dDatos.ID_PENSION = d.ID_PENSION;
            dDatos.FECHA_LLEGADA = d.FECHA_LLEGADA;
            dDatos.FECHA_SALIDA = d.FECHA_SALIDA;

            return serv.AgregarDetalleHabitacion(dDatos);
        }

        public bool EliminarDetalleHabitacion(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetalleHabitacion));
            StringReader reader = new StringReader(detalle);
            Modelo.DetalleHabitacion d = (Modelo.DetalleHabitacion)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();
            Datos.DETALLE_HABITACION dDatos = new Datos.DETALLE_HABITACION();
            dDatos.NUMERO_HABITACION = d.NUMERO_HABITACION;
            dDatos.RUT_CLIENTE = d.RUT_CLIENTE;

            return serv.AgregarDetalleHabitacion(dDatos);
        }
        #endregion

        #region Detalle Pasajeros
        /*
        public bool AgregarDetallePasajeros(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetallePasajeros));
            StringReader reader = new StringReader(detalle);
            Modelo.DetallePasajeros d = (Modelo.DetallePasajeros)ser.Deserialize(reader);
            ServicioReserva serv = new ServicioReserva();
            Datos.DETALLE_PASAJEROS dDatos = new Datos.DETALLE_PASAJEROS();
            dDatos.NUMERO_HABITACION = d.NUMERO_HABITACION;
            dDatos.RUT_HUESPED = d.RUT_HUESPED;
            dDatos.FECHA_LLEGADA = d.FECHA_LLEGADA;
            dDatos.FECHA_SALIDA = d.FECHA_SALIDA;
            dDatos.ID_PENSION = d.ID_PENSION;

            return serv.AgregarDetallePasajeros(dDatos);
        }
        */
        #endregion

        #region Minuta
        public bool AgregarMinuta(string pension)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Minuta));
            StringReader reader = new StringReader(pension);
            Modelo.Minuta p = (Modelo.Minuta)ser.Deserialize(reader);
            ServicioMinuta serv = new ServicioMinuta();
            Datos.PENSION pdatos = new Datos.PENSION();
            pdatos.NOMBRE_PENSION = p.NOMBRE_PENSION;
            pdatos.VALOR_PENSION = p.VALOR_PENSION;
            pdatos.HABILITADO = p.HABILITADO;

            return serv.AgregarMinuta(pdatos);
        }

        public bool AgregarDetalleMinuta(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetallePlato));
            StringReader reader = new StringReader(detalle);
            Modelo.DetallePlato d = (Modelo.DetallePlato)ser.Deserialize(reader);
            ServicioMinuta serv = new ServicioMinuta();
            Datos.DETALLE_PLATOS dDatos = new Datos.DETALLE_PLATOS();
            dDatos.ID_PLATO = d.ID_PLATO;
            dDatos.CANTIDAD = d.CANTIDAD;

            return serv.AgregarDetalleMinuta(dDatos);
        }

        public DetallePlato obtenerDetallePlato(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetallePlato));
            StringReader reader = new StringReader(detalle);
            Modelo.DetallePlato d = (Modelo.DetallePlato)ser.Deserialize(reader);
            ServicioMinuta serv = new ServicioMinuta();
            Datos.DETALLE_PLATOS Datos = new Datos.DETALLE_PLATOS();
            Datos.ID_DETALLE_PLATOS = d.ID_DETALLE_PLATOS;

            if (serv.obtenerDetallePlatos(Datos) == null)
            {
                return null;
            }
            else
            {
                Datos.DETALLE_PLATOS Datos2 = serv.obtenerDetallePlatos(Datos);


                d.CANTIDAD = Datos2.CANTIDAD;
                d.ID_DETALLE_PLATOS = Datos2.ID_DETALLE_PLATOS;
                d.ID_PENSION = Datos2.ID_PENSION;
                d.ID_PLATO = Datos2.ID_PLATO;
             

                return d;
            }
        }

        public bool EliminarDetallePlatos(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetallePlato));
            StringReader reader = new StringReader(detalle);
            Modelo.DetallePlato d = (Modelo.DetallePlato)ser.Deserialize(reader);
            ServicioMinuta servicio = new ServicioMinuta();

            Datos.DETALLE_PLATOS dDatos = new Datos.DETALLE_PLATOS();
            //Datos Detalle
            dDatos.ID_DETALLE_PLATOS = d.ID_DETALLE_PLATOS;

            return servicio.EliminarDetallePlatos(dDatos);
        }

        public string ListarDetalleMinuta(string minuta)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Minuta));
            StringReader reader = new StringReader(minuta);
            Modelo.Minuta m = (Modelo.Minuta)ser.Deserialize(reader);
            ServicioMinuta serv = new ServicioMinuta();

            Datos.PENSION pDatos = new Datos.PENSION();
            pDatos.ID_PENSION = m.ID_PENSION;

            List<Datos.DETALLE_PLATOS> listaDetalle = serv.ListaDetalleMinuta(pDatos);

            if (listaDetalle == null)
            {
                return null;
            }
            else
            {
                Modelo.DetallePlatoCollection listaDetalle2 = new DetallePlatoCollection();

                foreach (Datos.DETALLE_PLATOS p in listaDetalle)
                {
                    Modelo.DetallePlato pModelo = new Modelo.DetallePlato();
                    pModelo.ID_PLATO = p.ID_PLATO;
                    pModelo.ID_PENSION = p.ID_PENSION;
                    pModelo.CANTIDAD = p.CANTIDAD;
                    pModelo.ID_DETALLE_PLATOS = p.ID_DETALLE_PLATOS;

                    listaDetalle2.Add(pModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.DetallePlatoCollection));
                StringWriter writer2 = new StringWriter();
                ser2.Serialize(writer2, listaDetalle2);
                writer2.Close();
                return writer2.ToString();
            }
        }

        public string ListarMinuta()
        {

            ServicioMinuta servicio = new ServicioMinuta();
            List<Datos.PENSION> minuta = servicio.ListarMinuta();
            Modelo.MinutaCollection listaMinuta = new Modelo.MinutaCollection();

            foreach (Datos.PENSION p in minuta)
            {
                Modelo.Minuta pMinuta = new Modelo.Minuta();

                pMinuta.ID_PENSION = p.ID_PENSION;
                pMinuta.NOMBRE_PENSION = p.NOMBRE_PENSION;
                pMinuta.VALOR_PENSION = p.VALOR_PENSION;
                pMinuta.HABILITADO = p.HABILITADO;

                listaMinuta.Add(pMinuta);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.MinutaCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaMinuta);
            writer.Close();
            return writer.ToString();
        }

        public bool EliminarMinuta(string pension)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Minuta));
            StringReader reader = new StringReader(pension);
            Modelo.Minuta m = (Modelo.Minuta)ser.Deserialize(reader);
            ServicioMinuta serv = new ServicioMinuta();
            Datos.PENSION pDatos = new Datos.PENSION();

            pDatos.ID_PENSION = m.ID_PENSION;

            return serv.EliminarMinuta(pDatos);
        }


        public bool ModificarMinuta(string pension)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Minuta));
            StringReader reader = new StringReader(pension);
            Modelo.Minuta m = (Modelo.Minuta)ser.Deserialize(reader);
            ServicioMinuta serv = new ServicioMinuta();
            Datos.PENSION pDatos = new Datos.PENSION();

            pDatos.ID_PENSION = m.ID_PENSION;
            pDatos.HABILITADO = "F";
    

            return serv.EditarMinuta(pDatos);
        }

        public bool ExisteMinuta(string pension)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Minuta));
            StringReader reader = new StringReader(pension);
            Modelo.Minuta m = (Modelo.Minuta)ser.Deserialize(reader);
            ServicioMinuta serv = new ServicioMinuta();
            Datos.PENSION pDatos = new Datos.PENSION();
            pDatos.ID_PENSION = m.ID_PENSION;

            if (!serv.ExisteMinuta(pDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Minuta ObtenerMinuta(string pension)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Minuta));
            StringReader reader = new StringReader(pension);
            Modelo.Minuta m = (Modelo.Minuta)ser.Deserialize(reader);
            ServicioMinuta serv = new ServicioMinuta();
            Datos.PENSION pDatos = new Datos.PENSION();
            pDatos.ID_PENSION = m.ID_PENSION;

            if (!serv.ExisteMinuta(pDatos))
            {
                return null;
            }
            else
            {
                Datos.PENSION pDatos2 = serv.ObtenerMinuta(pDatos);

                m.ID_PENSION = pDatos2.ID_PENSION;
                m.NOMBRE_PENSION = pDatos2.NOMBRE_PENSION;
                m.VALOR_PENSION = pDatos2.VALOR_PENSION;
                m.HABILITADO = pDatos2.HABILITADO;
                

                return m;
            }
        }

       


        #endregion

        #region DDL
        //DDL
        public string ListarPais()
        {
            ServicioDireccion servicio = new ServicioDireccion();
            List<Datos.PAIS> pais = servicio.ListarPaises();
            Modelo.PaisCollection listaPais = new Modelo.PaisCollection();
            foreach (Datos.PAIS p in pais)
            {
                Modelo.Pais pModelo = new Modelo.Pais();
                pModelo.ID_PAIS = p.ID_PAIS;
                pModelo.NOMBRE_PAIS = p.NOMBRE_PAIS;
                listaPais.Add(pModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PaisCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaPais);
            return writer.ToString();
        }

        public string ListarRegion()
        {
            ServicioDireccion servicio = new ServicioDireccion();
            List<Datos.REGION> region = servicio.ListarRegion();
            Modelo.RegionCollection listaRegion = new Modelo.RegionCollection();
            foreach (Datos.REGION r in region)
            {
                Modelo.Region rModelo = new Modelo.Region();
                rModelo.Id_Pais = r.ID_PAIS;
                rModelo.Id_Region = r.ID_REGION;
                rModelo.Nombre = r.NOMBRE_REGION;
                listaRegion.Add(rModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.RegionCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaRegion);
            return writer.ToString();
        }
        
        public string ListarComuna()
        {
            ServicioDireccion servicio = new ServicioDireccion();
            List<Datos.COMUNA> comuna = servicio.ListarComuna();
            Modelo.ComunaCollection listaComuna = new Modelo.ComunaCollection();
            foreach (Datos.COMUNA c in comuna)
            {
                Modelo.Comuna cModelo = new Modelo.Comuna();
                cModelo.Id_Region = c.ID_REGION;
                cModelo.Id_Comuna = c.ID_COMUNA;
                cModelo.Nombre = c.NOMBRE_COMUNA;
                listaComuna.Add(cModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.ComunaCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaComuna);
            return writer.ToString();
        }

        public string ListarTipoHabitacion()
        {
            ServicioHabitacion servicio = new ServicioHabitacion();
            List<Datos.TIPO_HABITACION> tipo_habitacion = servicio.ListarTipoHabitacion();
            Modelo.TipoHabitacionCollection listaTipoHabitacion = new Modelo.TipoHabitacionCollection();
            foreach (Datos.TIPO_HABITACION t in tipo_habitacion)
            {
                Modelo.TipoHabitacion tModelo = new Modelo.TipoHabitacion();
                tModelo.ID_TIPO_HABITACION = t.ID_TIPO_HABITACION;
                tModelo.NOMBRE_TIPO_HABITACION = t.NOMBRE_TIPO_HABITACION;
                tModelo.CANTIDAD_PASAJERO = t.CANTIDAD_PASAJERO;
                listaTipoHabitacion.Add(tModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoHabitacionCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaTipoHabitacion);
            return writer.ToString();
        }

        public string ListarCategoriaHabitacion()
        {
            ServicioCategoria servicio = new ServicioCategoria();
            List<Datos.CATEGORIA_HABITACION> categoria = servicio.ListarCategoriaHabitacion();
            Modelo.CategoriaHabitacionCollection listaCateegoriaHabitacion = new Modelo.CategoriaHabitacionCollection();
            foreach (Datos.CATEGORIA_HABITACION c in categoria)
            {
                Modelo.CategoriaHabitacion cModelo = new Modelo.CategoriaHabitacion();
                cModelo.ID_CATEGORIA_HABITACION = c.ID_CATEGORIA_HABITACION;
                cModelo.NOMBRE_CATEGORIA = c.NOMBRE_CATEGORIA;
                cModelo.PRECIO_CATEGORIA = c.PRECIO_CATEGORIA;
                listaCateegoriaHabitacion.Add(cModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.CategoriaHabitacionCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaCateegoriaHabitacion);
            return writer.ToString();
        }

        public string ListarCategoria()
        {
            ServicioPlato servicio = new ServicioPlato();
            List<Datos.CATEGORIA> categoria = servicio.ListarCategoria();
            Modelo.CategoriaCollection listaCategoria = new Modelo.CategoriaCollection();
            foreach (Datos.CATEGORIA c in categoria)
            {
                Modelo.Categoria cModelo = new Modelo.Categoria();
                cModelo.ID_CATEGORIA = c.ID_CATEGORIA;
                cModelo.NOMBRE_CATEGORIA = c.NOMBRE_CATEGORIA;
                listaCategoria.Add(cModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.CategoriaCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaCategoria);
            return writer.ToString();
        }

        public string ListarTipoPlato()
        {
            ServicioPlato servicio = new ServicioPlato();
            List<Datos.TIPO_PLATO> TipoPlato = servicio.ListarTipoPlato();
            Modelo.TipoPlatoCollection listaPlatos = new Modelo.TipoPlatoCollection();
            foreach (Datos.TIPO_PLATO c in TipoPlato)
            {
                Modelo.TipoPlato cModelo = new Modelo.TipoPlato();
                cModelo.ID_TIPO_PLATO = c.ID_TIPO_PLATO;
                cModelo.NOMBRE_TIPO_PLATO = c.NOMBRE_TIPO_PLATO;
                listaPlatos.Add(cModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlatoCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaPlatos);
            return writer.ToString();
        }

        public string ListarFamilia()
        {
            ServicioProducto servicio = new ServicioProducto();
            List<Datos.FAMILIA> familia = servicio.ListarFamilia();
            Modelo.FamiliaCollection listaFamilia = new Modelo.FamiliaCollection();

            foreach (Datos.FAMILIA f in familia)
            {
                Modelo.Familia fModelo = new Modelo.Familia();
                fModelo.ID_FAMILIA = f.ID_FAMILIA;
                fModelo.NOMBRE_FAMILIA = f.NOMBRE_FAMILIA;
                
                listaFamilia.Add(fModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.FamiliaCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaFamilia);
            writer.Close();
            return writer.ToString();
        }

        public string ListarHuespedService(string cliente)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Cliente));
            StringReader reader = new StringReader(cliente);
            Modelo.Cliente c = (Modelo.Cliente)ser.Deserialize(reader);
            ServicioCliente serv = new ServicioCliente();

            Datos.CLIENTE cDatos = new Datos.CLIENTE();
            cDatos.RUT_CLIENTE = c.RUT_CLIENTE;

            List<Datos.HUESPED> listaHuesped = serv.ListarHuesped(cDatos);

            if (listaHuesped == null)
            {
                return null;
            }
            else
            {
                XmlSerializer servicio = new XmlSerializer(typeof(Modelo.Huesped));
                Modelo.HuespedCollection listaHuesped2 = new Modelo.HuespedCollection();

                foreach (Datos.HUESPED h in listaHuesped)
                {
                    Modelo.Huesped hModelo = new Modelo.Huesped();
                    hModelo.RUT_HUESPED = h.RUT_HUESPED;
                    hModelo.DV_HUESPED = h.DV_HUESPED;
                    hModelo.PNOMBRE_HUESPED = h.PNOMBRE_HUESPED;
                    hModelo.SNOMBRE_HUESPED = h.SNOMBRE_HUESPED;
                    hModelo.APP_PATERNO_HUESPED = h.APP_PATERNO_HUESPED;
                    hModelo.APP_PATERNO_HUESPED = h.APP_PATERNO_HUESPED;

                    listaHuesped2.Add(hModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.HuespedCollection));
                StringWriter writer = new StringWriter();
                ser2.Serialize(writer, listaHuesped2);
                writer.Close();
                return writer.ToString();
            }
        }

       /* public string ListarMinuta()
        {
            ServicioMinuta servicio = new ServicioMinuta();
            List<Datos.PENSION> minuta = servicio.ListarMinuta();
            Modelo.PensionCollection listaPension = new Modelo.PensionCollection();

            foreach (Datos.PENSION p in minuta)
            {
                Modelo.Pension pModelo = new Modelo.Pension();
                pModelo.ID_PENSION = p.ID_PENSION;
                pModelo.NOMBRE_PENSION = p.NOMBRE_PENSION;
                pModelo.VALOR_PENSION = p.VALOR_PENSION;

                listaPension.Add(pModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PensionCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaPension);
            writer.Close();
            return writer.ToString();
        }
        */

        public string ListarHabitacionDisponibleCategoria(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetalleOrden));
            StringReader reader = new StringReader(detalle);
            Modelo.DetalleOrden d = (Modelo.DetalleOrden)ser.Deserialize(reader);
            ServicioHabitacion serv = new ServicioHabitacion();

            Datos.DETALLE_ORDEN dDatos = new Datos.DETALLE_ORDEN();
            dDatos.ID_CATEGORIA_HABITACION = d.ID_CATEGORIA_HABITACION;

            List<Datos.HABITACION> listaHabitacion = serv.listarHabitacionDisponibleCategoria(dDatos);

            if (listaHabitacion == null)
            {
                return null;
            }
            else
            {
                XmlSerializer servicio = new XmlSerializer(typeof(Modelo.Habitacion));
                Modelo.HabitacionCollection listaHabitacion2 = new Modelo.HabitacionCollection();

                foreach (Datos.HABITACION h in listaHabitacion)
                {
                    Modelo.Habitacion hModelo = new Modelo.Habitacion();
                    hModelo.NUMERO_HABITACION = h.NUMERO_HABITACION;
                    hModelo.ESTADO_HABITACION = h.ESTADO_HABITACION;
                    hModelo.ID_CATEGORIA_HABITACION = h.ID_CATEGORIA_HABITACION;
                    hModelo.ID_TIPO_HABITACION = h.ID_TIPO_HABITACION;

                    listaHabitacion2.Add(hModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.HabitacionCollection));
                StringWriter writer = new StringWriter();
                ser2.Serialize(writer, listaHabitacion2);
                writer.Close();
                return writer.ToString();
            }
        }

        public string ListarHabitacionDisponible(string detalle)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.DetalleOrden));
            StringReader reader = new StringReader(detalle);
            Modelo.DetalleOrden d = (Modelo.DetalleOrden)ser.Deserialize(reader);
            ServicioHabitacion serv = new ServicioHabitacion();

            Datos.DETALLE_ORDEN dDatos = new Datos.DETALLE_ORDEN();
            dDatos.ID_CATEGORIA_HABITACION = d.ID_CATEGORIA_HABITACION;

            List<Datos.HABITACION> listaHabitacion = serv.listarHabitacionDisponible(dDatos);

            if (listaHabitacion == null)
            {
                return null;
            }
            else
            {
                XmlSerializer servicio = new XmlSerializer(typeof(Modelo.Habitacion));
                Modelo.HabitacionCollection listaHabitacion2 = new Modelo.HabitacionCollection();

                foreach (Datos.HABITACION h in listaHabitacion)
                {
                    Modelo.Habitacion hModelo = new Modelo.Habitacion();
                    hModelo.NUMERO_HABITACION = h.NUMERO_HABITACION;
                    hModelo.ESTADO_HABITACION = h.ESTADO_HABITACION;
                    hModelo.ID_CATEGORIA_HABITACION = h.ID_CATEGORIA_HABITACION;
                    hModelo.ID_TIPO_HABITACION = h.ID_TIPO_HABITACION;

                    listaHabitacion2.Add(hModelo);
                }

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.HabitacionCollection));
                StringWriter writer = new StringWriter();
                ser2.Serialize(writer, listaHabitacion2);
                writer.Close();
                return writer.ToString();
            }
        }
        #endregion
    }
}
