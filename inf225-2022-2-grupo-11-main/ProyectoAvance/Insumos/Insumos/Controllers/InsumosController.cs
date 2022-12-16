using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using Dapper;
using Insumos.Business;
using Insumos.DataAccess;
using Insumos.Models;
using Transversal.Util.Controller;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;

namespace Insumos.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class InsumosController : BaseWebApiNoBusController<InsumosBusiness, InsumosModel>
    {
        public InsumosController(string conString, ILogger<InsumosController> logger) : base(conString, DataBaseType.SqlServer, new InsumosDA(), logger ) { }

        /// <summary>
        /// Obtiene todos los Insumos, si no se indican parámetros de consulta se retornan todos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns ...</response>
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<InsumosModel>>> GetInsumos() => await base.Get();

        /// <summary>
        /// Obtiene Insumos a partir de su ID
        /// </summary>
        /// <param name="id">Identificador del Insumo</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request: TO DO
        /// </remarks>
        /// <response code="200">Returns ...</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<InsumosModel>> GetInsumosById(string id) => await base.GetById(id);
        
        /// <summary>
        /// Permite crear nuevos Insumos
        /// </summary>
        /// <param name="value">Informacion del tipo de documento</param>
        /// <returns></returns>
        /// <remarks>
        /// Aqui se puede indicar un ejemplo de como llamar este servicio
        /// </remarks>
        /// <response code="200">Inserción correcta</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<InsumosModel>> PostInsumos([FromBody]InsumosModel value) => await base.Post(value);

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<InsumosModel>> PutInsumos([FromBody]InsumosModel value) => await base.Put(value);

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<InsumosModel>> DeleteInsumos(string id) => await base.Delete(id);

        /// <summary>
        /// Permite obtener Insumos según la tienda
        /// </summary>
        /// <param name="tienda">Tienda a la que pertenece el insumo</param>
        /// <returns>Retorna todos los insumos correspondientes a la tienda</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Insumos encontrados</response>
        [HttpGet("{tienda}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<InsumosModel>> GetInsumosByTienda(string tienda)
        {
            using var connection = new SqlConnection(conString);
            var mats = await connection.QueryAsync<InsumosModel>("select * from Insumos where Tienda = @Tienda", new {Tienda = tienda});
            return Ok(mats);
        }
    }
}