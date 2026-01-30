using BemEstar.Dica.Infra.Config;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace BemEstar.Dica.Infra.Db
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public NpgsqlConnectionFactory(AppConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(); 
             
        }

        //Padrão de Projeto Factory Method
        public IDbConnection GetConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
