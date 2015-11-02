using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.ModuloPartido;
using DbExtensions;

namespace UrnaApi.Dominio.AcessoDados
{
    public class PartidoRepositorio : IPartidoRepositorio
    {
        public void Excluir(Partido partido)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Partido item)
        {
            throw new NotImplementedException();
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
