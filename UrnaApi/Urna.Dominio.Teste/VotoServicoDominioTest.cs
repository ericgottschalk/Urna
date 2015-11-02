using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.AcessoDados;
using UrnaApi.Dominio.ModuloEleitor;
using UrnaApi.Dominio.ModuloVoto;

namespace Urna.Dominio.Teste
{
    [TestClass]
    public class VotoServicoDominioTest
    {
        [TestMethod]
        public void Votar()
        {

        }

        [TestMethod]
        public void QuantosVotaram()
        {
            IVotoRepositorio votoRepositorio = new VotoRepositorio();
            var votoServico = new VotoServicoDominio(votoRepositorio);
            IEleitorRepositorio eleitorRepositorio = new EleitorRepositorio();
            var eleitorServico = new EleitorServicoDominio(eleitorRepositorio);

            var lista = eleitorRepositorio.FindByName("");
            int votaram = lista.Count(t => t.Votou == 'S');

            Assert.IsTrue(votaram == votoServico.QuantosEleitoresVotaram());
        }

        [TestMethod]
        public void QuantosNaoVotaram()
        {
            IVotoRepositorio votoRepositorio = new VotoRepositorio();
            var votoServico = new VotoServicoDominio(votoRepositorio);
            IEleitorRepositorio eleitorRepositorio = new EleitorRepositorio();
            var eleitorServico = new EleitorServicoDominio(eleitorRepositorio);

            var lista = eleitorRepositorio.FindByName("");
            int naoVotaram = lista.Count(t => t.Votou == 'N');

            Assert.IsTrue(naoVotaram == votoServico.QuantosEleitoresVotaram());
        }
    }
}
