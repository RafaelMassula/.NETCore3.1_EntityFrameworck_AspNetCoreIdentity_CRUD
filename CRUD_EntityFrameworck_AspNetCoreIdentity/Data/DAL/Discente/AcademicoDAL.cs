using Microsoft.EntityFrameworkCore;
using Modelo.Discente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_EntityFrameworck_AspNetCoreIdentity.Data.DAL.Discente
{
    public class AcademicoDAL
    {
        public IEScontext IEScontext { get; }
        public AcademicoDAL(IEScontext iEScontext)
        {
            IEScontext = iEScontext; 
        }
        public IQueryable<Academico> ObeterAcademicosClassificadosPorNome()
        {
            return IEScontext.Academicos.OrderBy(a => a.Nome);
        }
        public async Task<Academico> ObterAcademicosPorId(long id)
        {
            return await IEScontext.Academicos.FindAsync(id);
        }
        public async Task<Academico> RegistrarOuAlterarAcademico(Academico academico)
        {
            if (academico.AcademicoId == null)
            {
                IEScontext.Academicos.Add(academico);
            }
            else
            {
                IEScontext.Academicos.Update(academico);
            }
            await IEScontext.SaveChangesAsync();
            return academico;
        }
        public async Task<Academico> RemoverAcademico(long id)
        {
            Academico academico = await ObterAcademicosPorId(id);
            IEScontext.Academicos.Remove(academico);
            await IEScontext.SaveChangesAsync();
            return academico;
        }
    }
}
