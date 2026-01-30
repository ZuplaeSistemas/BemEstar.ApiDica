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
        public IActionResult Get()
        {
            List<DicaModel> model = this._service.Read();
            List<DicaGetResponse> response = new List<DicaGetResponse>();
            foreach(var item in model)
            {
                DicaGetResponse dicaResponse = new DicaGetResponse
                {
                    Titulo = item.Titulo;
                    Categoria = item.Categoria;
                    Descricao = item.Descricao;
                };
                response.Add(dicaResponse);
            }
            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DicaModel model = this._service.ReadById(id);
            DicaGetResponse dicaResponse = new DicaGetResponse
            {
                Titulo = model.Titulo;
                Categoria = model.Categoria;
                Descricao = model.Descricao;
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
        public IActionResult Post([FromBody] DicaPostRequest request)
        {
            DicaModel model = new DicaModel
            {
                Titulo = request.Titulo;
                Categoria = request.Categoria;
                Descricao = request.Descricao;
            };
            this._service.Create(model);

            return Created(); //Retorna 204 - resposta mais adequada.
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DicaPostRequest request)
        {
            DicaModel model = new DicaModel
            {
                Id = id;
                Titulo = request.Titulo;
                Categoria = request.Categoria;
                Descricao = request.Descricao;
            };
            this._service.Update(model);
            return NoContent(); //Retorno padrão do put em que não há resposta.
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