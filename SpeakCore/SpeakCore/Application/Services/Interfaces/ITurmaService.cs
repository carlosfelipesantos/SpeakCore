using SpeakCore.Application.DTOs.Turma;

namespace SpeakCore.Application.Services.Interfaces
{
    public interface ITurmaService
    {
        Task<TurmaResponseDTO> AdicionarAsync(TurmaCreateDTO dto);
        Task<TurmaResponseDTO?> ObterPorIdAsync(int id);
        Task <List<TurmaResponseDTO>> ObterTodosAsync();
        Task<TurmaResponseDTO> AtualizarAsync(int id, TurmaUpdateDTO dto);
        Task RemoverAsync(int id);

    }
}
