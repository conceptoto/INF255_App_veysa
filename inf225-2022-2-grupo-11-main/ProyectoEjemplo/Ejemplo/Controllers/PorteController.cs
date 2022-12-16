using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;
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
    public class PorteController : BaseWebApiNoBusController<PorteBusiness, PorteModel>
    {
        public PorteController(string conString, ILogger<ColorController> logger) : base(conString, DataBaseType.SqlServer, new PorteDA(), logger ) { }

        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PorteModel>>> GetPorte() => await base.Get();

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PorteModel>> GetPorteById(string id) => await base.GetById(id);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PorteModel>> PostPorte([FromBody]PorteModel value) => await base.Post(value);

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PorteModel>> PutPorte([FromBody]PorteModel value) => await base.Put(value);

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PorteModel>> DeletePorte(string id) => await base.Delete(id);
    }
}