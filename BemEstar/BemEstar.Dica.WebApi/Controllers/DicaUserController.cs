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
        public IActionResult Get()
        {
            List<USer> model = this._service.Read();
           
            List<DicaUserResponseViewModel> response = new List<DicaUserResponseViewModel>();
            foreach(var u in model)
            {
                DicaUserResponseViewModel userResponse = new DicaUserResponseViewModel();
                userResponse.Id = u.Id;
                userResponse.Email = u.Email;
                userResponse.Password = u.Password;
                userResponse.Person_Id = u.Person_Id;
                userResponse.CreatedAt = u.CreatedAt;
                userResponse.Person = this._personService.ReadById(u.Person_Id);
                response.Add(userResponse);
            }
            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DicaUser model = this._service.ReadById(id);
            DicaUserGetResponse response = new DicaUserGetResponse
            {
                Id = model.Id;
                Email = model.Email;
                Password = model.Password;
                Person_Id = model.Person_Id;
                CreatedAt = model.CreatedAt;
                Person = this._personService.ReadById(user.Person_Id);
            };
            return Ok(response);
        }

        [HttpGet("exist/{id}")]
        public IActionResult Exists(int id)
        {
            ExistResponse response = new ExistResponse
            {
                Id = id;
                Exist = this.service.Exists(id);
            }; 
            return Ok(response);
        }


        [HttpPost]
        public IActionResult Post([FromBody] DicaUserPostRequest viewModel)
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
        public IActionResult Put(int id, [FromBody] UserPutRequest request)
        {
            User model = new User();
            model.Id = id;
            model.Password = request.Password;

            this._service.Update(model);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try{
                this._service.Delete(id);
                StatusCodeResult result = new StatusCodeResult(204);
                return NoContent();
            }
            catch (Exception ex)
            {
                StatusCodeResult result = new StatusCodeResult(500);
                return result;
            }
           
        }

    }
    
}