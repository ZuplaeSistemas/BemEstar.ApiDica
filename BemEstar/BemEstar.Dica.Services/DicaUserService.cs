using BemEstar.Dica.Infra.Db;
using BemEstar.Dica.Infra.Repositories;
using BemEstar.Dica.Models;
using Microsoft.AspNet.Identity;

namespace BemEstar.Dica.Services;

public class DicaUserService : Service<DicaUser>
{
    private readonly PasswordHasher<User> _passwordHasher;
    public DicaUserService(DicaUserRepository repository) : base(repository)
    {

    }
<<<<<<< HEAD
    public override int Create(User model)
    {
        model.Password = model.Password.GetHashCode().ToString();
        return base.Create(model);
    }
=======
    public override int Create(User model)
    {
        model.Password = model.Password.GetHashCode().ToString();
        return base.Create(model);
    }
    public bool Login(string email, string password)
    {
        base.Read().foreach(user =>
        {
            if(user.Email == email)
            {
                var result = _passwordHasher.VerifyHashedPassword(user.Password, password)
                if(result == PasswordVerificationResult.Sucess)
                {
                    throw new Exception("Login sucessful");
                }
            }
            else
            {
                throw new Exception("Invalid password or email");
            }
>>>>>>> bb02fbf4863774235fd56894b698f9377f2ad563

        });
    }

}
