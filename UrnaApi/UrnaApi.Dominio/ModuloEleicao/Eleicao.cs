using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.AcessoDados;
using UrnaApi.Dominio.ModuloCandidato;
using UrnaApi.Dominio.ModuloCargo;
using UrnaApi.Dominio.ModuloEleitor;
using UrnaApi.Dominio.ModuloPartido;
using UrnaApi.Dominio.ModuloVoto;

namespace UrnaApi.Dominio.ModuloEleicao
{
    public class Eleicao
    {
        public bool iniciouEleicao;
        private VotoServicoDominio voto;
        private PartidoServicoDominio partido;
        private CandidatoServicoDominio candidato;
        private CargoServicoDominio cargo;
        private EleitorServicoDominio eleitor;

        public Eleicao()
        {
            this.iniciouEleicao = false;

            IVotoRepositorio voto = new VotoRepositorio();
            this.voto = new VotoServicoDominio(voto);
            IPartidoRepositorio partido = new PartidoRepositorio();
            this.partido = new PartidoServicoDominio(partido);
            ICandidatoRepositorio candidato = new CandidatoRepositorio();
            this.candidato = new CandidatoServicoDominio(candidato);
            ICargoRepositorio cargo = new CargoRepositorio();
            this.cargo = new CargoServicoDominio(cargo);
            IEleitorRepositorio eleitor = new EleitorRepositorio();
            this.eleitor = new EleitorServicoDominio(eleitor);
        }

        public void IniciarEleicao()
        {
            this.iniciouEleicao = true;
        }

        public void EncerrarEleicao()
        {
            this.iniciouEleicao = false;
        }

        public void Votar(string cpf, int numero)
        {
            if (!this.iniciouEleicao)
            {
                throw new Exception("Não é possível votar, a eleição não iniciou");
            }

            this.voto.RegistrarVoto(cpf, numero);
        }
    }
}
