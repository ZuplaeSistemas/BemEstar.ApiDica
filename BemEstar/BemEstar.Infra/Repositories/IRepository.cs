using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemEstar.Dica.Infra.Repositories
{
    public interface IRepository<T>
    {
        int Create(T entity);

        List<T> Read();
        T REadById(int id);
        void Update(T entity);
        void Delete(int id);
        bool Exists(int id);
    }
}
