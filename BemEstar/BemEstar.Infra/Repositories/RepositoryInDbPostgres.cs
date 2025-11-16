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
            string parameters = string.Join(", ",props.Where(p => p.Name != "Id").Select(p => "@" + p.Name.ToLower()));
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
            throw new NotImplementedException();
        }

        public List<T> Read()
        {
            //pegar a conexão com o postgres
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();

            string commandText = $"SELECT * FROM {tablename}";
            using NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);

            using NpgsqlDataReader dataReader = selectCommand.ExecuteReader();

            List<DicaModel> dicaList = new List<DicaModel>();

            while (dataReader.Read())
            {
                DicaModel dicaModel = new DicaModel();
                dicaModel.Id = Convert.ToInt32(dataReader["id"]);
                dicaModel.Titulo = dataReader["titulo"].ToString();
                dicaModel.Descricao = dataReader["descricao"].ToString();
                dicaModel.Categoria = dataReader["categoria"].ToString();

                dicaList.Add(dicaModel);
            }
            return new List<T>();
        }

        public T ReadById(int id)
        {
            public DicaModel ReadById(int id)

            //pegar a conexão com o postgres
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();

            string commandText = $"SELECT * FROM {tablename} WHERE id = @id";
            using NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);
            selectCommand.Parameters.AddWithValue("id", id);

            NpgsqlDataReader dataReader = selectCommand.ExecuteReader();

            DicaModel dicaModel = new DicaModel();
            if (dataReader.Read())
            {
                dicaModel.Id = Convert.ToInt32(dataReader["id"]);
                dicaModel.Titulo = dataReader["titulo"].ToString();
                dicaModel.Descricao = dataReader["descricao"].ToString();
                dicaModel.Categoria = dataReader["categoria"].ToString();
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public void Update(T entity)
        {
            //pegar a conexão com o postgres
            using NpgsqlConnection connection = (NpgsqlConnection)this._dbConnectionFactory.GetConnection();

            string commandText = "UPDATE dica SET titulo = @titulo, descricao = @descricao, categoria = @categoria WHERE id = @id";
            using NpgsqlCommand updateCommand = new NpgsqlCommand(commandText, connection);
            updateCommand.Parameters.AddWithValue("titulo", model.Titulo);
            updateCommand.Parameters.AddWithValue("descricao", model.Descricao);
            updateCommand.Parameters.AddWithValue("categoria", model.Categoria);
            updateCommand.Parameters.AddWithValue("id", model.Id);

            updateCommand.ExecuteNonQuery();
        }
    } 
}
