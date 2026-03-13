using LearnHomeAPI.Applications.Service;
using LearnHomeAPI.DTOs.AlunoDto;
using LearnHomeAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService _service;
        public AlunoController(AlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<LerAlunoDto> alunosDto = _service.Listar();
                return Ok(alunosDto);
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
                LerAlunoDto alunoDto = _service.ObterPorId(id);
                return Ok(alunoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{nome}")]
        public IActionResult ObterPorNome(string nome)
        {
            try
            {
                LerAlunoDto alunoDto = _service.ObterPorNome(nome);
                return Ok(alunoDto);
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
                LerAlunoDto alunoDto = _service.ObterPorEmail(email);
                return Ok(alunoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Adicionar(AdicionarAlunoDto adicionarAluno)
        {
            try
            {
                _service.Adicionar(adicionarAluno);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, AtualizarAlunoDto alunoDto)
        {
            try
            {
                _service.Atualizar(id, alunoDto);
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