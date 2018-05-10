﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioNotificacion
    {
        HostalEntities ent = new HostalEntities();

        public List<NOTIFICACION> ListaNotificacion(USUARIO usuario)
        {
            var lista = (from consulta in ent.NOTIFICACION
                         where consulta.ID_USUARIO == usuario.ID_USUARIO
                         && consulta.ESTADO_NOTIFICACION.Equals("Habilitado")
                         select consulta).ToList();
            return lista;
        }
    }
}