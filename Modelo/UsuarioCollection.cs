using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class UsuarioCollection :List<Usuario>
    {
        public static List<Usuario> ListaUsuarios() {
            ServicioUsuario serv = new ServicioUsuario();
            return GenerarLista(serv.ListarUsuarios());
        }

        private static List<Usuario> GenerarLista(List<USUARIO> listDatos) {
            List<Usuario> list = new List<Usuario>();
            Usuario user;
            foreach (USUARIO dato in listDatos) {
                user = new Usuario();
                user.ESTADO = dato.ESTADO;
                user.ID_USUARIO = dato.ID_USUARIO;
                user.NOMBRE_USUARIO = dato.NOMBRE_USUARIO;
                user.PASSWORD = dato.PASSWORD;
                user.TIPO_USUARIO = dato.TIPO_USUARIO;

                list.Add(user);
            }
            return list;
        }
    }
}
