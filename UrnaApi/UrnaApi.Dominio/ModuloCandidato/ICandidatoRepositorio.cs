using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.Comum;

namespace UrnaApi.Dominio.ModuloCandidato
{
    public interface ICandidatoRepositorio : IRepositorio<Candidato>
    {
        void Excluir(Candidato item);

        Candidato BuscarPorNomePopular(string nome);
    }
}
