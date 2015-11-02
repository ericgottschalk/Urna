using System;
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
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;
        public void Cadastrar(Cargo item)
        {
            if (FindByName(item.Nome).Count != 0)
            {
                throw new Exception("Este cargo já existe");
            }

            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "INSERT INTO Cargo (Nome,Situacao) VALUES (@paramNome,@paramSituacao)";
                comando.AddParameter("paramNome",item.Nome);
                comando.AddParameter("paramSituacao", item.Situacao);
                connection.Open();

                comando.ExecuteNonQuery();

                transacao.Complete();
                connection.Close();
            }
        }

        public void Editar(Cargo item)
        {
            if(FindByName(item.Nome).Count != 0)
            {
                throw new Exception("Já existe um cargo com esse nome");
            }

            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "UPDATE Cargo SET Nome = @paramNome, Situacao = @paramSituacao WHERE IDCargo = @paramIDCargo";
                comando.AddParameter("paramNome", item.Nome);
                comando.AddParameter("paramSituacao", item.Situacao);
                comando.AddParameter("paramIDCargo", item.Id);
                connection.Open();

                comando.ExecuteNonQuery();

                transacao.Complete();
                connection.Close();
            }
        }
        //mudar situação == ativar ou inativar o cargo
        public void MudarSituacao(Cargo cargo)
        {
            if(FindById(cargo.Id) == null)
            {
                throw new Exception("Este cargo não existe");
            }
            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText =
                    "UPDATE Cargo SET Nome = @paramNome, Situacao = @paramSituacao WHERE IDCargo = @paramIDCargo";
                comando.AddParameter("paramNome", cargo.Nome);
                if(cargo.Situacao == 'A')
                {
                    comando.AddParameter("paramSituacao", 'I');
                } else if(cargo.Situacao == 'I')
                {
                    comando.AddParameter("paramSituacao", 'A');
                }                
                comando.AddParameter("paramIDCargo", cargo.Id);
                connection.Open();

                comando.ExecuteNonQuery();

                transacao.Complete();
                connection.Close();
            }
        }

        public Cargo FindById(int id)
        {
            Cargo cargoEncontrado = null;

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

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand comando = connection.CreateCommand();
                comando.CommandText = "SELECT IDCargo,Nome,Situacao FROM Cargo WHERE Nome LIKE '%' + @paramNome + '%'";
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
