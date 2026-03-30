
namespace SpeakCore.Application.DTOs.Aluno
{
    public class AlunoResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public string CPF { get; set; }
        public DateTime DataCadastro { get; set; }

        public List<MatriculaDTO> Matriculas { get; set; }

    }
}
