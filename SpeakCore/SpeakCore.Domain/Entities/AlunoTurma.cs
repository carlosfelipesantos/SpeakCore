namespace SpeakCore.Domain.Entities
{
    public class AlunoTurma
    {
        public DateTime DataMatricula { get; set; }
        public bool Ativo { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}
