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

        [TestMethod]
        public void Editar()
        {
            ICandidatoRepositorio repositorio = new CandidatoRepositorio();
            var servico = new CandidatoServicoDominio(repositorio);

            var candidato = new Candidato()
            {
                NomeCompleto = "Editar Test",
                NomePopular = "Editar",
                DataNascimento = DateTime.Now,
                IdPartido = 1,
                IdCargo = 2,
                Numero = 12234,
                Exibe = true,
                Foto = "foto.jpg",
                RegistroTRE = "2344563"
            };

            servico.Add(candidato);
            var tempCandidato = servico.FindByName("Editar T")[0];

            tempCandidato.NomeCompleto = "Editado com sucesso";
            servico.Editar(tempCandidato);

            Assert.IsTrue(servico.FindByName("Editado").Count > 0);
        }

        [TestMethod]
        public void Deletar()
        {
            ICandidatoRepositorio repositorio = new CandidatoRepositorio();
            var servico = new CandidatoServicoDominio(repositorio);

            var candidato = new Candidato()
            {
                NomeCompleto = "Delete Test",
                NomePopular = "Delete",
                DataNascimento = DateTime.Now,
                IdPartido = 1,
                IdCargo = 2,
                Numero = 144334,
                Exibe = true,
                Foto = "foto.jpg",
                RegistroTRE = "224344563"
            };

            servico.Add(candidato);
            Assert.IsTrue(servico.FindByName("Delete").Count > 0);

            var tempCandidato = servico.FindByName("Delete T")[0];
            servico.Excluir(tempCandidato);

            Assert.IsTrue(servico.FindByName("Delete").Count == 0);
        }
    }
}
