using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.ModuloPartido;
using DbExtensions;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace UrnaApi.Dominio.AcessoDados
{
    public class PartidoRepositorio : IPartidoRepositorio
    {
        public void Excluir(Partido partido)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;
            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Partido WHERE IDPartido = @paramIDPartido";
                cmd.AddParameter("paramIDPartido", partido.Id);

                connection.Open();

                cmd.ExecuteNonQuery();

                transacao.Complete();
                connection.Close();
            }
        }

        public void Cadastrar(Partido item)
        {
            List<Partido> partidoEncontrado = FindByName(item.Nome);
            if (partidoEncontrado.TrueForAll(partido=>partido.Nome == item.Nome) && partidoEncontrado.TrueForAll(partido=>partido.Slogan == item.Slogan))
            {
                throw new Exception("Nome e slogan ja existentes.");
            }
            string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;
            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "INSERT INTO Partido (Nome,Slogan,Sigla) VALUES (@paramNome,@paramSlogan,@paramSigla)";
                comando.AddParameter("paramNome", item.Nome);
                comando.AddParameter("paramSlogan", item.Slogan);
                comando.AddParameter("paramSigla", item.Sigla);
                connection.Open();

                comando.ExecuteNonQuery();

                transacao.Complete();
                connection.Close();
            }
        }

        public void Editar(Partido item)
        {
            throw new NotImplementedException();
        }

        public Partido FindById(int id)
        {
            Partido partidoEncontrado = null;

            string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "SELECT IDPartido,Nome,Slogan,Sigla FROM Cargo WHERE IDPartido = @paramIDPartido";

                comando.AddParameter("paramIDPartido", id);

                connection.Open();
                IDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    int idDb = Convert.ToInt32(reader["IDPartido"]);
                    string nome = reader["Nome"].ToString();
                    string slogan = reader["Slogan"].ToString();
                    string sigla = reader["Sigla"].ToString();

                    partidoEncontrado = new Partido()
                    {
                        Id = idDb,
                        Nome = nome,
                        Slogan = slogan,
                        Sigla = sigla
                    };
                }
                connection.Close();
            }

            return partidoEncontrado;
        }

        public List<Partido> FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
