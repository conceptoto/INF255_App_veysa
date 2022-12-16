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
using Pedidos.Business;
using Pedidos.DataAccess;
using Pedidos.Models;
using Transversal.Util.Controller;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;

namespace Pedidos.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : BaseWebApiNoBusController<PedidosBusiness, PedidosModel>
    {
        public PedidosController(string conString, ILogger<PedidosController> logger) : base(conString, DataBaseType.SqlServer, new PedidosDA(), logger ) { }

        /// <summary>
        /// Obtiene todos los pedidos.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns ...</response>
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PedidosModel>>> GetPedidos() => await base.Get();

        /// <summary>
        /// Obtiene pedidos a partir de su ID
        /// </summary>
        /// <param name="id">Identificador del pedido</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request: TO DO
        /// </remarks>
        /// <response code="200">Returns ...</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PedidosModel>> GetPedidosById(string id) => await base.GetById(id);

        /// <summary>
        /// Permite crear nuevos pedidos
        /// </summary>
        /// <param name="value">Informacion del tipo de documento</param>
        /// <returns></returns>
        /// <remarks>
        /// Aqui se puede indicar un ejemplo de como llamar este servicio
        /// </remarks>
        /// <response code="200">Inserción correcta</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PedidosModel>> PostPedidos([FromBody]PedidosModel value) => await base.Post(value);

        /// <summary>
        /// Actualiza un pedido.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns ...</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PedidosModel>> PutPedidos([FromBody]PedidosModel value) => await base.Put(value);

        /// <summary>
        /// Cambia el estado de Activo.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns ...</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PedidosModel>> DeletePedidos(string id) => await base.Delete(id);
        
        /// <summary>
        /// Permite obtener Pedidos según la tienda
        /// </summary>
        /// <param name="tienda">Tienda a la que pertenece el pedido</param>
        /// <returns>Retorna todos los pedidos correspondientes a la tienda</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Pedidos encontrados</response>
        [HttpGet("{tienda}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PedidosModel>> GetPedidosByTienda(string tienda)
        {
            using var connection = new SqlConnection(conString);
            var mats = await connection.QueryAsync<PedidosModel>("select * from Pedidos where Tienda = @Tienda", new {Tienda = tienda});
            return Ok(mats);
        }
    }
}