using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.Comum;

namespace UrnaApi.Dominio.ModuloPartido
{
    public class Partido : EntidadeBase
    {
        public string Nome { get; set; }

        public string Slogan { get; set; }

        public string Sigla { get; set; }
    }
}
