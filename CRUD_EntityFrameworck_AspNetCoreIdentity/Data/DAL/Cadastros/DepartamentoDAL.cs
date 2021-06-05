using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace capitulo01.Data.DAL.Cadastros
{
    public class DepartamentoDAL
    {
        public IEScontext IEScontext { get; }
        public DepartamentoDAL(IEScontext iEScontext)
        {
            IEScontext = iEScontext;
        }
        public IQueryable<Departamento> ObterDepartamentosClassificadosPorNome()
        {
            return IEScontext.Departamentos
                .Include(d => d.Instituicao)
                .OrderBy(d => d.Nome);
        }
        public async Task<Departamento> ObterDepartamentoPorId(long id)
        {
            var departemento = await IEScontext.Departamentos
                .SingleOrDefaultAsync(d => d.DepartamentoId == id);
            IEScontext.Instituicoes.Where(i => departemento.InstituicaoId == i.InstituicaoId).Load();
            return departemento;
        }
        public async Task<Departamento> RegistrarOuAlterarDepartamento(Departamento departamento)
        {
            if (departamento.DepartamentoId == null)
            {
                IEScontext.Departamentos.Add(departamento);
            }
            else
            {
                IEScontext.Departamentos.Update(departamento);
            }
            await IEScontext.SaveChangesAsync();
            return departamento;
        }
        public async Task<Departamento> RemoverDepartamento(long id)
        {
            var departamento = await ObterDepartamentoPorId(id);
            IEScontext.Departamentos.Remove(departamento);
            await IEScontext.SaveChangesAsync();
            return departamento;
        }

    }
}
