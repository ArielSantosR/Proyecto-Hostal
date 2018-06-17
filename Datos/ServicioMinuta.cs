using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioMinuta
    {
        HostalEntities ent = new HostalEntities();

        public bool AgregarMinuta(PENSION minuta)
        {
            ent.PENSION.Add(minuta);
            ent.SaveChanges();
            return true;
        }

        public List<PENSION> ListarMinuta()
        {
            var lista = (from consulta in ent.PENSION
                         orderby consulta.ID_PENSION
                         select consulta).ToList();
            return lista;
        }

        public bool AgregarDetallePlatos(DETALLE_PLATOS d)
        {
            ent.DETALLE_PLATOS.Add(d);
            ent.SaveChanges();
            return true;
        }

        public bool EliminarDetallePlatos(DETALLE_PLATOS detalle)
        {
            DETALLE_PLATOS d = ent.DETALLE_PLATOS.FirstOrDefault(objeto =>
                objeto.ID_DETALLE_PLATOS.Equals(detalle.ID_DETALLE_PLATOS));

            if (d != null)
            {
                ent.DETALLE_PLATOS.Remove(d);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AgregarDetalleMinuta(DETALLE_PLATOS detalle)
        {
            ent.DETALLE_PLATOS.Add(detalle);
            ent.SaveChanges();
            return true;
        }

        public PENSION ObtenerMinuta(PENSION minuta)
        {
            PENSION p = ent.PENSION.FirstOrDefault(objeto =>
            objeto.ID_PENSION.Equals(minuta.ID_PENSION));

            if (p != null)
            {
                return p;
            }
            else
            {
                return null;
            }
        }

        public DETALLE_PLATOS obtenerDetallePlatos(DETALLE_PLATOS detalle)
        {
            DETALLE_PLATOS d = ent.DETALLE_PLATOS.FirstOrDefault(objeto =>
            objeto.ID_DETALLE_PLATOS == detalle.ID_DETALLE_PLATOS);

            if (d != null)
            {
                return d;
            }
            else
            {
                return null;
            }
        }

        /*
         *  DETALLE_PLATOS d = ent.DETALLE_PLATOS.FirstOrDefault(objeto =>
            objeto.ID_PENSION.Equals(minuta.ID_PENSION));
         */

        public bool EliminarDetalleMinuta(PENSION minuta)
        {
            DETALLE_PLATOS d = ent.DETALLE_PLATOS.FirstOrDefault(objeto =>
            objeto.ID_PENSION.Equals(minuta.ID_PENSION));



            if (d != null)
            {
                
                    ent.DETALLE_PLATOS.Remove(d);

                    ent.SaveChanges();
                    
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarMinuta(PENSION minuta)
        {
            PENSION p = ent.PENSION.FirstOrDefault(objeto =>
            objeto.ID_PENSION.Equals(minuta.ID_PENSION));

           

            if (p != null)
            {
                ent.PENSION.Remove(p);
                
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditarMinuta(PENSION minuta)
        {
            PENSION p = ent.PENSION.FirstOrDefault(objeto =>
            objeto.ID_PENSION.Equals(minuta.ID_PENSION));

            if (p != null)
            {

                p.NOMBRE_PENSION = minuta.NOMBRE_PENSION;
                p.VALOR_PENSION = minuta.VALOR_PENSION;
               
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExisteMinuta(PENSION minuta)
        {
            PENSION p = ent.PENSION.FirstOrDefault(objeto =>
            objeto.ID_PENSION.Equals(minuta.ID_PENSION));
            if (p != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<DETALLE_PLATOS> ListaDetalleMinuta(PENSION platos)
        {
            var lista = (from consulta in ent.DETALLE_PLATOS
                         where consulta.ID_PENSION == platos.ID_PENSION
                         orderby consulta.ID_PLATO
                         select consulta).ToList();
            return lista;
        }
    }
}
