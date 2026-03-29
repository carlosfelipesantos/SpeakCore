using SpeakCore.Application.DTOs.Aluno;

namespace SpeakCore.Application.Services.Interfaces
{
    public interface IAlunoService
    {
        Task<AlunoResponseDTO> AdicionarAsync(AlunoCreateDTO dto);
        Task<AlunoResponseDTO?> ObterPorIdAsync(int id);
        Task<List<AlunoResponseDTO>> ObterTodosAsync();
        Task<AlunoResponseDTO> AtualizarAsync(int id, AlunoUpdateDTO dto);
        Task RemoverAsync(int id);

    }
}
