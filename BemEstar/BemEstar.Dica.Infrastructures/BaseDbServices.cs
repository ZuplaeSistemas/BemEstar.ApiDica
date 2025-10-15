using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BemEstar.Dica.Infrastructure
{
    // Classe abstrata que fornece métodos genéricos para CRUD
    // Segue o padrão Template Method
    public abstract class BaseDbService
    {
        private readonly IDbConnectionFactory _factory;

        // Recebe a fábrica via injeção de dependência
        protected BaseDbService(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        // Método genérico para INSERT, UPDATE, DELETE (não retorna dados)
        protected int ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
        {
            // Obtém conexão aberta da fábrica
            var conn = _factory.GetOpenConnection();
            try
            {
                var cmd = new NpgsqlCommand(sql, conn);

                // Adiciona parâmetros se houver
                if (parameters?.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                // Executa o comando
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                // Fecha e devolve a conexão para o pool
                _factory.CloseConnection(conn);
            }
        }

        // Método genérico para SELECTs que retornam lista de objetos
        protected List<T> ExecuteQuery<T>(string sql, Func<IDataReader, T> map, params NpgsqlParameter[] parameters)
        {
            var conn = _factory.GetOpenConnection();
            try
            {
                var cmd = new NpgsqlCommand(sql, conn);

                if (parameters?.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                var reader = cmd.ExecuteReader();
                var list = new List<T>();

                while (reader.Read())
                {
                    // Mapeia cada linha para o objeto T
                    list.Add(map(reader));
                }

                // Fecha e libera recursos
                reader.Close();
                reader.Dispose();
                cmd.Dispose();

                return list;
            }
            finally
            {
                _factory.CloseConnection(conn);
            }
        }

        // Método genérico para SELECTs que retornam apenas um objeto
        protected T ExecuteQuerySingle<T>(string sql, Func<IDataReader, T> map, params NpgsqlParameter[] parameters)
        {
            var conn = _factory.GetOpenConnection();
            try
            {
                var cmd = new NpgsqlCommand(sql, conn);

                if (parameters?.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                var reader = cmd.ExecuteReader();
                T item = default;

                if (reader.Read())
                {
                    item = map(reader);
                }

                reader.Close();
                reader.Dispose();
                cmd.Dispose();

                return item;
            }
            finally
            {
                _factory.CloseConnection(conn);
            }
        }
    }
}
