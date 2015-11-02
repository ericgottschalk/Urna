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

        public void RegistrarVoto(string cpf,int idCandidato)
        {
            this.repositorio.RegistrarVoto(cpf, idCandidato);
        }
    }
}
