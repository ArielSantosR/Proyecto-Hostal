using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Huesped
    {
        public int RUT_HUESPED { get; set; }
        public string DV_HUESPED { get; set; }
        public string PNOMBRE_HUESPED { get; set; }
        public string SNOMBRE_HUESPED { get; set; }
        public string APP_PATERNO_HUESPED { get; set; }
        public string APP_MATERNO_HUESPED { get; set; }
        public Nullable<long> TELEFONO_HUESPED { get; set; }
        public string REGISTRADO { get; set; }
        public Nullable<short> NUMERO_HABITACION { get; set; }
        public int RUT_CLIENTE { get; set; }

        public string RutYNombre
        {
            get { return this.RUT_HUESPED + "-" + this.DV_HUESPED + " " + this.PNOMBRE_HUESPED + " " + this.APP_PATERNO_HUESPED; }
        }

        public bool Crear() {
            ServicioHuesped serv = new ServicioHuesped();
            HUESPED datos = new HUESPED();
            datos.APP_MATERNO_HUESPED = this.APP_MATERNO_HUESPED;
            datos.APP_PATERNO_HUESPED = this.APP_PATERNO_HUESPED;
            datos.DV_HUESPED = this.DV_HUESPED;
            datos.PNOMBRE_HUESPED = this.PNOMBRE_HUESPED;
            datos.RUT_CLIENTE = this.RUT_CLIENTE;
            datos.SNOMBRE_HUESPED = this.SNOMBRE_HUESPED;
            datos.RUT_HUESPED = this.RUT_HUESPED;
            datos.NUMERO_HABITACION = this.NUMERO_HABITACION;
            datos.TELEFONO_HUESPED = this.TELEFONO_HUESPED;
            datos.REGISTRADO = this.REGISTRADO;

            return serv.RegistrarHuesped(datos);
        }
        public bool Update() {
            ServicioHuesped serv = new ServicioHuesped();
            HUESPED datos = new HUESPED();
            datos.APP_MATERNO_HUESPED = this.APP_MATERNO_HUESPED;
            datos.APP_PATERNO_HUESPED = this.APP_PATERNO_HUESPED;
            datos.DV_HUESPED = this.DV_HUESPED;
            datos.PNOMBRE_HUESPED = this.PNOMBRE_HUESPED;
            datos.RUT_CLIENTE = this.RUT_CLIENTE;
            datos.SNOMBRE_HUESPED = this.SNOMBRE_HUESPED;
            datos.RUT_HUESPED = this.RUT_HUESPED;
            datos.NUMERO_HABITACION = this.NUMERO_HABITACION;
            datos.TELEFONO_HUESPED = this.TELEFONO_HUESPED;
            datos.REGISTRADO = this.REGISTRADO;

            return serv.ModificarHuesped(datos);
        }

        public bool BuscarHuesped() {
            ServicioHuesped serv = new ServicioHuesped();
            HUESPED datos = serv.BuscarHuesped(this.RUT_HUESPED);
            if (datos != null) {
                this.APP_MATERNO_HUESPED = datos.APP_MATERNO_HUESPED;
                this.APP_PATERNO_HUESPED = datos.APP_PATERNO_HUESPED;
                this.DV_HUESPED = datos.DV_HUESPED;
                this.PNOMBRE_HUESPED = datos.PNOMBRE_HUESPED;
                this.REGISTRADO = datos.REGISTRADO;
                this.RUT_CLIENTE = datos.RUT_CLIENTE;
                this.RUT_HUESPED = datos.RUT_HUESPED;
                this.SNOMBRE_HUESPED = datos.SNOMBRE_HUESPED;
                this.TELEFONO_HUESPED = datos.TELEFONO_HUESPED;
                this.NUMERO_HABITACION = datos.NUMERO_HABITACION;

                return true;
            }
            else {
                return false;

            }
        }

        public bool ExisteRut() {
            ServicioHuesped serv = new ServicioHuesped();
            HUESPED h = new HUESPED();
            h.RUT_HUESPED = this.RUT_HUESPED;
            return serv.ExisteHuesped(h);
             
        }

        public bool Delete() {
            ServicioHuesped serv = new ServicioHuesped();
            HUESPED h = new HUESPED();
            h.RUT_HUESPED = this.RUT_HUESPED;
            return serv.EliminarHuesped(h);
        }
    }
}
