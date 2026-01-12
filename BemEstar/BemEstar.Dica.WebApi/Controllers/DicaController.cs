using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BemEstar.Dica.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]

    public class DicaController : ControllerBase
    {
        private DicaService _service;
        public DicaController(DicaService service)
        {
            _service = service;                            
        }

        [HttpGet]
        public List<DicaModel> Get()
        {
            return this._service.Read();
        }


        [HttpGet("{id}")]
        public DicaModel Get(int id)
        {
            return this._service.ReadById(id);
        }

        [HttpGet("exist/{id}")]
        public bool Exists(int id)
        {
            return this._service.Exists(id);
        }


        [HttpPost]
        public void Post([FromBody] DicaModel model)
        {
            this._service.Create(model);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DicaModel model)
        {
            if (id != model.Id)
            {
                throw new ArgumentException("O ID do Objeto Person não é igual ao Id da URL.");
            }
            this._service.Update(model);
        }

        [Authorize(Roles = "Admin")]
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