using SpeakCore.Application.DTOs.Aluno;
using SpeakCore.Application.DTOs.Professor;

namespace SpeakCore.Application.Services.Interfaces
{
    public interface IProfessorService
    {
        Task<ProfessorResponseDTO> AdicionarAsync(ProfessorCreateDTO dto);
        Task<ProfessorResponseDTO?> ObterPorIdAsync(int id);
        Task <List<ProfessorResponseDTO>> ObterTodosAsync();
        Task<ProfessorResponseDTO> AtualizarAsync(int id, ProfessorUpdateDTO dto);
        Task RemoverAsync(int id);

    }
}
