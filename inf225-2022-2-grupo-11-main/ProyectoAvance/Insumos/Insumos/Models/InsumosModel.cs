using Dapper.Contrib.Extensions;

namespace Insumos.Models
{
    [Table("Insumos")]
    public class InsumosModel
    {
        [ExplicitKey]
        public int CodigoInsumos { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string Link_Imagen { get; set; }
        public int Precio { get; set; }
        public string Tienda { get; set; }
        public bool Activo { get; set; }
    }
}