using SpeakCore.Application.DTOs.Disciplina;
using SpeakCore.Application.Services.Interfaces;
using SpeakCore.Domain.Entities;
using SpeakCore.Domain.Interfaces;

namespace SpeakCore.Application.Services.Implementations
{
    public class DisciplinaService : IDisciplinaService
    {
        private readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaService(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }
        
        public async Task<DisciplinaResponseDTO> AdicionarAsync(DisciplinaCreateDTO dto)
        {
            if (await _disciplinaRepository.NomeExisteAsync(dto.Nome))
                throw new ArgumentException("Já existe uma disciplina com esse nome.");

            var disciplina = MapParaEntidade(dto);
            await _disciplinaRepository.AdicionarAsync(disciplina);
            return MapParaResponse(disciplina);
        }

        public async Task<DisciplinaResponseDTO?> ObterPorIdAsync(int id)
        {
            var disciplina = await _disciplinaRepository.ObterPorIdAsync(id);
            if (disciplina == null)
                throw new KeyNotFoundException("Disciplina nao encontrada");
            return MapParaResponse(disciplina);
        }

        public async Task<List<DisciplinaResponseDTO>> ObterTodasAsync()
        {
            var disciplinas = await _disciplinaRepository.ObterTodasAsync();
            return disciplinas.Select(d => MapParaResponse(d)).ToList();

        }

        public async Task<DisciplinaResponseDTO> AtualizarAsync(int id, DisciplinaUpdateDTO dto)
        {
            var disciplina = await _disciplinaRepository.ObterPorIdAsync(id);

            if (disciplina == null)
                throw new KeyNotFoundException("Disciplina nao encontrada");

            //so valida duplicidade se o nome tiver mudado
            if (disciplina.Nome != dto.Nome &&
            await _disciplinaRepository.NomeExisteAsync(dto.Nome))
            {
                throw new ArgumentException("Já existe uma disciplina com esse nome.");
            }

            AtualizarDisciplina(disciplina, dto);

            await _disciplinaRepository.AtualizarAsync(disciplina);

            return MapParaResponse(disciplina);

        }

        public async Task RemoverAsync(int id)
        {
            var disciplina = await _disciplinaRepository.ObterPorIdAsync(id);

            if (disciplina == null)
                throw new KeyNotFoundException("Disciplina nao encontrada");


            await _disciplinaRepository.RemoverAsync(disciplina);
        }


        //Mapeamentos
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

        private void AtualizarDisciplina(Disciplina disciplina, DisciplinaUpdateDTO dto)
        {
             
                disciplina.Nome = dto.Nome;
                disciplina.Descricao = dto.Descricao;
                disciplina.Ativo = dto.Ativo;
            
        }
    }
}
