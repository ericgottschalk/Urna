using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.ModuloEleitor;
using DbExtensions;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace UrnaApi.Dominio.AcessoDados
{
    public class EleitorRepositorio : IEleitorRepositorio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;

        public void Excluir(Eleitor eleitor)
        {
            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Eleitor WHERE IDEleitor = @paramId";
                cmd.AddParameter("paramId", eleitor.Id);

                connection.Open();
                cmd.ExecuteNonQuery();

                transacao.Complete();
            }
        }

        public void Cadastrar(Eleitor item)
        {
            if (!this.ValidarEleitor(item))
            {
                throw new Exception("Eleitor não pode ser inserido");
            }

            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Eleitor(Nome, TituloEleitoral, RG, CPF, DataNascimento, ZonaEleitoral, Secao, Situacao, Votou) "
                                  + "VALUES (@paramNome,@paramTitulo,@paramRG,@paramCPF,@paramData,@paramZona,@paramSecao,@paramSituacao,@paramVotou)";

                cmd.AddParameter("paramNome", item.Nome);
                cmd.AddParameter("paramTitulo", item.TituloEleitor);
                cmd.AddParameter("paramRG", item.RG);
                cmd.AddParameter("paramCPF", item.CPF);
                cmd.AddParameter("paramData", item.DataNascimento);
                cmd.AddParameter("paramZona", item.ZonaEleitoral);
                cmd.AddParameter("paramSecao", item.Secao);
                cmd.AddParameter("paramSituacao", item.Situacao);
                cmd.AddParameter("paramVotou", item.Votou);

                connection.Open();
                cmd.ExecuteNonQuery();

                transacao.Complete();
            }                
        }

        public void Editar(Eleitor item)
        {
            if (!this.ValidarEleitor(item))
            {
                throw new Exception("Eleitor não pode ser editado");
            }

            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE Eleitor SET Nome=@paramNome, TituloEleitoral=@paramTitulo, RG=@paramRG, CPF=@paramCPF, DataNascimento=@paramData, "
                                  + "ZonaEleitoral=@paramZona, Secao=@paramSecao, Situacao=@paramSituacao, Votou=@paramVotou " 
                                  + "WHERE idEleitor = @paramIdEleitor";

                cmd.AddParameter("paramNome", item.Nome);
                cmd.AddParameter("paramTitulo", item.TituloEleitor);
                cmd.AddParameter("paramRG", item.RG);
                cmd.AddParameter("paramCPF", item.CPF);
                cmd.AddParameter("paramData", item.DataNascimento);
                cmd.AddParameter("paramZona", item.ZonaEleitoral);
                cmd.AddParameter("paramSecao", item.Secao);
                cmd.AddParameter("paramSituacao", item.Situacao);
                cmd.AddParameter("paramVotou", item.Votou);
                cmd.AddParameter("paramIdEleitor", item.Id);

                connection.Open();
                cmd.ExecuteNonQuery();

                transacao.Complete();
            }
        }

        public Eleitor FindById(int id)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                var eleitor = new Eleitor();

                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Eleitor WHERE IDEleitor = @paramId";
                cmd.AddParameter("paramId", id);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    eleitor = this.CriarEleitor(reader);
                }

                return eleitor;
            }
        }

        public List<Eleitor> FindByName(string name)
        {
            using(IDbConnection connection = new SqlConnection(this.connectionString))
            {
                List<Eleitor> eleitores = new List<Eleitor>();

                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Eleitor WHERE Nome LIKE '%' + @paramNome + '%'";
                cmd.AddParameter("paramNome", name);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    eleitores.Add(this.CriarEleitor(reader));
                }

                return eleitores;
            }
        }

        public Eleitor BuscarPorRG(string rg)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                Eleitor eleitor = null;

                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Eleitor WHERE RG = @paramRg";
                cmd.AddParameter("paramRg", rg);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    eleitor = this.CriarEleitor(reader);
                }

                return eleitor;
            }
        }

        public Eleitor BuscarPorCpf(string cpf)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                Eleitor eleitor = null;

                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Eleitor WHERE CPF = @paramCpf";
                cmd.AddParameter("paramCpf", cpf);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    eleitor = this.CriarEleitor(reader);
                }

                return eleitor;
            }
        }

        public Eleitor BuscarPorTituloEleitor(string titulo)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                Eleitor eleitor = null;

                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Eleitor WHERE TituloEleitoral = @paramTitulo";
                cmd.AddParameter("paramTitulo", titulo);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    eleitor = this.CriarEleitor(reader);
                }

                return eleitor;
            }
        }

        private bool ValidarEleitor(Eleitor eleitor)
        {
            bool valido = true;

            Eleitor tempEleitor = this.BuscarPorCpf(eleitor.CPF);
            if (tempEleitor != null && tempEleitor.Id != eleitor.Id)
            {
                valido = false;
            }

            tempEleitor = this.BuscarPorRG(eleitor.RG);
            if (tempEleitor != null && tempEleitor.Id != eleitor.Id)
            {
                valido = false;
            }

            tempEleitor = this.BuscarPorTituloEleitor(eleitor.TituloEleitor);
            if (tempEleitor != null && tempEleitor.Id != eleitor.Id)
            {
                valido = false;
            }

            return valido;
        }

        private Eleitor CriarEleitor(IDataReader reader)
        {
            return new Eleitor()
            {
                Id = Convert.ToInt32(reader["IDEleitor"]),
                Nome = reader["Nome"].ToString(),
                DataNascimento = Convert.ToDateTime(reader["DataNascimento"].ToString()),
                TituloEleitor = reader["TituloEleitoral"].ToString(),
                RG = reader["RG"].ToString(),
                CPF = reader["CPF"].ToString(),
                ZonaEleitoral = reader["ZonaEleitoral"].ToString(),
                Secao = reader["Secao"].ToString(),
                Situacao =Convert.ToChar(reader["Situacao"].ToString()),
                Votou = Convert.ToChar(reader["Votou"].ToString())
            };
        }
    }
}
