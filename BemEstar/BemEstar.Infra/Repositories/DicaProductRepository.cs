using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemEstar.Dica.Infra.Repositories
{
    public class DicaProductRepository : RepositoryInMemory<DicaProduct>
    {
        public DicaProductRepository(IDbConnectionFactory factory)
        {
            
         }
    }
}
