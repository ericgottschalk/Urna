using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.ModuloPartido;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using UrnaApi.Dominio.ModuloEleicao;

namespace UrnaApi.Dominio.AcessoDados
{
    public class PartidoRepositorio : IPartidoRepositorio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;

        public void Excluir(Partido partido)
        {
            if (Eleicao.Iniciou)
            {
                throw new Exception("As eleições iniciaram não é possivel fazer essa operação");
            }
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
            if (Eleicao.Iniciou)
            {
                throw new Exception("As eleições iniciaram não é possivel fazer essa operação");
            }
            List<Partido> partidoEncontrado = FindByName(item.Nome);
            if (partidoEncontrado.FindAll(partido => partido.Nome == item.Nome).Count != 0
                && partidoEncontrado.FindAll(partido => partido.Slogan == item.Slogan).Count != 0)
            {
                throw new Exception("Nome e slogan ja existentes.");
            }
            
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
            if (Eleicao.Iniciou)
            {
                throw new Exception("As eleições iniciaram não é possivel fazer essa operação");
            }
            List<Partido> partidoEncontrado = FindByName(item.Nome);
            if (partidoEncontrado.FindAll(partido => partido.Nome == item.Nome).Count != 0
                && partidoEncontrado.FindAll(partido => partido.Slogan == item.Slogan).Count != 0)
            {
                throw new Exception("Nome e slogan ja existentes.");
            }

            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "UPDATE Partido SET Nome = @paramNome, Slogan = @paramSlogan, Sigla = @paramSigla WHERE IDPartido = @paramIDPartido";
                comando.AddParameter("paramNome", item.Nome);
                comando.AddParameter("paramSlogan", item.Slogan);
                comando.AddParameter("paramSigla", item.Sigla);
                comando.AddParameter("paramIDPartido", item.Id);
                connection.Open();

                comando.ExecuteNonQuery();

                transacao.Complete();
                connection.Close();
            }
        }

        public Partido FindById(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                Partido partidoEncontrado = null;

                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "SELECT IDPartido,Nome,Slogan,Sigla FROM Partido WHERE IDPartido = @paramIDPartido";

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

                return partidoEncontrado;
            }
        }

        public List<Partido> FindByName(string name)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<Partido> partidosEncontrados = new List<Partido>();

                IDbCommand comando = connection.CreateCommand();
                comando.CommandText = "SELECT IDPartido,Nome,Slogan,Sigla FROM Partido WHERE Nome LIKE '%' + @paramNome + '%'";
                comando.AddParameter("paramNome", name);
                connection.Open();
                IDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    int idDb = Convert.ToInt32(reader["IDPartido"]);
                    string nome = reader["Nome"].ToString();
                    string slogan = reader["Slogan"].ToString();
                    string sigla = reader["Sigla"].ToString();

                    Partido partidoEncontrado = new Partido()
                    {
                        Id = idDb,
                        Nome = nome,
                        Slogan = slogan,
                        Sigla = sigla
                    };

                    partidosEncontrados.Add(partidoEncontrado);
                }

                return partidosEncontrados;
            }
        }
    }
}
