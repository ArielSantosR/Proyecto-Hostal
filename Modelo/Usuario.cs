using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Usuario
    {
        public short ID_USUARIO { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string PASSWORD { get; set; }
        public string TIPO_USUARIO { get; set; }
        public string ESTADO { get; set; }

        public bool Update() {
            ServicioUsuario serv = new ServicioUsuario();
            USUARIO datos = new USUARIO();
            datos.ID_USUARIO = this.ID_USUARIO;
            datos.NOMBRE_USUARIO = this.NOMBRE_USUARIO;
            datos.PASSWORD = this.PASSWORD;
            datos.TIPO_USUARIO = this.TIPO_USUARIO;
            datos.ESTADO = this.ESTADO;

            return serv.ModificarUsuario(datos);
        }
    }
}
