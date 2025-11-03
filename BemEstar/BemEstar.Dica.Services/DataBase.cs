using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace BemEstar.Dica.Services
{
    internal class DataBase
    {
        private readonly string _connectionString;

        public DataBase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) //Definir local padrão para acessar as confirgurações como o diretório onde está rodando a aplicação
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<DataBase>()
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            this._connectionString = configuration.GetConnectionString("Postgres");
        }

        public NpgsqlConnection GetConnection() 
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public void CloseConnection(NpgsqlConnection connection)
        {
            
                connection.Close();
        
        }
    }
}
