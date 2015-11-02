using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.ModuloCandidato;
using DbExtensions;
using System.Transactions;

namespace UrnaApi.Dominio.AcessoDados
{
    public class CandidatoRepositorio : ICandidatoRepositorio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;

        public void Excluir(Candidato item)
        {
            using (TransactionScope transacao = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Candidato WHERE idCandidato = @paramId";
                cmd.AddParameter("paramId", item.Id);

                connection.Open();
                cmd.ExecuteNonQuery();

                transacao.Complete();
            }
        }

        public void Cadastrar(Candidato item)
        {
            if (!this.ValidarCandidato(item))
            {
                throw new Exception("Candidato não pode ser inserido");
            }

            using (TransactionScope transation = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Candidato (idCandidato, nomeCompleto, nomePopular, dataNascimento, registroTRE, idPartido, foto, numero, idCargo, exibe)"
                    + "VALUES (@paramId, @paramNomeCompleto, @paramNomePopular, @paramData, @paramRegistroTRE, @paramIDPartido, @paramFoto, @paramNumero, @paramIdCargo, @paramExibe)";

                cmd.AddParameter("paramId", item.Id);
                cmd.AddParameter("paramNomeCompleto", item.NomeCompleto);
                cmd.AddParameter("paramNomePopular", item.NomePopular);
                cmd.AddParameter("paramData", item.DataNascimento);
                cmd.AddParameter("paramRegistroTRE", item.RegistroTRE);
                cmd.AddParameter("paramIDPartido", item.IdPartido);
                cmd.AddParameter("paramFoto", item.Foto);
                cmd.AddParameter("paramNumero", item.Numero);
                cmd.AddParameter("paramIdCargo", item.IdCargo);
                cmd.AddParameter("paramExibe", item.Exibe);

                connection.Open();
                cmd.ExecuteNonQuery();

                transation.Complete();
            }
        }

        public void Editar(Candidato item)
        {
            if (!this.ValidarCandidato(item))
            {
                throw new Exception("Candidato não pode ser editado");
            }

            using (TransactionScope transation = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE Candidato SET NomeCompleto = @paramNomeCompleto, NomePopular = @paramNomePopular,"
                                  + "RegistroTRE = @paramRegistroTRE, DataNascimento = @paramData, IdPartido = @paramIDPartido,"
                                  + "Foto = @paramFoto, Numero = @paramNumero, @IdCargo = @paramIdCargo, Exibe = @paramExibe"
                                  + "WHERE IdCandidato = @paramId";

                
                cmd.AddParameter("paramNomeCompleto", item.NomeCompleto);
                cmd.AddParameter("paramNomePopular", item.NomePopular);
                cmd.AddParameter("paramRegistroTRE", item.RegistroTRE);
                cmd.AddParameter("paramData", item.DataNascimento);
                cmd.AddParameter("paramIDPartido", item.IdPartido);
                cmd.AddParameter("paramFoto", item.Foto);
                cmd.AddParameter("paramNumero", item.Numero);
                cmd.AddParameter("paramIdCargo", item.IdCargo);
                cmd.AddParameter("paramExibe", item.Exibe);
                cmd.AddParameter("paramId", item.Id);

                connection.Open();
                cmd.ExecuteNonQuery();

                transation.Complete();
            }
        }

        public Candidato FindById(int id)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                Candidato candidato = null;

                IDbCommand cmd = connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM Candidado WHERE IdCandidato = @paramId";
                cmd.AddParameter("paramId", id);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    candidato = this.CriarCandidato(reader);
                }

                return candidato;
            }
        }

        public List<Candidato> FindByName(string name)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                List<Candidato> candidatos = new List<Candidato>();

                IDbCommand cmd = connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM Candidado WHERE NomeCompleto LIKE '%@paramNome%'";
                cmd.AddParameter("paramNome", name);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    candidatos.Add(this.CriarCandidato(reader));
                }

                return candidatos;
            }
        }

        public Candidato BuscarPorNomePopular(string nome)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                Candidato candidato = null;

                IDbCommand cmd = connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM Candidado WHERE NomePopular = @paramNomePopular";
                cmd.AddParameter("paramNomePopular", nome);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    candidato = this.CriarCandidato(reader);
                }

                return candidato;
            }
        }

        public Candidato BuscarPorRegistroTRE(string registro)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                Candidato candidato = null;

                IDbCommand cmd = connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM Candidado WHERE RegistroTRE = @paramRegistroTRE";
                cmd.AddParameter("paramRegistroTRE", registro);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    candidato = this.CriarCandidato(reader);
                }

                return candidato;
            }
        }

        public Candidato BuscarPorNumero(int numero)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                Candidato candidato = null;

                IDbCommand cmd = connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM Candidado WHERE Numero = @paramNumero";
                cmd.AddParameter("paramNumero", numero);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    candidato = this.CriarCandidato(reader);
                }

                return candidato;
            }
        }

        private Candidato BuscarPrefeitoPorPartido(int IdPartido)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                Candidato candidato = null;

                IDbCommand cmd = connection.CreateCommand();

                string cmdTxt = "SELECT * FROM Candidato C"
                          + "INNER JOIN Cargo CA ON CA.IDCargo = C.IDCargo"
                          + "WHERE C.IdPartido = @paramIdPartido AND CA.Nome = 'Prefeito'"
                          + "AND C.NomeCompleto <> 'Voto Nulo' AND C.NomeCompleto <> 'Voto em Branco'";

                cmd.CommandText = cmdTxt;
                cmd.AddParameter("paramIdPartido", IdPartido);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    candidato = this.CriarCandidato(reader);
                }

                return candidato;
            }
        }

        private bool ValidarCandidato(Candidato canditato)
        {
            bool valido = true;

            if (String.IsNullOrWhiteSpace(canditato.NomeCompleto) || String.IsNullOrWhiteSpace(canditato.NomePopular))
            {
                valido = false;
            }

            if (this.BuscarPorNomePopular(canditato.NomePopular) != null)
            {
                valido = false;
            }

            if (this.BuscarPorRegistroTRE(canditato.RegistroTRE) != null)
            {
                valido = false;
            }

            if (this.BuscarPorNumero(canditato.Numero) != null)
            {
                valido = false;
            }

            if (this.BuscarPrefeitoPorPartido(canditato.IdPartido) != null)
            {
                valido = false;
            }

            return valido;
        }

        public Candidato CriarCandidato(IDataReader reader)
        {
            return new Candidato()
            {
                Id = Convert.ToInt32(reader["IdCandidato"]),
                NomeCompleto = reader["NomeCompleto"].ToString(),
                NomePopular = reader["NomePopular"].ToString(),
                DataNascimento = Convert.ToDateTime(reader["DataNacimento"]),
                RegistroTRE = reader["RegistroTRE"].ToString(),
                IdPartido = Convert.ToInt32(reader["idPartido"]),
                IdCargo = Convert.ToInt32(reader["IDCargo"]),
                Numero = Convert.ToInt32(reader["Numero"]),
                Exibe = Convert.ToBoolean(Convert.ToInt32(reader["Exibe"])),
                Foto = reader["Foto"].ToString()
            };
        }
    }
}
