using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.Negocio;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;
using Pedidos.Models;
using Pedidos.DataAccess;


namespace Pedidos.Business
{
    public class PedidosBusiness : BaseNoBusBusiness<PedidosModel>
    {
        public PedidosBusiness() : base() { }

        public PedidosBusiness(string Configuration) : base(Configuration, DataBaseType.SqlServer, new PedidosDA()) { }
    }
}
