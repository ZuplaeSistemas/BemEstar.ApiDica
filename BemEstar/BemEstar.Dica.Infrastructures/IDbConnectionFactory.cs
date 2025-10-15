using Npgsql;

namespace BemEstar.Dica.Infrastructure
{
    // Interface que define os métodos de criação e fechamento de conexões.
    // Permite que qualquer implementação de fábrica siga o mesmo contrato.
    public interface IDbConnectionFactory
    {
        // Retorna uma conexão aberta pronta para uso
        NpgsqlConnection GetOpenConnection();

        // Fecha e libera a conexão
        void CloseConnection(NpgsqlConnection connection);
    }
}
