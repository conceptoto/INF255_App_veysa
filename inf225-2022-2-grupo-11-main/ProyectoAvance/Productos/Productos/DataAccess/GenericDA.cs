using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.BaseDapper;
using Productos.Models;

namespace Productos.DataAccess
{
    public class GenericDA : BaseDapperGeneric
    {
        public GenericDA() { }

        public GenericDA(string conString, BaseDapperGeneric.DataBaseType motor) : base(conString, motor) { }
    }
}
