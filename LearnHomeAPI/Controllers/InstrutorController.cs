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
                List<LerInstrutorDto> instrutores = _service.Listar();
                return Ok(instrutores);
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
                LerInstrutorDto instrutor = _service.ObterPorId(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{nome}")]
        public IActionResult ObterPorId(string nome)
        {
            try
            {
                LerInstrutorDto instrutor = _service.ObterPorNome(nome);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{email}")]
        public IActionResult ObterPorEmail(string email)
        {
            try
            {
                LerInstrutorDto instrutor = _service.ObterPorEmail(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Adicionar(AdicionarInstrutorDto instrutor)
        {
            try
            {
                _service.Adicionar(instrutor);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, AtualizarInstrutorDto instrutor)
        {
            try
            {
                _service.Atualizar(id, instrutor);
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
