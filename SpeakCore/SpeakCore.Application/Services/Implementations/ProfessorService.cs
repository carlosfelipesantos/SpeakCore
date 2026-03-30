using SpeakCore.Application.DTOs.Professor;
using SpeakCore.Application.Services.Interfaces;
using SpeakCore.Domain.Entities;
using SpeakCore.Domain.Interfaces;


namespace SpeakCore.Application.Services.Implementations
{
    public class ProfessorService: IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        public ProfessorService(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<ProfessorResponseDTO> AdicionarAsync(ProfessorCreateDTO dto)
        {
           

            if (await _professorRepository.EmailExisteAsync(dto.Email))
                throw new ArgumentException("Email já cadastrado.");


            var professor = MapParaEntidade(dto);

            await _professorRepository.AdicionarAsync(professor);
            return MapParaResponse(professor);
        }

        public async Task<ProfessorResponseDTO?> ObterPorIdAsync(int id)
        {
            var professor = await _professorRepository.ObterPorIdAsync(id);

            if (professor == null)
                throw new KeyNotFoundException("Professor nao encontrado");
            return MapParaResponse(professor);
        }

        public async Task<List<ProfessorResponseDTO>> ObterTodosAsync()
        {
            var professores = await _professorRepository.ObterTodosAsync();
            return professores.Select(p => MapParaResponse(p)).ToList();
        }

        public async Task<ProfessorResponseDTO> AtualizarAsync(int id, ProfessorUpdateDTO dto)
        {
            var professor = await _professorRepository.ObterPorIdAsync(id);

       
            if (professor == null)
                throw new KeyNotFoundException("Professor nao encontrado");

            if (!dto.Email.Contains("@"))
                throw new ArgumentException("Email inválido.");


            if (professor.Email != dto.Email &&
                await _professorRepository.EmailExisteAsync(dto.Email))
            {
                throw new ArgumentException("Email já cadastrado.");
            }

            AtualizarProfessor(professor, dto);
            await _professorRepository.AtualizarAsync(professor);

            return MapParaResponse(professor);
        }

        public async Task RemoverAsync(int id)
        {
            var professor = await _professorRepository.ObterPorIdAsync(id);

            if (professor == null)
                throw new KeyNotFoundException("Professor nao encontrado");

            if (await _professorRepository.PossuiTurmasAsync(id))
                throw new InvalidOperationException("Professor possui turmas vinculadas.");


            await _professorRepository.RemoverAsync(professor);
        }

        private Professor MapParaEntidade(ProfessorCreateDTO dto)
        {
            return new Professor {
            Nome = dto.Nome,
            Email = dto.Email,
            Especialidade = dto.Especialidade,
            Ativo = true

            };
        }

        private ProfessorResponseDTO MapParaResponse(Professor professor) 
        {
            return new ProfessorResponseDTO
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Email = professor.Email,
                Especialidade = professor.Especialidade,
                Ativo = professor.Ativo,
            
            };
        }

        private void AtualizarProfessor(Professor professor, ProfessorUpdateDTO dto)
        {
            professor.Nome = dto.Nome;
            professor.Email = dto.Email;
            professor.Especialidade = dto.Especialidade;
            professor.Ativo = dto.Ativo;
        }

      
    }
}
