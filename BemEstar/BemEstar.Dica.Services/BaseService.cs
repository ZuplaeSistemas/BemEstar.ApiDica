using System.Collections.Generic;
using BemEstar.Dica.Models;
using BemEstar.Dica.Infrastructure;

namespace BemEstar.Dica.Services
{
    public abstract class BaseService<T> where T : BaseModel
    {
        // Fábrica de conexão fornecida pelo Singleton
        protected readonly IDbConnectionFactory _connectionFactory;

       /// Construtor que injeta a fábrica de conexão via Singleton
        protected BaseService()
        {
            _connectionFactory = DbFactorySingleton.Instance;
        }

        /// CREATE
        public abstract void Create(T model);

        /// DELETE_BY_ID
        public abstract void Delete(int id);

        /// READ_ALL
        public abstract List<T> Read();

        /// READ_BY_ID
        public abstract T ReadById(int id);

        /// UPDATE
        public abstract void Update(T model);
    }
}
