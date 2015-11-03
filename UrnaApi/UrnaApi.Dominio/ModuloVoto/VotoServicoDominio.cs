using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaApi.Dominio.ModuloVoto
{
    public class VotoServicoDominio
    {
        private IVotoRepositorio repositorio;

        public VotoServicoDominio(IVotoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void RegistrarVoto(string cpf,int numero)
        {
            this.repositorio.RegistrarVoto(cpf, numero);
        }

        public Dictionary<string, int> RelacaoCandidatoVoto()
        {
            return this.repositorio.RelacaoCandidatoVoto();
        }

        public int QuantosEleitoresVotaram()
        {
            return this.repositorio.QuantosEleitoresVotaram();
        }

        public int QuantosEleitoresNaoVotaram()
        {
            return this.repositorio.QuantosEleitoresNaoVotaram();
        }
    }
}
