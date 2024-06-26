﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaApi.Dominio.Comum;

namespace UrnaApi.Dominio.ModuloCargo
{
    public interface ICargoRepositorio : IRepositorio<Cargo>
    {
        void MudarSituacao(Cargo cargo);
    }
}
