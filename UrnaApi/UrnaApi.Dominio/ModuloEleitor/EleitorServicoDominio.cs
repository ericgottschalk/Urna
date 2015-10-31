using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaApi.Dominio.ModuloEleitor
{
    public class EleitorServicoDominio
    {
        private IEleitorRepositorio repositorio;

        public EleitorServicoDominio(IEleitorRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void Add(Eleitor eleitor)
        {
            this.repositorio.Cadastrar(eleitor);
        }

        public void Editar(Eleitor eleitor)
        {
            this.repositorio.Editar(eleitor);
        }

        public void Excluir(Eleitor eleitor)
        {
            this.repositorio.Excluir(eleitor);
        }

        public Eleitor FindById(int id)
        {
            return this.repositorio.FindById(id);
        }

        public List<Eleitor> FindByName(string nome)
        {
            return this.repositorio.FindByName(nome);
        }
    }
}
