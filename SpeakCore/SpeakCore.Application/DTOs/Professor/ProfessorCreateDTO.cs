using System.ComponentModel.DataAnnotations;

namespace SpeakCore.Application.DTOs.Professor
{
    public class ProfessorCreateDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Especialidade { get; set; }
  
    }
}
