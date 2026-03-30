using SpeakCore.Domain.Entities;

namespace SpeakCore.Domain.Interfaces
{
    public interface IAlunoRepository
    {
        Task AdicionarAsync(Aluno aluno);
        
        Task<Aluno?> ObterPorIdAsync(int id);
        Task<List<Aluno>> ObterTodosAsync();

        Task AtualizarAsync(Aluno aluno);
        Task RemoverAsync(Aluno aluno);
  
        Task<bool> EmailExisteAsync(string email);
        Task<bool> CpfExisteAsync(string cpf);

        Task<bool> PossuiTurmasAsync(int alunoId);
        Task<bool> PossuiTurmasAtivasAsync(int alunoId);
    }
}
