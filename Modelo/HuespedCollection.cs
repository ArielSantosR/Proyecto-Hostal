using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class HuespedCollection : List<Huesped>
    {
        public static List<Huesped> ListaHuesped() {
            ServicioHuesped serv = new ServicioHuesped();
            return GenerarLista(serv.ListarHuesped());
        }

        private static List<Huesped> GenerarLista(List<HUESPED> listDatos) {
            List<Huesped> list = new List<Huesped>();
            Huesped huesped;
            foreach (HUESPED dato in listDatos) {
                huesped = new Huesped();
                huesped.APP_MATERNO_HUESPED = dato.APP_MATERNO_HUESPED;
                huesped.APP_PATERNO_HUESPED = dato.APP_PATERNO_HUESPED;
                huesped.DV_HUESPED = dato.DV_HUESPED;
                huesped.NUMERO_HABITACION = dato.NUMERO_HABITACION;
                huesped.PNOMBRE_HUESPED = dato.PNOMBRE_HUESPED;
                huesped.RUT_CLIENTE = dato.RUT_CLIENTE;
                huesped.RUT_HUESPED = dato.RUT_HUESPED;
                huesped.SNOMBRE_HUESPED = dato.SNOMBRE_HUESPED;
                huesped.TELEFONO_HUESPED = huesped.TELEFONO_HUESPED;
                huesped.REGISTRADO = huesped.REGISTRADO;

                list.Add(huesped);
            }
            return list;
        }
    }
}
