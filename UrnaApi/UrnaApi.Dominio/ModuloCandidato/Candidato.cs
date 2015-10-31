using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.Comum;

namespace UrnaApi.Dominio.ModuloCandidato
{
    public class Candidato: EntidadeBase
    {
        public string NomeCompleto { get; set; }

        public string NomePopular { get; set; }

        public DateTime DataNascimento { get; set; }

        public string RegistroTRE { get; set; }

        public int IdPartido { get; set; }

        public int Numero { get; set; }

        public int IdCargo { get; set; }

        public bool Exibe { get; set; }
    }
}
