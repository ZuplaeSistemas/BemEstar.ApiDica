using System;
using System.Collections.Generic;
using BemEstar.Dica.Models;
using BemEstar.Dica.Infrastructure;
using Npgsql;

namespace BemEstar.Dica.Services
{
    // Cont�m os m�todos CRUD para DicaModel
    public class DicaService : BaseDbService
    {
        // Construtor: usa o Singleton da f�brica
        public DicaService() : base(DbFactorySingleton.Instance) { }

        // Cria uma nova dica
        public override void Create(DicaModel model)
        {
            const string sql = "INSERT INTO dica (titulo, descricao, categoria) VALUES (@titulo, @descricao, @categoria)";

            // Chama o m�todo gen�rico para INSERT/UPDATE/DELETE
            ExecuteNonQuery(sql,
                new NpgsqlParameter("@titulo", model.Titulo),
                new NpgsqlParameter("@descricao", model.Descricao),
                new NpgsqlParameter("@categoria", model.Categoria));
        }

        // Deleta uma dica pelo ID
        public override void Delete(int id)
        {
            const string sql = "DELETE FROM dica WHERE id = @id";

            ExecuteNonQuery(sql, new NpgsqlParameter("@id", id));
        }

        // L� todas as dicas
        public override List<DicaModel> Read()
        {
            const string sql = "SELECT * FROM dica";

            // Chama o m�todo gen�rico de SELECT que retorna lista
            return ExecuteQuery(sql, reader => new DicaModel
            {
                Id = Convert.ToInt32(reader["id"]),
                Titulo = reader["titulo"].ToString(),
                Descricao = reader["descricao"].ToString(),
                Categoria = reader["categoria"].ToString()
            });
        }

        // L� uma dica espec�fica pelo ID
        public override DicaModel ReadById(int id)
        {
            const string sql = "SELECT * FROM dica WHERE id = @id";

            return ExecuteQuerySingle(sql, reader => new DicaModel
            {
                Id = Convert.ToInt32(reader["id"]),
                Titulo = reader["titulo"].ToString(),
                Descricao = reader["descricao"].ToString(),
                Categoria = reader["categoria"].ToString()
            }, new NpgsqlParameter("@id", id));
        }

        // Atualiza uma dica existente
        public override void Update(DicaModel model)
        {
            const string sql = "UPDATE dica SET titulo = @titulo, descricao = @descricao, categoria = @categoria WHERE id = @id";

            ExecuteNonQuery(sql,
                new NpgsqlParameter("@titulo", model.Titulo),
                new NpgsqlParameter("@descricao", model.Descricao),
                new NpgsqlParameter("@categoria", model.Categoria),
                new NpgsqlParameter("@id", model.Id));
        }
    }
}
