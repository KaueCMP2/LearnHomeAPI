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
        private readonly Cursoervice _service;
        public CursoController(Cursoervice service)
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

        [HttpGet("CursoId/{id}")]
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

        [HttpGet("CursoNome/{nome}")]
        public IActionResult ObterPorId(string nome)
        {
            try
            {
                List<LerCursoDto> cursoDto = _service.ObterPorNome(nome);
                return Ok(cursoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")] // Indica que recebe dados no formato multpart/from-data
        public IActionResult Adicionar([FromForm] AdicionarCursoDto cursoDto)
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

        [HttpPut("CursoId/{cursoId}")]
        [Consumes("multipart/form-data")] // Indica que recebe dados no formato multpart/from-data
        public IActionResult Atualizar(int cursoId, [FromForm] AtualizarCursoDto cursoDto)
        {
            try
            {
                _service.Atualizar(cursoId, cursoDto);
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
