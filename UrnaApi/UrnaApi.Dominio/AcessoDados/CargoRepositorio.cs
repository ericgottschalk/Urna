﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.ModuloCargo;
using DbExtensions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;

namespace UrnaApi.Dominio.AcessoDados
{
    public class CargoRepositorio : ICargoRepositorio
    {
        public void Cadastrar(Cargo item)
        {
            throw new NotImplementedException();
        }

        public void Editar(Cargo item)
        {
            throw new NotImplementedException();
        }

        public Cargo FindById(int id)
        {
            Cargo cargoEncontrado = null;

            string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "SELECT IDCargo,Nome,Situacao FROM Cargo WHERE IDCargo = @paramIDCargo";

                comando.AddParameter("paramIDCargo", id);

                connection.Open();
                IDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    int idDb = Convert.ToInt32(reader["IDCargo"]);
                    string nome = reader["Nome"].ToString();
                    char situacao = Convert.ToChar(reader["Situacao"]);

                    cargoEncontrado = new Cargo()
                    {
                        Id = idDb,
                        Nome = nome,
                        Situacao = situacao
                    };
                }
                connection.Close();
            }

            return cargoEncontrado;
        }

        public List<Cargo> FindByName(string name)
        {
            List<Cargo> cargosEncontrados = new List<Cargo>();

            string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText = "SELECT IDCargo,Nome,Situacao FROM Cargo WHERE Nome = @paramNome";
                comando.AddParameter("paramNome", name);
                connection.Open();
                IDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    int idDb = Convert.ToInt32(reader["IDCargo"]);
                    string nome = reader["Nome"].ToString();
                    char situacao = Convert.ToChar(reader["Situacao"]);
                    Cargo cargoEncontrado = new Cargo()
                    {
                        Id =idDb,
                        Nome =nome,
                        Situacao = situacao
                    };
                    cargosEncontrados.Add(cargoEncontrado);
                }
                connection.Close();
            }
            return cargosEncontrados;
        }
    }
}
