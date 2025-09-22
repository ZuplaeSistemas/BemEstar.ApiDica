using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
namespace BemEstar.Dica.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DicaController : ControllerBase
    {
        private DicaService _service = new DicaService();

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


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this._service.Delete(id);
        }
    }
}