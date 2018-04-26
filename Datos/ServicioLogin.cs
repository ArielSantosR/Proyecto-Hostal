using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Datos
{
    public class ServicioLogin
    {
        HostalEntities ent = new HostalEntities();

        public bool Login(USUARIO user)
        {
            USUARIO usuario = ent.USUARIO.FirstOrDefault(objeto => objeto.NOMBRE_USUARIO.Equals(user.NOMBRE_USUARIO));
            if (usuario != null)
            {
                if (BCrypt.CheckPassword(user.PASSWORD, usuario.PASSWORD))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
