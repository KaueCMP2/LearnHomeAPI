using LearnHomeAPI.Applications.Service;
using LearnHomeAPI.Domains;
using LearnHomeAPI.DTOs.AlunoDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoAlunoController : ControllerBase
    {
        private readonly CursoAlunoervice _service;
        public CursoAlunoController(CursoAlunoervice service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<LerCursoAlunoDto> CursoAluno = _service.Listar();
                return Ok(CursoAluno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("cursoId/{cursoId}/alunoId/{alunoId}")]
        public IActionResult ObterPorId(int cursoId, int alunoId)
        {
            try
            {
                return Ok(_service.ObterPorId(cursoId, alunoId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")] // Indica que recebe dados no formato multpart/from-data
        public IActionResult Adicionar([FromForm] AdicionarCursoAlunoDto cursoAlunoDto)
        {
            try
            {
                _service.Adicionar(cursoAlunoDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("cursoId/{cursoId}/alunoId/{alunoId}")]
        [Consumes("multipart/form-data")] // Indica que recebe dados no formato multpart/from-data
        public IActionResult Atualizar(int cursoId, int alunoId, [FromForm] AtualizarCursoAlunoDto cursoAlunoDto)
        {
            try
            {
                _service.Atualizar(cursoId, alunoId, cursoAlunoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("cursoId/{cursoId}/alunoId/{alunoId}")]
        public IActionResult Remover(int cursoId, int alunoId)
        {
            try
            {
                _service.Remover(cursoId, alunoId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
