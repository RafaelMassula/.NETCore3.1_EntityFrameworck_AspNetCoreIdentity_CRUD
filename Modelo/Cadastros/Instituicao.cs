using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Cadastros
{
    public class Instituicao
    {
        public long? InstituicaoId { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public virtual ICollection<Departamento> Departamentos { get; set; }
    }
}
