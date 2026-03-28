using SpeakCore.Application.DTOs.Disciplina;

namespace SpeakCore.Application.Services.Interfaces
{
    public interface IDisciplinaService 
    {
        Task<DisciplinaResponseDTO> AdicionarAsync(DisciplinaCreateDTO disciplina);
        Task<DisciplinaResponseDTO?> ObterPorIdAsync(int id);
        Task<List<DisciplinaResponseDTO>> ObterTodasAsync();

        Task<DisciplinaResponseDTO> AtualizarAsync(int id, DisciplinaUpdateDTO dto);
        Task RemoverAsync(int id);

    }
}
