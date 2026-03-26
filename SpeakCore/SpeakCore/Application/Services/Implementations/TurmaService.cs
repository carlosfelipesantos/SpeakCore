using SpeakCore.Application.DTOs.Turma;
using SpeakCore.Domain.Entities;

namespace SpeakCore.Application.Services.Implementations
{
    public class TurmaService
    {
        private Turma MapParaEntidade(TurmaCreateDTO dto)
        {
            return new Turma
            {
                Numero = dto.Numero,
                AnoLetivo = dto.AnoLetivo,
                CapacidadeMax = dto.CapacidadeMax,
                Nivel = dto.Nivel,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                DisciplinaId = dto.DisciplinaId,
                ProfessorId = dto.ProfessorId
            };
        }

        private TurmaResponseDTO MapParaResponse(Turma turma)
        {
            return new TurmaResponseDTO
            {
                Id = turma.Id,
                Numero = turma.Numero,
                AnoLetivo = turma.AnoLetivo,
                CapacidadeMax = turma.CapacidadeMax,
                Nivel = turma.Nivel,
                DataInicio = turma.DataInicio,
                DataFim = turma.DataFim,
                DisciplinaId = turma.DisciplinaId,
                ProfessorId = turma.ProfessorId
            };
        }

        private void AtualizarTurma(Turma turma, TurmaUpdateDTO dto)
        {

            turma.Numero = dto.Numero;
            turma.AnoLetivo = dto.AnoLetivo;
            turma.Nivel = dto.Nivel;
            turma.DataFim = dto.DataFim;

        }
    }
}
