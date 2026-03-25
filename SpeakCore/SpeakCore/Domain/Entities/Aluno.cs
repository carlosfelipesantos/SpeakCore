namespace SpeakCore.Domain.Entities
{
    public class Aluno
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
         public bool Ativo { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }

        public ICollection<AlunoTurma> AlunoTurmas { get; set; }

    }
}
