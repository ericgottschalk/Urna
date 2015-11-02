using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.ModuloEleitor;
using DbExtensions;

namespace UrnaApi.Dominio.AcessoDados
{
    public class EleitorRepositorio : IEleitorRepositorio
    {
        public void Excluir(Eleitor eleitor)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Eleitor item)
        {
            throw new NotImplementedException();
        }

        public void Editar(Eleitor item)
        {
            throw new NotImplementedException();
        }

        public Eleitor FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Eleitor> FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
