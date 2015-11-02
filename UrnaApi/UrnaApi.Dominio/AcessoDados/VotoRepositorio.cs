﻿using DbExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UrnaApi.Dominio.ModuloVoto;

namespace UrnaApi.Dominio.AcessoDados
{
    public class VotoRepositorio : IVotoRepositorio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;

        public bool VerificaSeVotou(string cpf)
        {
            bool eleitorVotou = false;
            
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "SELECT Nome,CPF,Votou FROM Eleitor WHERE CPF = @paramCPF";

                comando.AddParameter("paramCPF", cpf);

                connection.Open();
                IDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    char votou = Convert.ToChar(reader["Votou"]);
                    if(votou == 'S')
                    {
                        eleitorVotou = true;
                    }
                }
            }
            return eleitorVotou;
        }

        public void RegistrarVoto(string cpf,int idCandidato)
        {
            if (!VerificaSeVotou(cpf))
            {
                throw new Exception("O eleitor com esse CPF já votou");
            }

            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comandoInsere = connection.CreateCommand();
                comandoInsere.CommandText = "INSERT INTO Voto (IDCandidato) VALUES (@paramIDCandidato)";
                comandoInsere.AddParameter("paramIDCandidato", idCandidato);

                IDbCommand comandoAtualiza = connection.CreateCommand();
                comandoAtualiza.CommandText = "UPDATE Eleitor SET Votou = 'S' WHERE CPF = @paramCPF";
                comandoAtualiza.AddParameter("paramCPF", cpf);

                connection.Open();

                comandoInsere.ExecuteNonQuery();
                comandoAtualiza.ExecuteNonQuery();

                transacao.Complete();
                connection.Close();
            }
        }
    }
}