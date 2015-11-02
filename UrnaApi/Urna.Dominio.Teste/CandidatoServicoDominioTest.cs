using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrnaApi.Dominio.ModuloCandidato;
using UrnaApi.Dominio.AcessoDados;

namespace Urna.Dominio.Teste
{
    [TestClass]
    public class CandidatoServicoDominioTest
    {
        [TestMethod]
        public void Cadastrar()
        {
            ICandidatoRepositorio repositorio = new CandidatoRepositorio();
            var servico = new CandidatoServicoDominio(repositorio);

            var candidato = new Candidato()
            {
                NomeCompleto = "Candidato Test",
                NomePopular = "Test",
                DataNascimento = DateTime.Now,
                IdPartido = 1,
                IdCargo = 2,
                Numero = 1234,
                Exibe = true,
                Foto = "foto.jpg",
                RegistroTRE = "298363"
            };

            servico.Add(candidato);

            Assert.IsTrue(servico.FindByName("Candidato Tes").Count > 0);
        }
    }
}
