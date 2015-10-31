using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaApi.Dominio.Comum
{
    //T quando T é EntidadeBase
    public interface IRepositorio<T> where T : EntidadeBase
    {
        void Cadastrar(T item);

        void Editar(T item);
    }
}
