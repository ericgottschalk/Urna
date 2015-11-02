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
            throw new NotImplementedException();
        }

        public List<Partido> FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
