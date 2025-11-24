using BemEstar.Dica.Infra.Db;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BemEstar.Dica.Infra.Repositories
{
    public class RepositoryInDbPostgres<T> : RepositoryInDb<T> where T : BaseModel
    {
        public RepositoryInDbPostgres(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }
        
    } 
}
