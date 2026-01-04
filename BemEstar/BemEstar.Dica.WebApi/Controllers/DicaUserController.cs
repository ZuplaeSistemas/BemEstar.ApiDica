using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
namespace BemEstar.Dica.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DicaUserController : ControllerBase
    {
        private readonly DicaUserService _service;
        private readonly PersonService _personService;
        public DicaUserController(DicaUserService service, PersonService personService)
        {
            _service = service;
            _personService = personService;
        }
        [HttpGet]
        public List<DicaUserViewModel> Get()
        {
<<<<<<< HEAD
            List<USer> users = this._service.Read();
           

            List<DicaUserViewModel> listViewModel = new List<UserViewModel>();
            foreach(var u in users)
            {
                DicaUserViewModel duvm = new DicaUserViewModel();
                duvm.Id = u.Id;
                duvm.Email = u.Email;
                duvm.Password = u.Password;
                duvm.Person_Id = u.Person_Id;
                duvm.CreatedAt = u.CreatedAt;
                duvm.Person = this._personService.ReadById(u.Person_Id);
                listViewModel.Add(duvm);
            }
            return listViewModel;
=======
            List<USer> users = this._service.Read();
           

            List<DicaUserViewModel> listViewModel = new List<UserViewModel>();
            foreach(var u in users)
            {
                DicaUserViewModel duvm = new DicaUserViewModel();
                duvm.Id = u.Id;
                duvm.Email = u.Email;
        
                duvm.CreatedAt = u.CreatedAt;
                duvm.Person = this._personService.ReadById(u.Person_Id);
                listViewModel.Add(duvm);
            }
            return listViewModel;
>>>>>>> bb02fbf4863774235fd56894b698f9377f2ad563
        }


        [HttpGet("{id}")]
        public DicaUserViewModel Get(int id)
        {
<<<<<<< HEAD
            DicaUser user = this._service.ReadById(id);
            DicaUserViewModel duvm = new DicaUserViewModel();
            duvm.Id = user.ID;
            duvm.Email = user.Email;
            duvm.Password = user.Password;
            duvm.Person_Id = user.Person_Id;
            duvm.CreatedAt = user.CreatedAt;
            duvm.Person = this._personService.ReadById(user.Person_Id);

            return duvm;
=======
            DicaUser user = this._service.ReadById(id);
            DicaUserViewModel duvm = new DicaUserViewModel();
            duvm.Id = user.ID;
            duvm.Email = user.Email;
            duvm.Password = user.Password;
            duvm.CreatedAt = user.CreatedAt;
            duvm.Person = this._personService.ReadById(user.Person_Id);

            return duvm;
>>>>>>> bb02fbf4863774235fd56894b698f9377f2ad563
        }

        [HttpGet("exist/{id}")]
        public bool Exists(int id)
        {
            return this._service.Exists(id);
        }


        [HttpPost]
        public void Post([FromBody] DicaUser model)
        {
            this._service.Create(model);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DicaUser model)
        {
            if (id != model.Id)
            {
                throw new ArgumentException("O ID do Objeto DicaUser não é igual ao Id da URL.");
            }
            this._service.Update(model);
        }


        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            try{
                this._service.Delete(id);
                StatusCodeResult result = new StatusCodeResult(204);
                return result;
            }
            catch (Exception ex)
            {
                StatusCodeResult result = new StatusCodeResult(500);
                return result;
            }
           
        }

    }
    
}