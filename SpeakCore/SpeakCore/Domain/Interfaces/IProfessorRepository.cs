using SpeakCore.Domain.Entities;

namespace SpeakCore.Domain.Interfaces
{
    public interface IProfessorRepository
    {
        Task AdicionarAsync(Professor professor);

        Task<Professor?> ObterPorIdAsync(int id);
        Task<List<Professor>> ObterTodosAsync();


        Task AtualizarAsync(Professor professor);
        Task RemoverAsync(Professor professor);

        Task<bool> EmailExisteAsync(string email);

        Task<bool> PossuiTurmasAsync(int professorId);

    }
}
