using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;

namespace BemEstar.Dica.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DicaController : ControllerBase
    {
        private readonly DicaService _service;

        /// Injeção de Dependência automática pelo sistema do ASP.NET Core
        public DicaController(DicaService service)
        {
            _service = service;
        }

        /// READ_ALL
        [HttpGet]
        public ActionResult<List<DicaModel>> Get()
        {
            return Ok(_service.Read());
        }

        /// READ_BY_ID
        [HttpGet("{id}")]
        public ActionResult<DicaModel> Get(int id)
        {
            var dica = _service.ReadById(id);
            if (dica == null) return NotFound();
            return Ok(dica);
        }

        /// CREATE
        [HttpPost]
        public IActionResult Post([FromBody] DicaModel model)
        {
            _service.Create(model);
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        /// UPDATE
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DicaModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("O ID da URL não corresponde ao ID do modelo.");
            }

            _service.Update(model);
            return NoContent();
        }

        /// DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}
