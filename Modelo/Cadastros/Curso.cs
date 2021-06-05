using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Cadastros
{
   public class Curso
    {
        public long CursoId { get; set; }
        public string Nome { get; set; }
        public long DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public virtual ICollection<CursoDisciplina> CursoDisciplinas { get; set; }
    }
}
