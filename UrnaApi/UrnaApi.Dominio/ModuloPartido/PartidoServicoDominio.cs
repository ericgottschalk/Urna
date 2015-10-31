using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaApi.Dominio.ModuloPartido
{
    public class PartidoServicoDominio
    {
        private IPartidoRepositorio repositorio;

        public PartidoServicoDominio(IPartidoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void Add(Partido partido)
        {
            this.repositorio.Cadastrar(partido);
        }

        public void Editar(Partido partido)
        {
            this.repositorio.Editar(partido);
        }

        public void Exluir(Partido partido)
        {
            this.repositorio.Excluir(partido);
        }

        public Partido FindById(int id)
        {
            return this.repositorio.FindById(id);
        }

        public List<Partido> FindByName(string name)
        {
            return this.repositorio.FindByName(name);
        }
    }
}
