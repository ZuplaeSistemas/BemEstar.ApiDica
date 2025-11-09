using BemEstar.Dica.Infra.Db;
using BemEstar.Dica.Infra.Repositories;
using BemEstar.Dica.Models;
using Npgsql; //Não deve ter essa dependência, retirar no futuro.

namespace BemEstar.Dica.Services;

public class DicaService : Service<DicaModel>
{
    public DicaService(DicaRepository repository) : base(repository)
    {

    }

}

