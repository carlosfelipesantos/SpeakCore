using SpeakCore.Application.DTOs.Aluno;
using SpeakCore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SpeakCore.Application.Services.Implementations
{
    public class AlunoService
    {
        private Aluno MapParaEntidade(AlunoCreateDTO dto)
        {
            return new Aluno
            {
                CPF = dto.CPF,
                Nome = dto.Nome,
                Email = dto.Email,
                DataNascimento = dto.DataNascimento,

                AlunoTurmas = dto.TurmasIds.Select(turma => new AlunoTurma()
                {
                    TurmaId = turma,
                    DataMatricula = DateTime.Now
                }).ToList()
            };

        }

        private AlunoResponseDTO MapParaResponse(Aluno aluno)
        {
            return new AlunoResponseDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Email = aluno.Email,
                Ativo = aluno.Ativo,
                DataCadastro = aluno.DataCadastro,

                Turmas = aluno.AlunoTurmas
                .Select(at => at.TurmaId)
                .ToList()

            };



        }


        private void AtualizarAluno(Aluno aluno, AlunoUpdateDTO dto)
        {
            aluno.Nome = dto.Nome;
            aluno.Email = dto.Email;


        }


    }
}
