using BemEstar.Dica.Models;
using Npgsql; //Não deve ter essa dependência, retirar no futuro.

namespace BemEstar.Dica.Services;

public class DicaService : BaseService<DicaModel>
{
    private DataBase _dataBase;
    public DicaService()
    {
        this._dataBase = DataBase.Instance; //Ideal é instanciar a variável quando for ser utilizada.
    }
    public override void Create(DicaModel model)
    {

        //pegar a conexão com o postgres
        using NpgsqlConnection connection = this._dataBase.GetConnection();

        //O código abaixo será substituído pelo código acima
        //NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        //connection.Open();

        string commandText = "INSERT INTO dica (titulo, descricao, categoria) values (@titulo, @descricao, @categoria)";
        using NpgsqlCommand insertCommand = new NpgsqlCommand(commandText, connection);
        insertCommand.Parameters.AddWithValue("titulo", model.Titulo);
        insertCommand.Parameters.AddWithValue("descricao", model.Descricao);
        insertCommand.Parameters.AddWithValue("categoria", model.Categoria);

        insertCommand.ExecuteNonQuery();
    }
    public override void Delete(int id)
    {
        //pegar a conexão com o postgres
        using NpgsqlConnection connection = this._dataBase.GetConnection();

        string commandText = "DELETE FROM dica WHERE id = @id";
        using NpgsqlCommand deleteCommand = new NpgsqlCommand(commandText, connection);
        deleteCommand.Parameters.AddWithValue("id", id);
        
        deleteCommand.ExecuteNonQuery();
    }
    public override void Update(DicaModel model)
    {
        //pegar a conexão com o postgres
        using NpgsqlConnection connection = this._dataBase.GetConnection();

        string commandText = "UPDATE dica SET titulo = @titulo, descricao = @descricao, categoria = @categoria WHERE id = @id";
        using NpgsqlCommand updateCommand = new NpgsqlCommand(commandText, connection);
        updateCommand.Parameters.AddWithValue("titulo", model.Titulo);
        updateCommand.Parameters.AddWithValue("descricao", model.Descricao);
        updateCommand.Parameters.AddWithValue("categoria", model.Categoria);
        updateCommand.Parameters.AddWithValue("id", model.Id);

        updateCommand.ExecuteNonQuery();
    }
    public override List<DicaModel> Read()
    {
        //pegar a conexão com o postgres
        using NpgsqlConnection connection = this._dataBase.GetConnection();

        string commandText = "SELECT * FROM dica";
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
        return dicaList;
    }
    public override DicaModel ReadById(int id)
    {
        //pegar a conexão com o postgres
        using NpgsqlConnection connection = this._dataBase.GetConnection();

        string commandText = "SELECT * FROM dica WHERE id = @id";
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
        return dicaModel;
    }
}
