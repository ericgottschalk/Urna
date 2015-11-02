using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.AcessoDados;
using UrnaApi.Dominio.ModuloEleitor;

namespace Urna.Dominio.Teste
{
    [TestClass]
    public class EleitorServicoDominioTest
    {
        [TestMethod]
        public void Inserir()
        {
            IEleitorRepositorio repositorio = new EleitorRepositorio();
            var servico = new EleitorServicoDominio(repositorio);

            var eleitor = new Eleitor()
            {
                Nome = "Inserir test",
                CPF = "2836432",
                RG = "126312362",
                TituloEleitor = "8642",
                DataNascimento = DateTime.Now,
                Secao = "2733",
                ZonaEleitoral = "N213",
                Situacao = 'A',
                Votou = 'N'
            };

            servico.Add(eleitor);
            Assert.IsTrue(servico.FindByName("Inserir t").Count > 0);
        }

        [TestMethod]
        public void Editar()
        {
            IEleitorRepositorio repositorio = new EleitorRepositorio();
            var servico = new EleitorServicoDominio(repositorio);

            var eleitor = new Eleitor()
            {
                Nome = "Editar test",
                CPF = "234534432",
                RG = "126687662",
                TituloEleitor = "13642",
                DataNascimento = DateTime.Now,
                Secao = "2733",
                ZonaEleitoral = "N216",
                Situacao = 'A',
                Votou = 'N'
            };

            servico.Add(eleitor);
            eleitor = servico.BusarPorCpf(eleitor.CPF);
            eleitor.Nome = "Editado";

            servico.Editar(eleitor);
            Assert.IsTrue(servico.FindByName("Editado").Count > 0);
        }

        [TestMethod]
        public void Deletar()
        {
            IEleitorRepositorio repositorio = new EleitorRepositorio();
            var servico = new EleitorServicoDominio(repositorio);

            var eleitor = new Eleitor()
            {
                Nome = "Deletar test",
                CPF = "2367432",
                RG = "12846772",
                TituloEleitor = "178642",
                DataNascimento = DateTime.Now,
                Secao = "2733",
                ZonaEleitoral = "N216",
                Situacao = 'A',
                Votou = 'N'
            };

            servico.Add(eleitor);
            eleitor = servico.BuscarPorRG(eleitor.RG);

            servico.Excluir(eleitor);
            Assert.IsTrue(servico.FindByName("Deletar t").Count == 0);
        }
    }
}
