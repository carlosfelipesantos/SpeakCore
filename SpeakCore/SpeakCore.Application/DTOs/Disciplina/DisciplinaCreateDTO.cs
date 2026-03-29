using System.ComponentModel.DataAnnotations;

namespace SpeakCore.Application.DTOs.Disciplina
{
    public class DisciplinaCreateDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
    
    }
}
