using SpeakCore.Application.DTOs.Disciplina;
using SpeakCore.Domain.Entities;

namespace SpeakCore.Application.Services.Implementations
{
    public class DisciplinaService
    {

        private Disciplina MapParaEntidade(DisciplinaCreateDTO dto)
        {
            return new Disciplina
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Ativo = true
            };
        }

        private DisciplinaResponseDTO MapParaResponse(Disciplina disciplina)
        {
            return new DisciplinaResponseDTO
            {
                Id = disciplina.Id,
                Nome = disciplina.Nome,
                Descricao = disciplina.Descricao,
                Ativo = disciplina.Ativo
            };



        }

        private void AplicarAtualizacao(Disciplina disciplina, DisciplinaUpdateDTO dto)
        {
             disciplina.Nome = dto.Nome;
            disciplina.Descricao = dto.Descricao;
            disciplina.Ativo = dto.Ativo;

        }

    }
}
