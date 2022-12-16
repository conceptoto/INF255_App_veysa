using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.Negocio;
using Transversal.Util.BaseDapper;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;
using Ejemplo.Models;
using Ejemplo.DataAccess;


namespace Ejemplo.Business
{
    public class BordeBusiness:BaseNoBusGenericBusiness 
    {
        public BordeBusiness() : base() { }

        public BordeBusiness(string conn, DataBaseType database) : base(conn, database, new GenericDA()) { }

        public async Task<IEnumerable<BordeModel>> GetAll()
        {
            return (await this.ExecutedBasicQuery<BordeModel>("SELECT CodigoBorde,Nombre,Activo FROM Bordes",null));
        }
    }
}
