using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemEstar.Dica.Infra.Config
{
    public class AppConfiguration
    {
        private readonly IConfiguration _configuration;
        public AppConfiguration(IConfiguration configuration) 
        {
            this._configuration = configuration;
        }
        public string GetConnectionString(string dbName) 
        {
            return _configuration.GetConnectionString(dbName);
        }
    }
}
