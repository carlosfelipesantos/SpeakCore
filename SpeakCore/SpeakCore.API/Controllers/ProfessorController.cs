using Microsoft.AspNetCore.Mvc;
using SpeakCore.Application.DTOs.Professor;
using SpeakCore.Application.Services.Interfaces;

namespace SpeakCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _professorService;
        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var professor = await _professorService.ObterPorIdAsync(id);
                return Ok(professor);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professores = await _professorService.ObterTodosAsync();
            return Ok(professores);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ProfessorCreateDTO dto)
        {
            try
            {
                var professor = await _professorService.AdicionarAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = professor.Id }, professor);
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
        public async Task<IActionResult> Update(int id, [FromBody] ProfessorUpdateDTO dto)
        {
            try
            {
                var professorAtualizado = await _professorService.AtualizarAsync(id, dto);
                return Ok(professorAtualizado);
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
                await _professorService.RemoverAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}



