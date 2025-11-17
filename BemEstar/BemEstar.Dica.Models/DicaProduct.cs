using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemEstar.Dica.Models;
{
    public class DicaProduct : BaseModel
    {
        public string Name {get; set;}

        public string Description {get; set;}

        public string Ean {get; set;}

        public decimal Value {get; set;}
    }
}