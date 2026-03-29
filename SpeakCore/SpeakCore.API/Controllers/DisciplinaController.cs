using Microsoft.AspNetCore.Mvc;
using SpeakCore.Application.DTOs.Disciplina;
using SpeakCore.Application.Services.Interfaces;

namespace SpeakCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly IDisciplinaService _disciplinaService;

        public DisciplinaController(IDisciplinaService disciplinaService)
        {
            _disciplinaService = disciplinaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
              var disciplina =  await _disciplinaService.ObterPorIdAsync(id);
                return Ok(disciplina);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var disciplinas = await _disciplinaService.ObterTodasAsync();
            return Ok(disciplinas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DisciplinaCreateDTO dto)
        {
            try
            {
                var disciplina = await _disciplinaService.AdicionarAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = disciplina.Id }, disciplina);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DisciplinaUpdateDTO dto)
        {
            try
            {
                var disciplinaAtualizada = await _disciplinaService.AtualizarAsync(id, dto);
                return Ok(disciplinaAtualizada);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _disciplinaService.RemoverAsync(id);
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