using Microsoft.AspNetCore.Mvc;
using SpeakCore.Application.DTOs.Aluno;
using SpeakCore.Application.Services.Interfaces;

namespace SpeakCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController :ControllerBase
    {
        private readonly IAlunoService _alunoService;
        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpPatch("{alunoId}/turmas/{turmaId}/status")]
        public async Task<IActionResult> AtualizarStatusMatricula(int alunoId, int turmaId, [FromBody] bool ativo)
        {
            try
            {
                await _alunoService.AtualizarStatusMatriculaAsync(alunoId, turmaId, ativo);
                return NoContent(); // 204 - sucesso sem conteúdo
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var aluno = await _alunoService.ObterPorIdAsync(id);
                return Ok(aluno);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Todos")]
        public async Task<IActionResult> GetAll()
        {
            var alunos = await _alunoService.ObterTodosAsync();
            return Ok(alunos);    
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlunoCreateDTO dto)
        {
            try
            {
                var aluno = await _alunoService.AdicionarAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = aluno.Id }, aluno);
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
        public async Task<IActionResult> Update(int id, [FromBody] AlunoUpdateDTO dto)
        {
            try
            {
                var alunoAtualizado = await _alunoService.AtualizarAsync(id, dto);
                return Ok(alunoAtualizado);
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
                await _alunoService.RemoverAsync(id);
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

