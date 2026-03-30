

using System.ComponentModel.DataAnnotations;

namespace SpeakCore.Application.DTOs.Aluno
{
    public class AlunoUpdateDTO
    {

        public string Nome { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string CPF { get; set; }
        public List<int> TurmasIds { get; set; }

    }
}
