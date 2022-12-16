using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ejemplo.Models;
using Ejemplo.Business;
using System.Net;
using Transversal.Util.BaseDapper;
using Transversal.Util.Negocio;

namespace ejemplo.netcore.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class BordesController : ControllerBase
    {        
        private readonly ILogger<BordesController> _logger;
        private readonly string _conn;
        private readonly BaseDapperGeneric.DataBaseType _databasetype;

        private BordeBusiness _bordeBusiness;

        public BordesController(ILogger<BordesController> logger,string conString)
        {
            _logger = logger;
            _conn = conString;
            _databasetype = BaseDapperGeneric.DataBaseType.SqlServer;
            _bordeBusiness = new BordeBusiness(_conn,_databasetype);
        }

        /// <summary>
        /// Obtiene el listado de los elementos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Si encuentra el listado</response>
        /// <response code="500">Problemas al obtener los datos</response>
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BordeModel>>> Get()
        {
            try
            {
                return Ok(await _bordeBusiness.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - ...");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        } 
    }
}