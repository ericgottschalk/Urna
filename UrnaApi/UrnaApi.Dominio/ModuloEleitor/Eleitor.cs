using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.Comum;

namespace UrnaApi.Dominio.ModuloEleitor
{
    public class Eleitor : EntidadeBase
    {
        public string Nome { get; set; }

        public string TituloEleitor { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string ZonaEleitoral { get; set; }

        public string Secao { get; set; }

        public char Situacao { get; set; }

        public char Votou { get; set; }
    }
}
