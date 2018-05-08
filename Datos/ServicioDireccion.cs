using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioDireccion
    {
        HostalEntities ent = new HostalEntities();

        public List<PAIS> ListarPaises()
        {
            return ent.PAIS.ToList<PAIS>();
        }

        public List<REGION> ListarRegion()
        {
            return ent.REGION.ToList<REGION>();
        }

        public List<COMUNA> ListarComuna()
        {
            return ent.COMUNA.ToList<COMUNA>();
        }

        //PAIS

        public bool AgregarPais(PAIS p)
        {
            ent.PAIS.Add(p);
            ent.SaveChanges();
            return true;
        }
        public bool ExistePais(PAIS pais)
        {
            PAIS p = ent.PAIS.FirstOrDefault(objeto =>
            objeto.NOMBRE_PAIS.Equals(pais.NOMBRE_PAIS));
            if (p != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExistePaisID(PAIS pais)
        {
            PAIS p = ent.PAIS.FirstOrDefault(objeto =>
            objeto.ID_PAIS.Equals(pais.ID_PAIS));
            if (p != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public PAIS obtenerPais(PAIS pais)
        {

            PAIS p = ent.PAIS.FirstOrDefault(objeto =>
            objeto.ID_PAIS.Equals(pais.ID_PAIS));

            if (p != null)
            {
                return p;
            }
            else
            {
                return null;
            }
        }
        public bool EditarPais(PAIS pais)
        {
            PAIS p = ent.PAIS.FirstOrDefault(objeto =>
            objeto.ID_PAIS.Equals(pais.ID_PAIS));

            if (p != null)
            {
                p.ID_PAIS = pais.ID_PAIS;
                p.NOMBRE_PAIS = pais.NOMBRE_PAIS;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarPais(PAIS pais)
        {
            PAIS p = ent.PAIS.FirstOrDefault(objeto =>
            objeto.ID_PAIS.Equals(pais.ID_PAIS));

            if (p != null)
            {
                ent.PAIS.Remove(p);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //REGION

        public bool AgregarRegion(REGION r)
        {
            ent.REGION.Add(r);
            ent.SaveChanges();
            return true;
        }
        public bool ExisteRegion(REGION region)
        {
            REGION r = ent.REGION.FirstOrDefault(objeto =>
            objeto.NOMBRE_REGION.Equals(region.NOMBRE_REGION));
            if (r != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExisteRegionID(REGION region)
        {
            REGION r = ent.REGION.FirstOrDefault(objeto =>
            objeto.ID_REGION.Equals(region.ID_REGION));
            if (r != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public REGION obtenerRegion(REGION region)
        {

            REGION r = ent.REGION.FirstOrDefault(objeto =>
            objeto.ID_REGION.Equals(region.ID_REGION));

            if (r != null)
            {
                return r;
            }
            else
            {
                return null;
            }
        }
        public bool EditarRegion(REGION region)
        {
            REGION r = ent.REGION.FirstOrDefault(objeto =>
            objeto.ID_REGION.Equals(region.ID_REGION));

            if (r != null)
            {
                r.ID_REGION = region.ID_REGION;
                r.NOMBRE_REGION = region.NOMBRE_REGION;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarRegion(REGION region)
        {
            REGION r = ent.REGION.FirstOrDefault(objeto =>
            objeto.ID_REGION.Equals(region.ID_REGION));

            if (r != null)
            {
                ent.REGION.Remove(r);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //COMUNA

        public bool AgregarComuna(COMUNA c)
        {
            ent.COMUNA.Add(c);
            ent.SaveChanges();
            return true;
        }
        public bool ExisteComuna(COMUNA comuna)
        {
            COMUNA c = ent.COMUNA.FirstOrDefault(objeto =>
            objeto.NOMBRE_COMUNA.Equals(comuna.NOMBRE_COMUNA));
            if (c != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExisteComunaID(COMUNA comuna)
        {
            COMUNA c = ent.COMUNA.FirstOrDefault(objeto =>
            objeto.ID_COMUNA.Equals(comuna.ID_COMUNA));
            if (c != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public COMUNA obtenerComuna(COMUNA comuna)
        {

            COMUNA c = ent.COMUNA.FirstOrDefault(objeto =>
            objeto.ID_COMUNA.Equals(comuna.ID_COMUNA));

            if (c != null)
            {
                return c;
            }
            else
            {
                return null;
            }
        }
        public bool EditarComuna(COMUNA comuna)
        {
            COMUNA c = ent.COMUNA.FirstOrDefault(objeto =>
            objeto.ID_COMUNA.Equals(comuna.ID_COMUNA));

            if (c != null)
            {
                c.ID_COMUNA = comuna.ID_COMUNA;
                c.NOMBRE_COMUNA = comuna.NOMBRE_COMUNA;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarComuna(COMUNA comuna)
        {
            COMUNA c = ent.COMUNA.FirstOrDefault(objeto =>
            objeto.ID_COMUNA.Equals(comuna.ID_COMUNA));

            if (c != null)
            {
                ent.COMUNA.Remove(c);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
