using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoAspNetCore.Data.ORM;

namespace ProjetoAspNetCore.Mvc.Extensions.ViewComponents.Helpers
{
    public static class Util
    {
        public static int TotReg(CursoDbContext context)
        {
            return (from pac in context.Paciente.AsNoTracking() select pac).Count();
        }

        public static decimal GetNumRegEstado(CursoDbContext context, string estado)
        {
            return context.Paciente.AsNoTracking().Count(x => x.EstadoPaciente.Descricao.Contains(estado));
        }
    }
}
