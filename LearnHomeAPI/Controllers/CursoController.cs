using LearnHomeAPI.Applications.Service;
using LearnHomeAPI.DTOs.AlunoDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly CursoService _service;
        public CursoController(CursoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<LerCursoDto> cursoDto = _service.Listar();
                return Ok(cursoDto);
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
                LerCursoDto cursoDto = _service.ObterPorId(id);
                return Ok(cursoDto);
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
                LerCursoDto cursoDto = _service.ObterPorNome(nome);
                return Ok(cursoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Adicionar(AdicionarCursoDto cursoDto)
        {
            try
            {
                _service.Adicionar(cursoDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("cursoId/{cursoId}/instrutorId/{instrutorId}")]
        public IActionResult Atualizar(int cursoId, int instrutorId, AtualizarCursoDto cursoDto)
        {
            try
            {
                _service.Atualizar(cursoId, instrutorId, cursoDto);
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
