using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Cliente
    {
        public int RUT_CLIENTE { get; set; }
        public string DV_CLIENTE { get; set; }
        public string DIRECCION_CLIENTE { get; set; }
        public string CORREO_CLIENTE { get; set; }
        public Nullable<long> TELEFONO_CLIENTE { get; set; }
        public short ID_COMUNA { get; set; }
        public short ID_USUARIO { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public short ID_GIRO { get; set; }

        public bool Update() {
            ServicioCliente serv = new ServicioCliente();
            CLIENTE datos = new CLIENTE();
            datos.CORREO_CLIENTE = this.CORREO_CLIENTE;
            datos.DIRECCION_CLIENTE = this.DIRECCION_CLIENTE;
            datos.DV_CLIENTE = this.DV_CLIENTE;
            datos.ID_COMUNA = this.ID_COMUNA;
            datos.ID_USUARIO = this.ID_USUARIO;
            datos.NOMBRE_CLIENTE = this.NOMBRE_CLIENTE;
            datos.RUT_CLIENTE = this.RUT_CLIENTE;
            datos.TELEFONO_CLIENTE = this.TELEFONO_CLIENTE;
            datos.ID_GIRO = this.ID_GIRO;

            return serv.UpdateCliente(datos);
        }

        public bool BuscarCliente(short ID_USUARIO) {
            ServicioCliente serv = new ServicioCliente();
            CLIENTE datos = serv.BuscarCliente(ID_USUARIO);
            if (datos != null) {
                this.CORREO_CLIENTE = datos.CORREO_CLIENTE;
                this.DIRECCION_CLIENTE = datos.DIRECCION_CLIENTE;
                this.DV_CLIENTE = datos.DV_CLIENTE;
                this.ID_COMUNA = datos.ID_COMUNA;
                this.ID_USUARIO = datos.ID_USUARIO;
                this.NOMBRE_CLIENTE = datos.NOMBRE_CLIENTE;
                this.RUT_CLIENTE = datos.RUT_CLIENTE;
                this.TELEFONO_CLIENTE = datos.TELEFONO_CLIENTE;
                this.ID_GIRO = datos.ID_GIRO;

                return true;
            }
            else {
                return false;
            }
        }

        public bool BuscarCliente() {
            ServicioCliente serv = new ServicioCliente();
            CLIENTE datos = serv.BuscarCliente(this.RUT_CLIENTE);
            if (datos != null) {
                this.CORREO_CLIENTE = datos.CORREO_CLIENTE;
                this.DIRECCION_CLIENTE = datos.DIRECCION_CLIENTE;
                this.DV_CLIENTE = datos.DV_CLIENTE;
                this.ID_COMUNA = datos.ID_COMUNA;
                this.ID_USUARIO = datos.ID_USUARIO;
                this.NOMBRE_CLIENTE = datos.NOMBRE_CLIENTE;
                this.RUT_CLIENTE = datos.RUT_CLIENTE;
                this.TELEFONO_CLIENTE = datos.TELEFONO_CLIENTE;
                this.ID_GIRO = datos.ID_GIRO;

                return true;
            }
            else {
                return false;
            }
        }
    }
}
