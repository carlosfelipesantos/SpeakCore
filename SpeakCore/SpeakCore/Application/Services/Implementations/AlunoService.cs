using SpeakCore.Application.DTOs.Aluno;
using SpeakCore.Application.Services.Interfaces;
using SpeakCore.Domain.Entities;
using SpeakCore.Domain.Interfaces;

namespace SpeakCore.Application.Services.Implementations
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;

        public AlunoService(IAlunoRepository alunoRepository, ITurmaRepository turmaRepository)
        {
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
        }
        public async Task<AlunoResponseDTO> AdicionarAsync(AlunoCreateDTO dto)
        {
            
            if (dto.TurmasIds == null || !dto.TurmasIds.Any())
                throw new ArgumentException("O aluno deve estar matriculado em pelo menos uma turma.");

            if (await _alunoRepository.CpfExisteAsync(dto.CPF))
                throw new ArgumentException("CPF já cadastrado.");

            if (!dto.Email.Contains("@"))
                throw new ArgumentException("Email inválido.");

            if (await _alunoRepository.EmailExisteAsync(dto.Email))
                throw new ArgumentException("Email já cadastrado.");

            // Validação  das turmas
            foreach (var turmaId in dto.TurmasIds)
            {
                // se turma existe
                var turma = await _turmaRepository.ObterPorIdAsync(turmaId);
                if (turma == null)
                    throw new KeyNotFoundException($"Turma {turmaId} nao encontrada.");

                //capacidade
                var quantidade = await _turmaRepository.ObterQuantidadeAlunosAsync(turmaId);
                if (quantidade >= 5)
                    throw new InvalidOperationException($"Turma {turmaId} já atingiu o limite de 5 alunos.");
            }

            var aluno = MapParaEntidade(dto);

            await _alunoRepository.AdicionarAsync(aluno);

            return MapParaResponse(aluno);
        }



        public async Task<AlunoResponseDTO?> ObterPorIdAsync(int id)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            if (aluno == null) 
               throw new KeyNotFoundException("Aluno nao encontrado.") ;
            return MapParaResponse(aluno);
        }

        public async Task<List<AlunoResponseDTO>> ObterTodosAsync()
        {
            var alunos = await _alunoRepository.ObterTodosAsync();
            return alunos.Select(a => MapParaResponse(a)).ToList();
        }

        public async Task<AlunoResponseDTO> AtualizarAsync(int id, AlunoUpdateDTO dto)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            if (aluno == null)
                throw new KeyNotFoundException("Aluno nao encontrado.");

            if (dto.TurmasIds == null || !dto.TurmasIds.Any())
                throw new ArgumentException("O aluno deve estar matriculado em pelo menos uma turma.");

            // Validar CPF Email
            if (aluno.CPF != dto.CPF && await _alunoRepository.CpfExisteAsync(dto.CPF))
                throw new ArgumentException("CPF já cadastrado para outro aluno.");

            if (!dto.Email.Contains("@"))
                throw new ArgumentException("Email inválido.");

            if (aluno.Email != dto.Email && await _alunoRepository.EmailExisteAsync(dto.Email))
                throw new ArgumentException("Email já cadastrado para outro aluno.");

          
            await AtualizarAluno(aluno, dto);

            await _alunoRepository.AtualizarAsync(aluno);

            return MapParaResponse(aluno);
        }

        public async Task RemoverAsync(int id)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            if (aluno == null)
                throw new KeyNotFoundException("Aluno não encontrado.");

            if (await _alunoRepository.PossuiTurmasAsync(id))
                throw new InvalidOperationException("Não é possível excluir o aluno, pois ele está matriculado em turmas.");

            await _alunoRepository.RemoverAsync(aluno);
        }

     
        // Mapeamentos
        private Aluno MapParaEntidade(AlunoCreateDTO dto)
        {
            return new Aluno
            {
                CPF = dto.CPF,
                Nome = dto.Nome,
                Email = dto.Email,
                DataNascimento = dto.DataNascimento,
                AlunoTurmas = dto.TurmasIds.Select(turmaId => new AlunoTurma
                {
                    TurmaId = turmaId,
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
                Turmas = aluno.AlunoTurmas.Select(at => at.TurmaId).ToList()
            };
        }

 
        // Atualizar aluno e turmas
        private async Task AtualizarAluno(Aluno aluno, AlunoUpdateDTO dto)
        {
           
            aluno.Nome = dto.Nome;
            aluno.Email = dto.Email;
            aluno.CPF = dto.CPF;

            if (aluno.AlunoTurmas == null)
                aluno.AlunoTurmas = new List<AlunoTurma>();

            if (dto.TurmasIds != null && dto.TurmasIds.Any())
            {
                // Remover turmas caso aluno tenha saido 
                var turmasParaRemover = aluno.AlunoTurmas
                    .Where(at => !dto.TurmasIds.Contains(at.TurmaId))
                    .ToList(); 

                foreach (var at in turmasParaRemover)
                {
                    aluno.AlunoTurmas.Remove(at);
                }

                // Adicionar novas turmas 
                foreach (var turmaId in dto.TurmasIds)
                {
                    if (!aluno.AlunoTurmas.Any(at => at.TurmaId == turmaId))
                    {
                        //valida existencia da turma
                        var turma = await _turmaRepository.ObterPorIdAsync(turmaId);
                        if (turma == null)
                            throw new KeyNotFoundException("Turma nao encontrada.");

                        // Valida capacidade máxima
                        var quantidade = await _turmaRepository.ObterQuantidadeAlunosAsync(turmaId);
                        if (quantidade >= 5)
                            throw new InvalidOperationException(" Essa turma já atingiu o limite de 5 alunos.");

                        aluno.AlunoTurmas.Add(new AlunoTurma
                        {
                            TurmaId = turmaId,
                            DataMatricula = DateTime.Now
                        });
                    }
                }
            }
        }
    }
   }
