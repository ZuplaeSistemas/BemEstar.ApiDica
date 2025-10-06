using BemEstar.Dica.Models;
using Npgsql;

namespace BemEstar.Dica.Services;

public class DicaService : BaseService<DicaModel>
{
    private readonly string _connectionString = "Host-=18.220.9.40;Port=5432;Database=dica;Username=postgres;Password=123456";
    public override void Create(DicaModel model)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string commandText = "INSERT INTO dica (titulo, descricao, categoria) values (@titulo, @descricao, @categoria)";
        NpgsqlCommand insertCommand = new NpgsqlCommand(commandText, connection);
        insertCommand.Parameters.AddWithValue("titulo", model.Titulo);
        insertCommand.Parameters.AddWithValue("descricao", model.Descrição);
        insertCommand.Parameters.AddWithValue("categoria", model.Categoria);

        insertCommand.ExecuteNonQuery();
    }
    public override void Delete(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string commandText = "DELETE FROM dica WHERE id = @id";
        NpgsqlCommand deleteCommand = new NpgsqlCommand(commandText, connection);
        deleteCommand.Parameters.AddWithValue("id", id);
        
        deleteCommand.ExecuteNonQuery();
    }
    public override List<DicaModel> Read()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        
        string commandText = "SELECT * FROM dica";
        NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);
        
        NpgsqlDataReader dataReader = selectCommand.ExecuteReader();

        List<DicaModel> dicaList = new List<DicaModel>();

        while (dataReader.Read())
        {
            DicaModel dicaModel = new DicaModel();
            dicaModel.Id = Convert.ToInt32(dataReader["id"]);
            dicaModel.Titulo = dataReader["titulo"].ToString();
            dicaModel.Descrição = dataReader["descricao"].ToString();
            dicaModel.Categoria = dataReader["categoria"].ToString();

            list.Add(dicaModel);
        }
        connection.Close();
        return dicaList;
    }
    public override DicaModel ReadById(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string commandText = "SELECT * FROM dica WHERE id = @id";
        NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);
        selectCommand.Parameters.AddWithValue("id", id);

        NpgsqlDataReader dataReader = selectCommand.ExecuteReader();

        DicaModel dicaModel = new DicaModel();
        if (dataReader.Read())
        {
            dicaModel.Id = Convert.ToInt32(dataReader["id"]);
            dicaModel.Titulo = dataReader["titulo"].ToString();
            dicaModel.Descrição = dataReader["descricao"].ToString();
            dicaModel.Categoria = dataReader["categoria"].ToString();
        }
    }
    public override void Update(DicaModel model)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string commandText = "UPDATE dica SET titulo = @titulo, descricao = @descricao, categoria = @categoria WHERE id = @id"; 
        NpgsqlCommand updateCommand = new NpgsqlCommand(commandText, connection);
        updateCommand.Parameters.AddWithValue("titulo", model.Titulo);
        updateCommand.Parameters.AddWithValue("descricao", model.Descrição);
        updateCommand.Parameters.AddWithValue("categoria", model.Categoria);
        updateCommand.Parameters.AddWithValue("id", model.Id);
        updateCommand.ExecuteNonQuery();

    }
}
