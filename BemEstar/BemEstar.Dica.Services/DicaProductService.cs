using BemEstar.Dica.Infra.Db;
using BemEstar.Dica.Infra.Repositories;
using BemEstar.Dica.Models;
using Npgsql; //N�o deve ter essa depend�ncia, retirar no futuro.

namespace BemEstar.Dica.Services;

public class DicaProductService : Service<DicaProduct>
{
    public DicaProductService(DicaProductRepository repository) : base(repository)
    {

    }

}

