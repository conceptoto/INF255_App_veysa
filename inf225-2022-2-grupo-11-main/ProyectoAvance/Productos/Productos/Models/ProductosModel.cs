using Dapper.Contrib.Extensions;

namespace Productos.Models
{
    [Table("Productos")]
    public class ProductosModel
    {
        [ExplicitKey]
        public int CodigoProductos { get; set; }
        public string CodigoInsumos { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tienda { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public string TiempoFabricacionEstimado { get; set; }
        public bool Activo { get; set; }
    }
}