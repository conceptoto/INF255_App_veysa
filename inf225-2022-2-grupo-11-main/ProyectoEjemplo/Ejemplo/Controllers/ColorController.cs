using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ejemplo.Business;
using Ejemplo.DataAccess;
using Ejemplo.Models;
using Transversal.Util.Controller;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;

namespace Ejemplo.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class ColorController : BaseWebApiNoBusController<ColorBusiness, ColorModel>
    {
        public ColorController(string conString, ILogger<ColorController> logger) : base(conString, DataBaseType.SqlServer, new ColorDA(), logger ) { }

        /// <summary>
        /// Obtiene todos los colores, si no se indican parámetros de consulta se retornan todos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns ...</response>
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ColorModel>>> GetColor() => await base.Get();

        /// <summary>
        /// Obtiene color a partir de su ID
        /// </summary>
        /// <param name="id">Identificador del color</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request: TO DO
        /// </remarks>
        /// <response code="200">Returns ...</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ColorModel>> GetColorById(string id) => await base.GetById(id);

        /// <summary>
        /// Permite crear nuevos colores
        /// </summary>
        /// <param name="value">Informacion del tipo de documento</param>
        /// <returns></returns>
        /// <remarks>
        /// Aqui se puede indicar un ejemplo de como llamar este servicio
        /// </remarks>
        /// <response code="200">Inserción correcta</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ColorModel>> PostColor([FromBody]ColorModel value) => await base.Post(value);

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ColorModel>> PutColor([FromBody]ColorModel value) => await base.Put(value);

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ColorModel>> DeleteColor(string id) => await base.Delete(id);
    }
}