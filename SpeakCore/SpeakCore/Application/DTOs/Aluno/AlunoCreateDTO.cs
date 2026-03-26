
using System.ComponentModel.DataAnnotations;

namespace SpeakCore.Application.DTOs.Aluno
{
    public class AlunoCreateDTO
    {
        [Required]
        public string CPF { get; set; } = string.Empty;
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public DateTime DataNascimento { get; set; }

        [MinLength(1, ErrorMessage = "O aluno deve estar matriculado em pelo menos uma turma.")]
        public List<int> TurmasIds { get; set; }

    }
}
