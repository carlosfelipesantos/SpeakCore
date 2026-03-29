using SpeakCore.Domain.Entities;

namespace SpeakCore.Domain.Interfaces
{
    public interface IDisciplinaRepository
    {
        Task AdicionarAsync(Disciplina disciplina);

        Task <Disciplina?> ObterPorIdAsync(int id);
        Task <List<Disciplina>>ObterTodasAsync();
        Task<bool> NomeExisteAsync(string nome);

        Task AtualizarAsync (Disciplina disciplina);
        Task RemoverAsync (Disciplina disciplina);
    }
}
