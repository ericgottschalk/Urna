using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.AcessoDados;
using UrnaApi.Dominio.ModuloPartido;

namespace Urna.Dominio.Teste
{ 
    [TestClass]
    public class PartidoServicoDominioTest
    {
        [TestMethod]
        public void Inserir()
        {
            IPartidoRepositorio repositorio = new PartidoRepositorio();
            var servico = new PartidoServicoDominio(repositorio);

            var partido = new Partido()
            {
                Nome = "Partido dos testes",
                Sigla = "PDT",
                Slogan = "Só está pronto após ser testado!"
            };

            servico.Add(partido);
            Assert.IsTrue(servico.FindByName("Partido dos te").Count > 0);
        }

        [TestMethod]
        public void Editar()
        {
            IPartidoRepositorio repositorio = new PartidoRepositorio();
            var servico = new PartidoServicoDominio(repositorio);

            var partido = new Partido()
            {
                Nome = "Editar partido",
                Sigla = "EP",
                Slogan = "Testando editar..."
            };

            servico.Add(partido);
            partido = servico.FindByName("Editar par")[0];
            partido.Nome = "Partido Editato";
            partido.Slogan = "Editar Testado com sucesso";
      
            servico.Editar(partido);
            Assert.IsTrue(servico.FindByName("Partido Edi").Count > 0);
        }

        [TestMethod]
        public void Deletar()
        {
            IPartidoRepositorio repositorio = new PartidoRepositorio();
            var servico = new PartidoServicoDominio(repositorio);

            var partido = new Partido()
            {
                Nome = "Deletar partido",
                Sigla = "DP",
                Slogan = "Testando deletar..."
            };

            servico.Add(partido);
            partido = servico.FindByName("Deletar par")[0];

            servico.Excluir(partido);
            Assert.IsTrue(servico.FindByName("Deletar part").Count == 0);
        }
    }
}
