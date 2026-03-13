using LearnHomeAPI.Applications.Service;
using LearnHomeAPI.Domains;
using LearnHomeAPI.DTOs.AlunoDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LearnHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrutorController : ControllerBase
    {
        private readonly InstrutorService _service;
        public InstrutorController(InstrutorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<LerInstrutorDto> Instrutores = _service.Listar();
                return Ok(Instrutores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                LerInstrutorDto Instrutor = _service.ObterPorId(id);
                return Ok(Instrutor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("instrutorNome/{nome}")]
        public IActionResult ObterPorId(string nome)
        {
            try
            {
                List<LerInstrutorDto> Instrutor = _service.ObterPorNome(nome);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("instrutorEmail/{email}")]
        public IActionResult ObterPorEmail(string email)
        {
            try
            {
                List<LerInstrutorDto> Instrutor = _service.ObterPorEmail(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<AdicionarInstrutorDto> Adicionar(AdicionarInstrutorDto Instrutor)
        {
            try
            {
                _service.Adicionar(Instrutor);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<AtualizarInstrutorDto> Atualizar(int id, AtualizarInstrutorDto Instrutor)
        {
            try
            {
                _service.Atualizar(id, Instrutor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
