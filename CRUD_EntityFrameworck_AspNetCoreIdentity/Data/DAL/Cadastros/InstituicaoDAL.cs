using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_EntityFrameworck_AspNetCoreIdentity.Data.DAL.Cadastros
{
    public class InstituicaoDAL
    {
        public IEScontext IEScontext { get; }
        public InstituicaoDAL(IEScontext iEScontext)
        {
            IEScontext = iEScontext;
        }

        public IQueryable<Instituicao> ObterInstituicoesClasificadasPorNome()
        {
            return IEScontext.Instituicoes.OrderBy(i => i.Nome);
        }
        public async Task<Instituicao> ObterInstituicaoPorId(long? id)
        {
            return await IEScontext.Instituicoes
                 .Include(i => i.Departamentos)
                 .SingleOrDefaultAsync(i => i.InstituicaoId == id);
        }
        public async Task<Instituicao> RegistrarOuAlterarInstituicao(Instituicao instituicao)
        {
            if (instituicao.InstituicaoId == null)
            {
                IEScontext.Instituicoes.Add(instituicao);
            }
            else
            {
                IEScontext.Update(instituicao);
            }
            await IEScontext.SaveChangesAsync();
            return instituicao;
        }
        public async Task<Instituicao> RemoverInstituicao(long id)
        {
            Instituicao instituicao = await ObterInstituicaoPorId(id);
            IEScontext.Instituicoes.Remove(instituicao);
            await IEScontext.SaveChangesAsync();
            return instituicao;
        }
    }
}
