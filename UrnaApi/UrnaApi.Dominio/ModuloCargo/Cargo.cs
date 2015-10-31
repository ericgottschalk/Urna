using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.Comum;

namespace UrnaApi.Dominio.ModuloCargo
{
    public class Cargo : EntidadeBase
    {
        public string Nome { get; set; }

        public char Situacao { get; set; }
    }
}
