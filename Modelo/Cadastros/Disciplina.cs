using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Cadastros
{
    public class Disciplina
    {
        public long DisciplinaId { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<CursoDisciplina> CursoDisciplinas { get; set; }
    }
}
