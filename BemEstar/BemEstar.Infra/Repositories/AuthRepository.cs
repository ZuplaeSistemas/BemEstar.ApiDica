using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemEstar.Dica.Infra.Repositories
{
    public class AuthRepository
    {
        public bool Login(string email, string password)
        { 
            using IDbConnection connection = this._dbConnectionFactory.GetConnection();
            string commandText = $"SELECT * FROM user where email = @email and password = @password";

            using IDbCommand loginCommand = CreateCommand(connection, commandText);
            
            using IDbCommand loginCommand = connection.CreateCommand();
            loginCommand.CommandText = commandText;
            
            IDbDataParameter parameter = loginCommand.CreateParameter();
            parameter.ParameterName = "email";
            parameter.Value = email;
            loginCommand.Parameters.Add(emailParameter);

            IDbDataParameter parameter = loginCommandCommand.CreateParameter();
            parameter.ParameterName = "password";
            parameter.Value = password;
            loginCommand.Parameters.Add(passwordParameter);

            using IDataReader dataReader = loginCommand.ExecuteReader();

            bool retorno = dataReader.Read();
            return retorno;
        }
    }
}
