using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemEstar.Dica.Models;
{
    public class DicaUser : BaseModel //Herança
    {
 
        public string E-mail {get; set;}

        public string Password {get; set;}

        public int Person_Id {get; set;}

       
    }
}