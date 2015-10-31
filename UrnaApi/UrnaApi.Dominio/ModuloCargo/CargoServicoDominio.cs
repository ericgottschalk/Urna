using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaApi.Dominio.ModuloCargo
{
    public class CargoServicoDominio
    {
        private ICargoRepositorio repositorio;

        public CargoServicoDominio(ICargoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void Add(Cargo cargo)
        {
            this.repositorio.Cadastrar(cargo);
        }

        public void Editar(Cargo cargo)
        {
            this.repositorio.Editar(cargo);
        }

        public Cargo FindById(int id)
        {
            return this.repositorio.FindById(id);
        }

        public List<Cargo> FindByName(string nome)
        {
            return this.repositorio.FindByName(name);
        }
    }
}
