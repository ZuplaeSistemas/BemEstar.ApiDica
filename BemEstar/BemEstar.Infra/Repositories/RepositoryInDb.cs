using BemEstar.Dica.Infra.Db;
using BemEstar.Dica.Models;
using System.Data;

namespace BemEstar.Dica.Infra.Repositories
{
    public class RepositoryInDb<T> : IRepository<T> where T : BaseModel
    {
        private IDbConnectionFactory _dbConnectionFactory;
        private readonly string tablename = typeof(T).Name.ToLower();
        public RepositoryInDb(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }
        private IDbCommand CreateCommand(IDbConnection connection, string commandText)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }
        public int Create(T entity)
        {

            //Dispose Pattern (Resource Management / RAII)
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();

            //Reflection
            var props = entity.GetType().GetProperties();
            //Montar a query dinamicamente - Via Linq (Forma de fazer o Join/Where/Dictionary) e Reflection (Forma de caapturar dados de forma genérica)
            string columns = string.Join(", ", props.Where(p => p.Name != "Id").Select(p => p.Name.ToLower()));//Forma mais completa e mais rápida porém mais complexa de segmentar as colunas e valores das linhas da tabela
            string parameters = string.Join(", ", props.Where(p => p.Name != "Id").Select(p => "@" + p.Name.ToLower()));
            var propertiesValues = props.Where(p => p.Name != "Id").ToDictionary(p => p.Name.ToLower(), p => p.GetValue(entity, null));

            string commandText = $"INSERT INTO {tablename} {columns} values{parameters})";//Interpolação de string
            IDbCommand insertCommand = CreateCommand(connection, commandText);

            foreach (var item in propertiesValues)
            {
                IDbDataParameter parameter = insertCommand.CreateParameter();
                parameter.ParameterName = item.Key;
                parameter.Value = item.Value;
                insertCommand.Parameters.Add(parameter);
            }
            insertCommand.ExecuteNonQuery();
            return 0;
        }

        public void Delete(int id)
        {
            //pegar a conexão com o postgres
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();

            string commandText = $"DELETE FROM {tablename} WHERE id = @id";
            using IDbCommand deleteCommand = CreateCommand(connection, commandText);
         
            IDbDataParameter parameter = deleteCommand.CreateParameter();
            parameter.ParameterName = "id";
            parameter.Value = id;

            deleteCommand.ExecuteNonQuery();
        }

        public bool Exists(int id)
        {
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();
            string CommandText = $"SELECT 1 FROM {tablename} WHERE id = @id LIMIT 1";

            using IDbCommand existsCommand = CreateCommand(connection, commandText);

            IDbDataParameter parameter = existsCommand.CreateParameter();
            parameter.ParameterName = "id";
            parameter.Value = id;
            
            using IDataReader dataReader = existsCommand.ExecuteReader();

            return dataReader.Read();
        }

        public List<T> Read()
        {
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();

            string commandText = $"SELECT * FROM {tablename}";
            using IDbCommand selectCommand = CreateCommand(connection, commandText);

            using IDataReader dataReader = selectCommand.ExecuteReader();

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

            using IDbConnection connection = this._dbConnectionFactory.GetConnection();

            string commandText = $"SELECT * FROM {tablename} WHERE id = @id";
            using IDbCommand selectCommand = CreateCommand(connection, commandText);
            IDbDataParameter parameter = selectCommand.CreateParameter();
            parameter.ParameterName = "id";
            parameter.Value = id;

            using IDataReader dataReader = selectCommand.ExecuteReader();
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
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();
            var props = entity.GetType().GetProperties();
            string setClause = string.Join(", ", props.where(p => p.Name != "Id").Select(p => ${p.Name.ToLower()}= @{p.Name.ToLower()}"));

            string commandText = $"UPDATE {tablename} SET {SetClause} WHERE id = @id";
            using IDbCommand updateCommand = CreateCommand(connection, commandText);
            
            var propertiesValues = props.ToDictionary(p => p.Name.ToLower(), p => p.GetValue(entity, null));
            foreach (var item in propertiesValues)
            {
                IDbDataParameter parameter = updateCommand.CreateParameter();
                parameter.ParameterName = item.Key;
                parameter.Value = item.Value;
                updateCommand.Parameters.Add(parameter);
            }

            updateCommand.ExecuteNonQuery();
        }
    }
}