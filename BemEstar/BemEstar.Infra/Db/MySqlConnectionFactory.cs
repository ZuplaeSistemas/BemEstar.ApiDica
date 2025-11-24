using BemEstar.Dica.Infra.Config;
using System.Data;
using MySql.Data.MySqlClient;

namespace BemEstar.Dica.Infra.Db
{
    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public MySqlConnectionFactory(AppConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySql");            
        }
        public IDbConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;            
        }
    }    
}