using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    
    public class ServicioHuesped
    {
        HostalEntities ent = new HostalEntities();
        public HUESPED EncontrarHuesped(HUESPED h)
        {
            HUESPED huesped = ent.HUESPED.FirstOrDefault(objeto =>
            objeto.RUT_HUESPED.Equals(h.RUT_HUESPED));
            if (huesped == null)
            {
                return null;
            }
            else
            {
                return huesped;
            }
        }
        public bool RegistrarHuesped(HUESPED h)
        {
            ent.HUESPED.Add(h);
            ent.SaveChanges();
            return true;
        }
        public bool ExisteHuesped(HUESPED h)
        {
            HUESPED huesped = ent.HUESPED.FirstOrDefault(objeto =>
            objeto.RUT_HUESPED.Equals(h.RUT_HUESPED));
            if (huesped == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool EliminarHuesped(HUESPED h)
        {
            HUESPED huesped = ent.HUESPED.FirstOrDefault(objeto =>
            objeto.RUT_HUESPED.Equals(h.RUT_HUESPED));

            if (huesped == null)
            {
                return false;
            }
            else
            {
                ent.HUESPED.Remove(h);
                ent.SaveChanges();
                return true;
            }
        }
        public List<HUESPED> ListarHuesped()
        {
            var lista = (from consulta in ent.HUESPED
                         orderby consulta.RUT_HUESPED
                         select consulta).ToList();
            return lista;
        }

        public HUESPED BuscarHuesped(int RUT_HUESPED) {
            HUESPED huesped = ent.HUESPED.FirstOrDefault(x => x.RUT_HUESPED == RUT_HUESPED);
            return huesped;
        }

        public bool ModificarHuesped(HUESPED h)
        {
            HUESPED huesped = ent.HUESPED.FirstOrDefault(objeto => objeto.RUT_HUESPED == h.RUT_HUESPED);

            if (huesped == null)
            {
                return false;
            }
            else
            {
                //Se modifica cada atributo y se guardan los cambios en la base de datos
                huesped.RUT_HUESPED = h.RUT_HUESPED;
                huesped.DV_HUESPED = h.DV_HUESPED;
                huesped.PNOMBRE_HUESPED = h.PNOMBRE_HUESPED;
                huesped.SNOMBRE_HUESPED = h.SNOMBRE_HUESPED;
                huesped.APP_PATERNO_HUESPED = h.APP_PATERNO_HUESPED;
                huesped.APP_MATERNO_HUESPED = h.APP_MATERNO_HUESPED;
                huesped.TELEFONO_HUESPED = h.TELEFONO_HUESPED;
                huesped.REGISTRADO = h.REGISTRADO;
                huesped.NUMERO_HABITACION = h.NUMERO_HABITACION;
                huesped.RUT_CLIENTE = h.RUT_CLIENTE;

                ent.SaveChanges();
                return true;
            }
        }
    }
}
