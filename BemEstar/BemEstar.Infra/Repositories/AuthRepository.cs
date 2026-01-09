using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BemEstar.Dica.Infra.Db;

namespace BemEstar.Dica.Infra.Repositories
{
    public class AuthRepository
    {
        private IDbConnectionFactory _dbConnectionFactory;
        public AuthRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }
        public User GetUserByEmail(string email)
        { 
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();
            string commandText = $"SELECT * FROM user where email = @email";
            
            using IDbCommand loginCommand = connection.CreateCommand();
            loginCommand.CommandText = commandText;
            
            IDbDataParameter parameter = loginCommand.CreateParameter();
            parameter.ParameterName = "email";
            parameter.Value = email;
            loginCommand.Parameters.Add(emailParameter);

            User model = null;
            while (dataReader.Read())
            {
               model = new User();
               model.Email = dataReader["email"].ToString();
               model.Password = dataReader["password"].ToString();
               model.Id = Convert.ToInt32(DataReader["id"]);
               model.Dica_Id = Convert.ToInt32(DataReader["dica_id"]);
            }
            return model;
        }
    }
}
