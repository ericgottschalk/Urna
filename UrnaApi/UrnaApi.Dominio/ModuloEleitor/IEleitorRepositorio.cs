using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.Comum;

namespace UrnaApi.Dominio.ModuloEleitor
{
    public interface IEleitorRepositorio : IRepositorio<Eleitor>
    {
        void Excluir(Eleitor eleitor);
    }
}
