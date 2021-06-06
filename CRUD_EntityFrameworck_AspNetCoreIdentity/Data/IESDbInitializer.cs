using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_EntityFrameworck_AspNetCoreIdentity.Data
{
    public class IESDbInitializer
    {

        public static void Initialize(IEScontext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Departamentos.Any())
            {
                return;
            }
            var instituicoes = new Instituicao[]
            {
                new Instituicao
                {
                    Nome = "UniPará",
                    Endereco = "Pará"
                },
                new Instituicao
                {
                    Nome = "USP",
                    Endereco = "São Paulo"
                }
            };

            var departamentos = new Departamento[]
            {
                new Departamento
                {
                    Nome = "Ciência Da Computação ",
                    Instituicao = instituicoes[0]
                   
                },
                new Departamento
                {
                    Nome = "Sistema Da Informação",
                    Instituicao = instituicoes[1] 
                }
            };

            foreach(var instituicao in instituicoes)
            {
                context.Instituicoes.Add(instituicao);
            }

            foreach (var departamento in departamentos)
            {
                context.Departamentos.Add(departamento);
            }
            context.SaveChanges();
        }
       
    }
}
