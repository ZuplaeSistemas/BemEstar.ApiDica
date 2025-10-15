namespace BemEstar.Dica.Infrastructure
{
    // Singleton: garante uma única instância da fábrica em toda a aplicação
    public static class DbFactorySingleton
    {
        // Instância única da fábrica de conexão
        private static readonly IDbConnectionFactory _instance =
            new PostgresConnectionFactory(
                "Host=18.220.9.40;Port=5432;Database=dica;Username=postgres;Password=123456;Pooling=true" //pooling=true ativa o pool automático de conexão
            );

        // Propriedade pública para acessar a instância
        public static IDbConnectionFactory Instance => _instance;
    }
}
