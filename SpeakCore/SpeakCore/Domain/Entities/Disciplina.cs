namespace SpeakCore.Domain.Entities
{
    public class Disciplina
    {
        public int Id{ get; set; }
        public string Nome { get; set; }
        public string? Descricacao{ get; set; }
        public bool Ativo { get; set; }
    }
}
