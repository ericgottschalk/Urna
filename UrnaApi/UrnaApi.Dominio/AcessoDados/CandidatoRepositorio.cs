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

namespace UrnaApi.Dominio.AcessoDados
{
    public class CandidatoRepositorio : ICandidatoRepositorio
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["URNA"].ConnectionString;

        public void Excluir(Candidato item)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Candidato item)
        {
            throw new NotImplementedException();
        }

        public void Editar(Candidato item)
        {
            throw new NotImplementedException();
        }

        public Candidato FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Candidato> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Candidato BuscarPorNomePopular(string nome)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                Candidato candidato = null;

                IDbCommand cmd = connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM Candidado WHERE NomePopulas = @paramNomePopuar";
                cmd.AddParameter("paramNomePopuar", nome);
                connection.Open();

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    candidato = new Candidato()
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

                return candidato;
            }
        }

        public bool ValidarCandidato(Candidato canditato)
        {
            bool valido = true;

            if (String.IsNullOrWhiteSpace(canditato.NomeCompleto) || String.IsNullOrWhiteSpace(canditato.NomePopular))
            {
                valido = false;
            }

            if (BuscarPorNomePopular(canditato.NomePopular) != null)
            {
                valido = false;
            }

            return valido;
        }
    }
}
