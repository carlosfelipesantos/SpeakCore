
namespace SpeakCore.Application.DTOs.Aluno
{
    public class AlunoCreateDTO
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }

        public List<int> TurmasIds { get; set; }

    }
}
