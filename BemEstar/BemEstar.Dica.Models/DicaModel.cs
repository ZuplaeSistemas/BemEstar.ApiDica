namespace BemEstar.Dica.Models;

public class DicaModel : BaseModel
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;

    override public string ToString()
    {
        return $"{base.ToString()} - {this.Titulo} {this.Descricao} {this.Categoria}";
    }

}

