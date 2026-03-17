using LearnHomeAPI.Applications.Service;
using LearnHomeAPI.DTOs.AlunoDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace LearnHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaEspecializacaoController : ControllerBase
    {
        private readonly AreaEspecializacaoervice _service;
        public AreaEspecializacaoController(AreaEspecializacaoervice service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<LerAreaEspecializacaoDto> areasDto = _service.Listar();
                return Ok(areasDto);
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
                LerAreaEspecializacaoDto areaDto = _service.ObterPorId(id);
                return Ok(areaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("nomeAreaEspecializacao/{nome}")]
        public IActionResult ObterPorNome(string nome)
        {
            try
            {
                List<LerAreaEspecializacaoDto> areaDto = _service.ObterPorNome(nome);
                return Ok(areaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
