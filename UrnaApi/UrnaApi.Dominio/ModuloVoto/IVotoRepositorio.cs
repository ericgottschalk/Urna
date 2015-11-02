using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaApi.Dominio.ModuloVoto
{
    public interface IVotoRepositorio
    {
        void RegistrarVoto(string cpf,int idCandidato);
    }
}
