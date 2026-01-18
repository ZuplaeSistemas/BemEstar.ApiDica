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
    public override int Create(User model)
    {
        model.Password = model.Password.GetHashCode().ToString();
        return base.Create(model);
    }
    public override int Create(User model)
    {
        model.Password = model.Password.GetHashCode().ToString();
        return base.Create(model);
    
    }

    public override void Update(User model)
    {
        User existingUser = ReadById(model.Id);
        if(existingUser != null)
        {
            model.Email = existingUser.Email; // Evita que o email seja alterado
            model.Person_Id = existingUser.Person_Id; //Evita que o Person_Id seja alterado
            model.Password = _passwordHasher.HashPassword(model.Password);
            base.Update(model);
        }
        model.Password = _passwordHasher.HashPassword(model.Password);
        base.Update(model);
    }
}
