using Microsoft.AspNetCore.Mvc;
using SpeakCore.Application.DTOs.Turma;
using SpeakCore.Application.Services.Interfaces;

namespace SpeakCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaService _turmaService;
        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var turma = await _turmaService.ObterPorIdAsync(id);
                return Ok(turma);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var turmas = await _turmaService.ObterTodosAsync();
            return Ok(turmas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TurmaCreateDTO dto)
        {
            try
            {
                var turma = await _turmaService.AdicionarAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = turma.Id }, turma);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]TurmaUpdateDTO dto)
        {
            try
            {
                var turmaAtualizada = await _turmaService.AtualizarAsync(id, dto);
                return Ok(turmaAtualizada);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _turmaService.RemoverAsync(id);
                return NoContent();
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
