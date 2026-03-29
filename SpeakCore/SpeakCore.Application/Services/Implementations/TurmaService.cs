using SpeakCore.Application.DTOs.Turma;
using SpeakCore.Application.Services.Interfaces;
using SpeakCore.Domain.Entities;
using SpeakCore.Domain.Interfaces;
using SpeakCore.Infrastructure.Repositories;

namespace SpeakCore.Application.Services.Implementations
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly IProfessorRepository _professorRepository;

        public TurmaService(ITurmaRepository turmaRepository, IDisciplinaRepository disciplinaRepository, IProfessorRepository professorRepository)
        {
            _turmaRepository = turmaRepository;
            _disciplinaRepository = disciplinaRepository;
            _professorRepository = professorRepository;
        }

        public async Task<TurmaResponseDTO> AdicionarAsync(TurmaCreateDTO dto)
        {
            if (dto.CapacidadeMax <= 0)
                throw new ArgumentException("Capacidade deve ser maior que zero.");

            var disciplina = await _disciplinaRepository.ObterPorIdAsync(dto.DisciplinaId);
            if (disciplina == null)
                throw new KeyNotFoundException("Disciplina não encontrada.");

            var professor = await _professorRepository.ObterPorIdAsync(dto.ProfessorId);
            if (professor == null)
                throw new KeyNotFoundException("Professor não encontrado.");

            if (dto.DataFim <= dto.DataInicio)
                throw new ArgumentException("Data de fim deve ser maior que a data de início.");

            var turma = MapParaEntidade(dto);
            await _turmaRepository.AdicionarAsync(turma);
            return MapParaResponse(turma);
        }

        public async Task<TurmaResponseDTO?> ObterPorIdAsync(int id)
        {
            var turma = await _turmaRepository.ObterPorIdAsync(id);
            if (turma == null)
                throw new KeyNotFoundException("Turma nao encontrada");

            return MapParaResponse(turma);
        }

        public async Task<List<TurmaResponseDTO>> ObterTodosAsync()
        {
            var turmas = await _turmaRepository.ObterTodosAsync();
            return turmas.Select(t => MapParaResponse(t)).ToList();
        }

        public async Task<TurmaResponseDTO> AtualizarAsync(int id, TurmaUpdateDTO dto)
        {
            var turma = await _turmaRepository.ObterPorIdAsync(id);
            if (turma == null)
                throw new KeyNotFoundException("Turma nao encontrada");

            var professor = await _professorRepository.ObterPorIdAsync(dto.ProfessorId);
            if (professor == null)
                throw new KeyNotFoundException("Professor não encontrado.");

            if (dto.DataFim <= turma.DataInicio)
                throw new ArgumentException("Data de fim deve ser maior que a data de início.");

            AtualizarTurma(turma, dto);

            await _turmaRepository.AtualizarAsync(turma);

           return MapParaResponse(turma);
        }

        public async Task RemoverAsync(int id)
        {
            var turma = await _turmaRepository.ObterPorIdAsync(id);
            if (turma == null)
                throw new KeyNotFoundException("Turma nao encontrada");

            if (await _turmaRepository.PossuiAlunosAsync(id))
                throw new InvalidOperationException("Não é possível excluir a turma, pois possui alunos.");

            await _turmaRepository.RemoverAsync(turma);                     
        }

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
            turma.ProfessorId = dto.ProfessorId;

        }

    }
}
