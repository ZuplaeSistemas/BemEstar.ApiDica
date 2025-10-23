using BemEstar.Dica.Models;
using Npgsql; //Não deve ter essa dependência, retirar no futuro.

namespace BemEstar.Dica.Services;

public class DicaService : BaseService<DicaModel>
{
    private DataBase _dataBase;
    public DicaService()
    {
        this._dataBase = new DataBase(); //Ideal é instanciar a variável quando for ser utilizada.
    }
    public override void Create(DicaModel model)
    {

        //pegar a conexão com o postgres
        NpgsqlConnection connection = _dataBase.GetConnection();

        //O código abaixo será substituído pelo código acima
        //NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        //connection.Open();

        string commandText = "INSERT INTO dica (titulo, descricao, categoria) values (@titulo, @descricao, @categoria)";
        NpgsqlCommand insertCommand = new NpgsqlCommand(commandText, connection);
        insertCommand.Parameters.AddWithValue("titulo", model.Titulo);
        insertCommand.Parameters.AddWithValue("descricao", model.Descricao);
        insertCommand.Parameters.AddWithValue("categoria", model.Categoria);

        insertCommand.ExecuteNonQuery();
        _dataBase.CloseConnection(connection);
    }
    public override void Delete(int id)
    {
        //pegar a conexão com o postgres
        NpgsqlConnection connection = _dataBase.GetConnection();

        string commandText = "DELETE FROM dica WHERE id = @id";
        NpgsqlCommand deleteCommand = new NpgsqlCommand(commandText, connection);
        deleteCommand.Parameters.AddWithValue("id", id);
        
        deleteCommand.ExecuteNonQuery();
        _dataBase.CloseConnection(connection);
    }
    public override List<DicaModel> Read()
    {
        //pegar a conexão com o postgres
        NpgsqlConnection connection = _dataBase.GetConnection();

        string commandText = "SELECT * FROM dica";
        NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);
        
        NpgsqlDataReader dataReader = selectCommand.ExecuteReader();

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
        _dataBase.CloseConnection(connection);
        return dicaList;
    }
    public override DicaModel ReadById(int id)
    {
        //pegar a conexão com o postgres
        NpgsqlConnection connection = _dataBase.GetConnection();

        string commandText = "SELECT * FROM dica WHERE id = @id";
        NpgsqlCommand selectCommand = new NpgsqlCommand(commandText, connection);
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
        _dataBase.CloseConnection(connection);
        return dicaModel;
    }
    public override void Update(DicaModel model)
    {
        //pegar a conexão com o postgres
        NpgsqlConnection connection = _dataBase.GetConnection();

        string commandText = "UPDATE dica SET titulo = @titulo, descricao = @descricao, categoria = @categoria WHERE id = @id"; 
        NpgsqlCommand updateCommand = new NpgsqlCommand(commandText, connection);
        updateCommand.Parameters.AddWithValue("titulo", model.Titulo);
        updateCommand.Parameters.AddWithValue("descricao", model.Descricao);
        updateCommand.Parameters.AddWithValue("categoria", model.Categoria);
        updateCommand.Parameters.AddWithValue("id", model.Id);

        updateCommand.ExecuteNonQuery();
        _dataBase.CloseConnection(connection);
    }
}
