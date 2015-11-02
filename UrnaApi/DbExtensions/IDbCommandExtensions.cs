using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbExtensions
{
    public static class IDbCommandExtensions
    {
        public static void AddParameter(this IDbCommand idb,string nome,object valor)
        {
            IDbDataParameter param = idb.CreateParameter();
            param.ParameterName = nome;
            param.Value = valor ?? DBNull.Value;
            idb.Parameters.Add(param);
        }
    }
}
