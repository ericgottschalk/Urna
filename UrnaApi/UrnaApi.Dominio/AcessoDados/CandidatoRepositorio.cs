using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.ModuloCandidato;
using System.Transactions;
using UrnaApi.Dominio.ModuloEleicao;

namespace UrnaApi.Dominio.AcessoDados
{
    public class CandidatoRepositorio : ICandidatoRepositorio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;
        
        public void Excluir(Candidato item)
        {
            if (item.NomeCompleto == "Voto Nulo" || item.NomeCompleto == "Voto em Branco")
            {
                throw new Exception("Candidato não pode ser excluido");
            }
            if (Eleicao.Iniciou)
            {
                throw new Exception("As eleições iniciaram não é possivel fazer essa operação");
            }
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
            if (Eleicao.Iniciou)
            {
                throw new Exception("As eleições iniciaram não é possivel fazer essa operação");
            }
            using (TransactionScope transation = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Candidato (nomeCompleto, nomePopular, dataNascimento, registroTRE, idPartido, foto, numero, idCargo, exibe)"
                    + "VALUES (@paramNomeCompleto, @paramNomePopular, @paramData, @paramRegistroTRE, @paramIDPartido, @paramFoto, @paramNumero, @paramIdCargo, @paramExibe)";

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
            if (Eleicao.Iniciou)
            {
                throw new Exception("As eleições iniciaram não é possivel fazer essa operação");
            }
            using (TransactionScope transation = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE Candidato SET NomeCompleto = @paramNomeCompleto, NomePopular = @paramNomePopular,"
                                  + "RegistroTRE = @paramRegistroTRE, DataNascimento = @paramData, IdPartido = @paramIDPartido,"
                                  + "Foto = @paramFoto, Numero = @paramNumero, IdCargo = @paramIdCargo, Exibe = @paramExibe "
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

                cmd.CommandText = "SELECT IDCandidato,NomeCompleto,NomePopular,DataNascimento,RegistroTRE,IDPartido,Foto,Numero,IDCargo,Exibe FROM Candidato WHERE IdCandidato = @paramId";               
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

                cmd.CommandText = "SELECT IDCandidato,NomeCompleto,NomePopular,DataNascimento,RegistroTRE,IDPartido,Foto,Numero,IDCargo,Exibe FROM Candidato WHERE NomeCompleto LIKE '%' + @paramNome + '%'";
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

                cmd.CommandText = "SELECT IDCandidato,NomeCompleto,NomePopular,DataNascimento,RegistroTRE,IDPartido,Foto,Numero,IDCargo,Exibe FROM Candidato WHERE NomePopular = @paramNomePopular";
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

                cmd.CommandText = "SELECT IDCandidato,NomeCompleto,NomePopular,DataNascimento,RegistroTRE,IDPartido,Foto,Numero,IDCargo,Exibe FROM Candidato WHERE RegistroTRE = @paramRegistroTRE";
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

                cmd.CommandText = "SELECT IDCandidato,NomeCompleto,NomePopular,DataNascimento,RegistroTRE,IDPartido,Foto,Numero,IDCargo,Exibe FROM Candidato WHERE Numero = @paramNumero";
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

                string cmdTxt = "SELECT * FROM Candidato INNER JOIN Cargo ON Cargo.IDCargo = Candidato.IDCargo WHERE Candidato.IdPartido = @paramIdPartido AND Cargo.Nome = 'Prefeito' AND Candidato.NomeCompleto <> 'Voto Nulo' AND Candidato.NomeCompleto <> 'Voto em Branco'";

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
            var tempCandidato = new Candidato();
            bool valido = true;

            if (String.IsNullOrWhiteSpace(canditato.NomeCompleto) || String.IsNullOrWhiteSpace(canditato.NomePopular))
            {
                valido = false;
            }

            tempCandidato = this.BuscarPorNomePopular(canditato.NomePopular);
            if (tempCandidato != null && tempCandidato.Id != canditato.Id)
            {
                valido = false;
            }

            tempCandidato = this.BuscarPorRegistroTRE(canditato.RegistroTRE);
            if (tempCandidato != null && tempCandidato.Id != canditato.Id)
            {
                valido = false;
            }

            tempCandidato = this.BuscarPorNumero(canditato.Numero);
            if (tempCandidato != null && tempCandidato.Id != canditato.Id)
            {
                valido = false;
            }

            tempCandidato = this.BuscarPrefeitoPorPartido(canditato.IdPartido);
            if (tempCandidato != null && tempCandidato.Id != canditato.Id)
            {
                valido = false;
            }

            return valido;
        }

        public Candidato CriarCandidato(IDataReader reader)
        {
            return new Candidato()
            {
                Id = Convert.ToInt32(reader["IDCandidato"]),
                NomeCompleto = reader["NomeCompleto"].ToString(),
                NomePopular = reader["NomePopular"].ToString(),
                DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                RegistroTRE = reader["RegistroTRE"].ToString(),
                IdPartido = Convert.ToInt32(reader["IDPartido"]),
                IdCargo = Convert.ToInt32(reader["IDCargo"]),
                Numero = Convert.ToInt32(reader["Numero"]),
                Exibe = Convert.ToBoolean(Convert.ToInt32(reader["Exibe"])),
                Foto = reader["Foto"].ToString()
            };
        }
    }
}
