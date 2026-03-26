using SpeakCore.Application.DTOs.Professor;
using SpeakCore.Domain.Entities;

namespace SpeakCore.Application.Services.Implementations
{
    public class ProfessorService
    {
        private Professor MapParaEntidade(ProfessorCreateDTO dto)
        {
            return new Professor {
            Nome = dto.Nome,
            Email = dto.Email,
            Especialidade = dto.Especialidade

            
            };
        }

        private ProfessorResponseDTO MapParaResponseDTO(Professor professor) 
        {
            return new ProfessorResponseDTO
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Email = professor.Email,
                Especialidade = professor.Especialidade,
                Ativo = professor.Ativo
            };
        }

        private void AtualizarProfessor(Professor professor, ProfessorUpdateDTO dto)
        {
            professor.Nome = dto.Nome;
            professor.Email = dto.Email;
            professor.Especialidade = dto.Especialidade;
        }

    }
}
