namespace BemEstar.Dica.Infra.Repositories
{
    public class RepositoryInMySql<T> : RepositoryInDb<T> where T : BaseModel
    {
        public RepositoryInMySql(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }
       
    } 
}
