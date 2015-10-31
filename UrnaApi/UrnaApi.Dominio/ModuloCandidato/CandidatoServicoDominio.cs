using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaApi.Dominio.ModuloCandidato
{
    public class CandidatoServicoDominio
    {
        private ICandidatoRepositorio repositorio;

        public CandidatoServicoDominio(ICandidatoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void Add(Candidato canditado)
        {
            this.repositorio.Cadastrar(canditado);
        }

        public void Editar(Candidato candidato)
        {
            this.repositorio.Editar(candidato);
        }

        public void Excluir(Candidato candidato)
        {
            this.repositorio.Excluir(candidato);
        }

        public Candidato FindById(int id)
        {
            return this.repositorio.FindById(id);
        }

        public List<Candidato> FindByName(string nome)
        {
            return this.repositorio.FindByName(nome);
        }
    }
}
