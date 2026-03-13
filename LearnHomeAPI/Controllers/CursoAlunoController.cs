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
        private readonly CursoAlunoService _service;
        public CursoAlunoController(CursoAlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<LerCursoAlunoDto> cursosAlunos = _service.Listar();
                return Ok(cursosAlunos);
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
                LerCursoAlunoDto cursoAluno = _service.ObterPorId(cursoId, alunoId);
                return Ok(cursoAluno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Adicionar(AdicionarCursoAlunoDto cursoAlunoDto)
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

        [HttpPut]
        public IActionResult Atualizar(int cursoId, int alunoId, AtualizarCursoAlunoDto cursoAlunoDto)
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
