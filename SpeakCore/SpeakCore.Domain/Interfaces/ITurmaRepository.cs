using SpeakCore.Domain.Entities;

namespace SpeakCore.Domain.Interfaces
{
    public interface ITurmaRepository
    {
        Task AdicionarAsync (Turma turma);
        
        Task<Turma?> ObterPorIdAsync (int id);
        Task<List<Turma>> ObterTodosAsync ();

        Task AtualizarAsync (Turma turma);
        Task RemoverAsync (Turma turma);

        Task<bool> ExisteTurmaPorDisciplinaAsync(int disciplinaId);

        Task<int> ObterQuantidadeAlunosAsync(int turmaId);
        Task<bool> PossuiAlunosAsync(int turmaId);

    }
}
