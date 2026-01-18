using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using BemEstar.Dica.WebApi.DicaViewModel;
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
           

            List<DicaUserResponseViewModel> listViewModel = new List<DicaUserResponseViewModel>();
            foreach(var u in users)
            {
                DicaUserResponseViewModel duvm = new DicaUserResponseViewModel();
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
           

            List<DicaUserResponseViewModel> listViewModel = new List<DicaUserResponseViewModel>();
            foreach(var u in users)
            {
                DicaUserResponseViewModel duvm = new DicaUserResponseViewModel();
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
        public DicaUserResponseViewModel Get(int id)
        {
<<<<<<< HEAD
            DicaUser user = this._service.ReadById(id);
            DicaUserResponseViewModel duvm = new DicaUserResponseViewModel();
            duvm.Id = user.ID;
            duvm.Email = user.Email;
            duvm.Password = user.Password;
            duvm.Person_Id = user.Person_Id;
            duvm.CreatedAt = user.CreatedAt;
            duvm.Person = this._personService.ReadById(user.Person_Id);

            return duvm;
=======
            DicaUser user = this._service.ReadById(id);
            DicaUserResponseViewModel duvm = new DicaUserResponseViewModel();
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
        public IActionResult Post([FromBody] DicaUserRequestViewModel viewModel)
        {
            User model = new User
            {
                Email = viewModel.Email;
                Password = viewModel.Password;
                Person_Id = viewModel.Person_Id;
            };
            this._service.Create(model);
            return Created();
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserPasswordViewModel model)
        {
            if (id != model.Id)
            {
                throw new ArgumentException("O ID do Objeto DicaUser não é igual ao Id da URL.");
            }
            User userToUpdate = new User();
            userToUpdate.Id = model.Id;
            userToUpdate.Password = model.Password;

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