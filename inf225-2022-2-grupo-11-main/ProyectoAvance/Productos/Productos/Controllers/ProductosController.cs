using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;
using Productos.Business;
using Productos.DataAccess;
using Productos.Models;
using Transversal.Util.Controller;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;

namespace Productos.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : BaseWebApiNoBusController<ProductosBusiness, ProductosModel>
    {
        public ProductosController(string conString, ILogger<ProductosController> logger) : base(conString, DataBaseType.SqlServer, new ProductosDA(), logger ) { }

        /// <summary>
        /// Obtiene todos los Productos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns ...</response>
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductosModel>>> GetProductos() => await base.Get();

        /// <summary>
        /// Obtiene Productos a partir de su ID
        /// </summary>
        /// <param name="id">Identificador del Producto</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request: TO DO
        /// </remarks>
        /// <response code="200">Returns ...</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductosModel>> GetProductosById(string id) => await base.GetById(id);

        /// <summary>
        /// Permite crear nuevos Productos
        /// </summary>
        /// <param name="value">Informacion del tipo de documento</param>
        /// <returns></returns>
        /// <remarks>
        /// Aqui se puede indicar un ejemplo de como llamar este servicio
        /// </remarks>
        /// <response code="200">Inserción correcta</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductosModel>> PostProductos([FromBody]ProductosModel value) => await base.Post(value);

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductosModel>> PutProductos([FromBody]ProductosModel value) => await base.Put(value);

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductosModel>> DeleteProductos(string id) => await base.Delete(id);
    
        /// <summary>
        /// Permite obtener Productos según la tienda
        /// </summary>
        /// <param name="tienda">Tienda a la que pertenece el producto</param>
        /// <returns>Retorna todos los productos correspondientes a la tienda</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Productos encontrados</response>
        [HttpGet("{tienda}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductosModel>> GetProductosByTienda(string tienda)
        {
            using var connection = new SqlConnection(conString);
            var mats = await connection.QueryAsync<ProductosModel>("select * from Productos where Tienda = @Tienda", new {Tienda = tienda});
            return Ok(mats);
        }

        /// <summary>
        /// Permite obtener Productos según su descripción
        /// </summary>
        /// <param name="descripcion">Descripción del producto</param>
        /// <returns>Retorna todos los productos correspondientes a esa descripcion</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Productos encontrados</response>
        [HttpGet("{descripcion:alpha}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductosModel>> GetProductosByDescripcion(string descripcion)
        {
            using var connection = new SqlConnection(conString);
            var mats = await connection.QueryAsync<ProductosModel>("select * from Productos where Descripcion like '%' + @Descripcion + '%'", new {Descripcion = descripcion});
            return Ok(mats);
        }
    }
}