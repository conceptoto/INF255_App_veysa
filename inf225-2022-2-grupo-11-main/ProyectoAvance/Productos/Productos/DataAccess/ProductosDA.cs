using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.BaseDapper;
using Productos.Models;

namespace Productos.DataAccess
{
    public class ProductosDA : BaseDapper<ProductosModel>
    {
        public ProductosDA() { }

        public ProductosDA(string conString) : base(conString) { }

        public ProductosDA(string conString, BaseDapperGeneric.DataBaseType motor) : base(conString, motor) { }
    }
}
