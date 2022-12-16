using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.Negocio;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;
using Insumos.Models;
using Insumos.DataAccess;


namespace Insumos.Business
{
    public class InsumosBusiness : BaseNoBusBusiness<InsumosModel>
    {
        public InsumosBusiness() : base() { }

        public InsumosBusiness(string Configuration) : base(Configuration, DataBaseType.SqlServer, new InsumosDA()) { }
    }
}
