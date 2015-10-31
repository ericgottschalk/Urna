using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.Comum;

namespace UrnaApi.Dominio.ModuloPartido
{
    public interface IPartidoRepositorio : IRepositorio<Partido>
    { 
        void Excluir(Partido partido);
    }
}
