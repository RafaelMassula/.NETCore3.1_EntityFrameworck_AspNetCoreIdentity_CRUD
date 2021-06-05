using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Cadastros
{
   public class CursoDisciplina
    {
        public long CursoId { get; set; }
        public Curso Curso { get; set; }
        public long DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
