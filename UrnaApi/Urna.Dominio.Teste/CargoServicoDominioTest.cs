﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrnaApi.Dominio.ModuloCargo;
using UrnaApi.Dominio.AcessoDados;

namespace Urna.Dominio.Teste
{
    [TestClass]
    public class CargoServicoDominioTest
    {
        [TestMethod]
        public void Cadastrar()
        {
            ICargoRepositorio repositorio = new CargoRepositorio();
            var servico = new CargoServicoDominio(repositorio);

            Cargo cargo = new Cargo()
            {
                Nome = "Cargo Test",
                Situacao = 'A'              
            };

            servico.Add(cargo);

            Assert.IsTrue(servico.FindByName("Cargo Tes").Count > 0);
        }

        [TestMethod]
        public void Editar()
        {
            ICargoRepositorio repositorio = new CargoRepositorio();
            var servico = new CargoServicoDominio(repositorio);

            Cargo cargo = new Cargo()
            {
                Nome = "Editar Test",
                Situacao = 'A'
            };

            servico.Add(cargo);
            cargo = servico.FindByName("Editar T")[0];

            cargo.Nome = "Editado com sucesso";
            servico.Editar(cargo);

            Assert.IsTrue(servico.FindByName("Editado").Count > 0);
        }

        [TestMethod]
        public void MudarSituacao()
        {
            ICargoRepositorio repositorio = new CargoRepositorio();
            var servico = new CargoServicoDominio(repositorio);

            Cargo cargo = new Cargo()
            {
                Nome = "Situacao Test",
                Situacao = 'A'
            };

            servico.Add(cargo);
            cargo = servico.FindByName("Situacao T")[0];

            servico.MudarSituacao(cargo);

            Assert.IsTrue(servico.FindByName("Situacao")[0].Situacao == 'I');
        }
    }
}
