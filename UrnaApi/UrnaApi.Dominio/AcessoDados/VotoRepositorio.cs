using DbExtensions;
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
using UrnaApi.Dominio.ModuloCandidato;

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

        public void RegistrarVoto(string cpf, int numero)
        {
            if (!VerificaSeVotou(cpf))
            {
                throw new Exception("O eleitor com esse CPF já votou");
            }

            ICandidatoRepositorio repositorio = new CandidatoRepositorio();
            CandidatoServicoDominio candidatoDominio = new CandidatoServicoDominio(repositorio);

            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                Candidato candidato = candidatoDominio.BuscarPorNumero(numero);
                IDbCommand comandoInsere = connection.CreateCommand();
                comandoInsere.CommandText = "INSERT INTO Voto (IDCandidato) VALUES (@paramIDCandidato)";
                comandoInsere.AddParameter("paramIDCandidato", candidato.Id);

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

        public Dictionary<string, int> RelaçãoCandidatoVoto()
        {
            Dictionary<string, int> relacaoCandidatoVoto = new Dictionary<string, int>();

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "SELECT Candidato.NomePopular,COUNT(DISTINCT Voto.IDCandidato) as Votos FROM Candidato LEFT JOIN Voto ON Candidato.IDCandidato = Voto.IdCandidato GROUP BY Candidato.NomePopular;";

                connection.Open();
                IDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    string nomePopular = reader["NomePopular"].ToString();
                    int votos = Convert.ToInt32(reader["Votos"]);
                    relacaoCandidatoVoto.Add(nomePopular, votos);
                }
            }
            return relacaoCandidatoVoto;
        }

        public int QuantosEleitoresVotaram()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                int quant = 0;

                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "SELECT COUNT(1) as Votaram FROM Eleitor WHERE Votou = 'S'";

                connection.Open();
                IDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    quant = Convert.ToInt32(reader["Votaram"]);
                }

                return quant;
            }
        }

        public int QuantosEleitoresNaoVotaram()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                int quant = 0;

                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "SELECT COUNT(1) as NaoVotaram FROM Eleitor WHERE Votou = 'N'";

                connection.Open();
                IDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    quant = Convert.ToInt32(reader["NaoVotaram"]);
                }

                return quant;
            }
        }
    }
}
