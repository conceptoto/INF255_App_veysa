using Dapper.Contrib.Extensions;

namespace Pedidos.Models
{
    [Table("Pedidos")]
    public class PedidosModel
    {
        [ExplicitKey]
        public int CodigoPedidos { get; set; }
        public int CodigoProductos { get; set; }
        public int Precio { get; set; }
        public string Tienda { get; set; }
        public string NombreCliente { get; set; }
        public string CorreoCliente { get; set; }
        public string CelularCliente { get; set; }
        public string Fecha { get; set; }
        public string Estado { get; set; }
        public bool Activo { get; set; }
    }
}