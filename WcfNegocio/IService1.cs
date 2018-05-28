using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfNegocio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {
        #region Usuario
        //CRUD Usuario
        [OperationContract]
        bool Login(string user);

        [OperationContract]
        Usuario GetUsuario(string user);

        [OperationContract]
        bool ExisteUsuario(string user);

        [OperationContract]
        bool RegistroUsuario(string user);

        [OperationContract]
        bool EliminarUsuario(string user);

        [OperationContract]
        bool ModificarUsuario(string user);

        [OperationContract]
        string ListarUsuarios();

        #endregion

        #region Huesped
        //Huesped
        [OperationContract]
        bool ExisteHuesped(string huesped);

        [OperationContract]
        bool RegistroHuesped(string huesped);

        [OperationContract]
        bool EliminarHuesped(string huesped);

        [OperationContract]
        bool ModificarHuesped(string huesped);

        [OperationContract]
        string ListarHuesped();

        #endregion

        #region Cliente
        //CRUD Cliente
        [OperationContract]
        bool ExisteRutC(string cliente);

        [OperationContract]
        bool RegistroCliente(string cliente);

        [OperationContract]
        Cliente buscarIDC(string cliente);
        #endregion

        #region Empleado
        //CRUD Empleado
        [OperationContract]
        bool ExisteRutE(string empleado);

        [OperationContract]
        Empleado buscarIDE (string empleado);

        [OperationContract]
        bool RegistroEmpleado(string empleado);
        #endregion

        #region Proveedor
        //CRUD Proveedor
        [OperationContract]
        bool ExisteRutP(string proveedor);

        [OperationContract]
        bool RegistroProveedor(string proveedor);

        [OperationContract]
        string ListarProveedor();

        [OperationContract]
        string ListarProductosProveedor(string proveedor);

        [OperationContract]
        Proveedor buscarIDP(string proveedor);

        #endregion

        #region Habitacion
        //CRUD Habitacion
        [OperationContract]
        bool AgregarHabitacion(string habitacion);

        [OperationContract]
        bool ExisteHabitacion(string habitacion);

        [OperationContract]
        string ListarHabitacion();

        [OperationContract]
        Habitacion ObtenerHabitacion(string habitacion);

        [OperationContract]
        bool ModificarHabitacion(string habitacion);

        [OperationContract]
        bool EliminarHabitacion(string habitacion);
        #endregion

        #region Producto
        //CRUD Producto
        [OperationContract]
        bool AgregarProducto(string producto);

        [OperationContract]
        bool ModificarProducto(string producto);

        [OperationContract]
        Producto ObtenerProducto(string producto);

        [OperationContract]
        bool ExisteProducto(string producto);

        [OperationContract]
        string ListarProducto();

        [OperationContract]
        bool EliminarProducto(string producto);

        [OperationContract]
        string ListarProveedorProducto(string producto);
        #endregion

        #region Plato
        //CRUD Plato
        [OperationContract]
        bool AgregarPlato(string plato);

        [OperationContract]
        bool ExistePlato(string plato);

        [OperationContract]
        Plato ObtenerPlato(string plato);

        [OperationContract]
        bool ModificarPlato(string plato);

        [OperationContract]
        string ListarPlato();

        [OperationContract]
        bool EliminarPlato(string plato);
        #endregion

        #region Tipo Plato
        //CRUD tipo plato
        [OperationContract]
        bool AgregarTipoPlato(string tipoPlato);

        [OperationContract]
        bool EliminarTipoPlato(string tipoPlato);

        [OperationContract]
        bool ModificarTipoPlato(string tipoPlato);

        [OperationContract]
        bool ExisteTipoPlato(string tipoPlato);

        [OperationContract]
        TipoPlato ObtenerTipoPlato(string tipoPlato);
        #endregion

        #region Categoría Plato
        //categoria_plato

        [OperationContract]
        bool AgregarCategoria(string categoria);

        [OperationContract]
        bool EliminarCategoria(string categoria);

        [OperationContract]
        bool ModificarCategoria(string categoria);

        [OperationContract]
        bool ExisteCategoria(string categoria);

        [OperationContract]
        Categoria ObtenerCategoria(string categoria);
        #endregion

        #region Pais
        //PAIS
        [OperationContract]
        bool AgregarPais(string pais);

        [OperationContract]
        bool EliminarPais(string pais);

        [OperationContract]
        bool ModificarPais(string pais);

        [OperationContract]
        bool ExistePais(string pais);

        [OperationContract]
        Pais ObtenerPais(string pais);
        #endregion

        #region Tipo Proveedor
        //CRUD TIPO PROVEEDOR
        [OperationContract]
        bool AgregarTipoProveedor(string tipoProveedor);

        [OperationContract]
        bool EliminarTipoProveedor(string tipoProveedor);

        [OperationContract]
        bool ModificarTipoProveedor(string tipoProveedor);

        [OperationContract]
        bool ExisteTipoProveedor(string tipoProveedor);

        [OperationContract]
        TipoProveedor ObtenerTipoProveedor(string tipoProveedor);
        #endregion

        #region Pedido
        //CRUD Pedido
        [OperationContract]
        bool AgregarPedido(string pedido);

        [OperationContract]
        bool EditarEstadoPedido(string pedido);

        [OperationContract]
        bool EditarDetallePedido(string detalle);

        [OperationContract]
        bool AgregarDetallePedido(string detalle);

        [OperationContract]
        string ListarPedidoAdmin();

        [OperationContract]
        string ListarPedidoProveedor(string proveedor);

        [OperationContract]
        string ListarPedidoEmpleadoListo(string empleado);

        [OperationContract]
        string ListarPedidoEmpleadoPendiente(string empleado);

        [OperationContract]
        Pedido ObtenerPedido(string pedido);

        [OperationContract]
        DetallePedido obtenerDetallePedido(string detalle);

        [OperationContract]
        string ListarHistorialProveedor(string proveedor);

        [OperationContract]
        string ListarPedidoDespacho(string proveedor);

        [OperationContract]
        string ListarPedidosporDespachar(string proveedor);

        [OperationContract]
        string ListarDetallePedido(string pedido);

        [OperationContract]
        string ListarPedidoRecepcion();

        [OperationContract]
        bool EliminarDetallePedido(string detalle);
        #endregion

        #region Recepcion
        //CRUD Recepcion
        [OperationContract]
        bool AgregarRecepcion(string recepcion);

        [OperationContract]
        bool AgregarDetalleRecepcion(string detalle);
        #endregion

        #region Notificacion
        //CRUD Notificacion
        [OperationContract]
        string listaNotificacion(string usuario);

        [OperationContract]
        string HistorialNotificacion(string usuario);
        #endregion

        #region Reserva
        //CRUD Reserva
        [OperationContract]
        bool AgregarReserva(string orden);

        [OperationContract]
        bool AgregarDetalleReserva(string detalle);

        [OperationContract]
        string HistorialReserva(string cliente);

        [OperationContract]
        OrdenCompra ObtenerReserva(string reserva);

        [OperationContract]
        string ListarDetalleReserva(string orden);
     
        [OperationContract]
        string ListarReservaAdmin();

        [OperationContract]
        bool EditarEstadoReserva(string orden);

        [OperationContract]
        string ListarReservaAceptada();

        [OperationContract]
        string ListaHuespedesNoAsignados(string orden);

        [OperationContract]
        DetalleOrden ObtenerDetalleReserva(string detalle);

        [OperationContract]
        bool EditarEstadoDetalleReserva(string detalle);
        #endregion

        #region Detalle Habitacion
        [OperationContract]
        bool AgregarDetalleHabitacion(string detalle);
        #endregion

        #region Detalle Pasajeros
        [OperationContract]
        bool AgregarDetallePasajeros(string detalle);
        #endregion

        #region DDL
        //DDL
        [OperationContract]
        string ListarTipoProveedor();

        [OperationContract]
        string ListarPais();

        [OperationContract]
        string ListarComuna();

        [OperationContract]
        string ListarRegion();

        [OperationContract]
        string ListarTipoHabitacion();

        [OperationContract]
        string ListarCategoriaHabitacion();

        [OperationContract]
        string ListarTipoPlato();

        [OperationContract]
        string ListarFamilia();

        [OperationContract]
        string ListarHuespedService(string cliente);

        [OperationContract]
        string ListarMinuta();

        [OperationContract]
        string ListarHabitacionDisponibleCategoria(string detalle);

        [OperationContract]
        string ListarHabitacionDisponible(string detalle);
        #endregion
    }
}
