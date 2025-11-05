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
        //Padrão de Projeto Singleton - Fica na memória sempre retorna a mesma instância.
        private static readonly Lazy<DataBase> _instance = new Lazy<DataBase>(() => new DataBase());//Criar uma instância somente quando for utilizada, quando precisar de forma lazy
        private readonly string _connectionString;
      

        //Propriedade para acessar a instância única
        public static DataBase Instance => _instance.Value;
        private DataBase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) //Definir local padrão para acessar as confirgurações como o diretório onde está rodando a aplicação
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<DataBase>()
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            this._connectionString = configuration.GetConnectionString("Postgres");
        }

        //Padrão de Projeto Factory Method
        public NpgsqlConnection GetConnection() 
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
