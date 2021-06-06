using Modelo.Cadastros;
using Modelo.Discente;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_EntityFrameworck_AspNetCoreIdentity.Models.Infra;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRUD_EntityFrameworck_AspNetCoreIdentity.Data
{
    public class IEScontext: IdentityDbContext<UsuarioDaAplicacao>

    {
        public IEScontext(DbContextOptions<IEScontext> options) : base(options)
        {

        }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Academico> Academicos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Instituicao>().ToTable("Instituicoes");
            modelBuilder.Entity<Instituicao>().HasKey(i => i.InstituicaoId);
            modelBuilder.Entity<CursoDisciplina>().HasKey(cd => new { cd.CursoId, cd.DisciplinaId });
            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(cd => cd.Curso)
                .WithMany(c => c.CursoDisciplinas)
                .HasForeignKey(cd => cd.CursoId);
            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(cd => cd.Disciplina)
                .WithMany(d => d.CursoDisciplinas)
                .HasForeignKey(cd => cd.DisciplinaId);
        }
    }
}
