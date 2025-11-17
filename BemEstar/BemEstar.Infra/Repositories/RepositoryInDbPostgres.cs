using BemEstar.Dica.Infra.Db;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BemEstar.Dica.Infra.Repositories
{
    public class RepositoryInDbPostgres<T> : IRepository<T> where T : BaseModel
    {
        private IDbConnectionFactory _dbConnectionFactory;
        private readonly string tablename = typeof(T).Name.ToLower();
        public RepositoryInDbPostgres(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }
        public int Create(T entity)
        {

            //Dispose Pattern (Resource Management / RAII)
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();

            //Reflection
            var props = entity.GetType().GetProperties();
            //Montar a query dinamicamente - Via Linq (Forma de fazer o Join/Where/Dictionary) e Reflection (Forma de caapturar dados de forma genérica)
            string columns = string.Join(", ", props.Where(p => p.Name != "Id").Select(p => p.Name.ToLower()));//Forma mais completa e mais rápida porém mais complexa de segmentar as colunas e valores das linhas da tabela
            string parameters = string.Join(", ", props.Where(p => p.Name != "Id").Select(p => "@" + p.Name.ToLower()));
            var propertiesValues = props.Where(p => p.Name != "Id").ToDictionary(p => p.Name.ToLower(), p => p.GetValue(entity, null));

            string commandText = $"INSERT INTO {tablename} {columns} values{parameters})";//Interpolação de string
            using NpgsqlCommand insertCommand = new NpgsqlCommand(commandText, connection);
            foreach (var item in propertiesValues)
            {
                insertCommand.Parameters.AddWithValue(item.Key, item.Value);
            }
            insertCommand.ExecuteNonQuery();
            return 0;
        }

        public void Delete(int id)
        {
            //pegar a conexão com o postgres
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();

            string commandText = $"DELETE FROM {tablename} WHERE id = @id";
            using NpgsqlCommand deleteCommand = new NpgsqlCommand(commandText, connection);
            deleteCommand.Parameters.AddWithValue("id", id);

            deleteCommand.ExecuteNonQuery();
        }

        public bool Exists(int id)
        {
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();
            string CommandText = $"SELECT 1 FROM {tablename} WHERE id = @id LIMIT 1";
            existsCommand.Parameters.AddWithValue("id", id);

            using NpgsqlDataReader dataReader = existsCommand.ExecuteReader();

            return dataReader.Read();
        }

        public List<T> Read()
        {
            //pegar a conexão com o postgres
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();

            string commandText = $"SELECT * FROM {tablename}";
            using NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);

            using NpgsqlDataReader dataReader = selectCommand.ExecuteReader();

            List<T> dicaList = new List<T>();
            var props = typeof(T).GetProperties();

            while (dataReader.Read())
            {
                T entity = (T)Activator.CreateInstance(typeof(T));//Cria uma nova instância do elemento genérico
                foreach (var prop in props)
                {
                    if (dataReader[prop.Name.ToLower()] == null) //hasColumns
                        continue;

                    var colValue = dataReader[prop.Name.ToLower()];
                    if (colValue != DBNull.Value)
                        prop.SetValue(entity, Convert.ChangeType(colValue, prop.PropertyType));  //Quando tem só uma linha o if pode ser sem as chaves                                         
                }

                dicaList.Add(entity);
            }
            return dicaList;
        }

        public T ReadById(int id)
        {
            public DicaModel ReadById(int id)

            //pegar a conexão com o postgres
            using NpgsqlConnection connection = (NpgsqlConnection) this._dbConnectionFactory.GetConnection();

            string commandText = $"SELECT * FROM {tablename} WHERE id = @id";
            using NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);
            selectCommand.Parameters.AddWithValue("id", id);

            using NpgsqlDataReader dataReader = selectCommand.ExecuteReader();
            T entity = (T)Activator.CreateInstance(typeof(T));
            var props = typeof(T).GetProperties();

            if (dataReader.Read())
            {
                foreach(var prop in props)
                {
                    if (dataReader[prop.Name.ToLower()] == null) //hasColumns
                        continue;
                    
                    var colValue = dataReader[prop.Name.ToLower()];
                    if (colValue != DBNull.Value)
                        prop.SetValue(entity, Convert.ChangeType(colValue, prop.PropertyType));  //Quando tem só uma linha o if pode ser sem as chaves                                         
                }
            }
            return entity;
        }

        public void Update(T entity)
        {
            //pegar a conexão com o postgres
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();
            var props = entity.GetType().GetProperties().where(p => p.Name != "Id");
            string setClause = string.Join(", ", props.Select(p => ${p.Name.ToLower()}= @{p.Name.ToLower()}"));

            string commandText = $"UPDATE dica SET {SetClause} WHERE id = @id";
            using NpgsqlCommand updateCommand = new NpgsqlCommand(commandText, connection);
            
            var propertiesValues = props.ToDictionary(p => p.Name.ToLower(), p => p.GetValue(entity, null));
            foreach (var item in propertiesValues)
            {
                updateCommand.Parameters.AddWithValue(item.Key, item.Value);
            }

            updateCommand.ExecuteNonQuery();
        }
    } 
}
