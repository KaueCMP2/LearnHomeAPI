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
        private readonly Alunoervice _service;
        public AlunoController(Alunoervice service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            try
            {
                List<LerAlunoDto> AlunoDto = _service.Listar();
                return Ok(AlunoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult ObterPorId(int id)
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

        [HttpGet("nomeAluno/{nome}")]
        public ActionResult ObterPorNome(string nome)
        {
            try
            {
                List<LerAlunoDto> alunoDto = _service.ObterPorNome(nome);
                return Ok(alunoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("emailAluno/{email}")]
        [Consumes("multipart/form-data")]
        public ActionResult ObterPorEmail(string email)
        {
            try
            {
                List<LerAlunoDto> alunoDto = _service.ObterPorEmail(email);
                return Ok(alunoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public ActionResult Adicionar([FromForm] AdicionarAlunoDto adicionarAluno)
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
        [Consumes("multipart/form-data")] // Indica que recebe dados no formato multpart/from-data
        public ActionResult Atualizar(int id, [FromForm] AtualizarAlunoDto alunoDto)
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
        public ActionResult Remover(int id)
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