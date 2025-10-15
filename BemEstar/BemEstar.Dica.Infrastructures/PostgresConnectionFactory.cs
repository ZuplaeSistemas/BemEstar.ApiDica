using Npgsql;
using System.Data;

namespace BemEstar.Dica.Infrastructure
{
    // Implementação concreta da fábrica para PostgreSQL
    // Segue o padrão Factory Method
    public class PostgresConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        // Recebe a string de conexão no construtor
        public PostgresConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método que cria uma conexão aberta
        public NpgsqlConnection GetOpenConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open(); // Abre explicitamente
            return conn;
        }

        // Método que fecha e libera a conexão
        public void CloseConnection(NpgsqlConnection connection)
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();   // Fecha a conexão
                connection.Dispose(); // Libera recursos
            }
        }
    }
}
