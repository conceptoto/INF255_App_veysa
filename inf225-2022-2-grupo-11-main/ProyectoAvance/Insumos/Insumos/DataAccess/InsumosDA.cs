using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.BaseDapper;
using Insumos.Models;

namespace Insumos.DataAccess
{
    public class InsumosDA : BaseDapper<InsumosModel>
    {
        public InsumosDA() { }

        public InsumosDA(string conString) : base(conString) { }

        public InsumosDA(string conString, BaseDapperGeneric.DataBaseType motor) : base(conString, motor) { }
    }
}
