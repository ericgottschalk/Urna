using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaApi.Dominio.ModuloEleicao
{
    public class Eleicao
    {
        public static bool Iniciou { get; private set; }

        private Eleicao() { }

        public static void IniciarEleicao()
        {
            Iniciou = true;
        }

        public static void TerminarEleicao()
        {
            Iniciou = false;

        }
    }
}
