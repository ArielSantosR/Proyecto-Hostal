using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    public class ClienteCollection {


        public static List<Cliente> ListaClientes() {
            ServicioCliente serv = new ServicioCliente();
            return GenerarLista(serv.ListarCliente());
        }

        private static List<Cliente> GenerarLista(List<CLIENTE> listDatos) {
            List<Cliente> list = new List<Cliente>();
            Cliente cliente;
            foreach (CLIENTE dato in listDatos) {
                cliente = new Cliente();
                cliente.CORREO_CLIENTE = dato.CORREO_CLIENTE;
                cliente.DIRECCION_CLIENTE = dato.DIRECCION_CLIENTE;
                cliente.DV_CLIENTE = dato.DV_CLIENTE;
                cliente.ID_COMUNA = dato.ID_COMUNA;
                cliente.ID_USUARIO = dato.ID_USUARIO;
                cliente.NOMBRE_CLIENTE = dato.NOMBRE_CLIENTE;
                cliente.RUT_CLIENTE = dato.RUT_CLIENTE;
                cliente.TELEFONO_CLIENTE = dato.TELEFONO_CLIENTE;
                cliente.ID_GIRO = dato.ID_GIRO;

                list.Add(cliente);
            }
            return list;
        }
    }
}
