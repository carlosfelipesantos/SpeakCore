using SpeakCore.Application.DTOs.Aluno;
using SpeakCore.Application.Services.Interfaces;
using SpeakCore.Domain.Entities;
using SpeakCore.Domain.Interfaces;
using SpeakCore.Domain.Utils;

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

        public async Task AtualizarStatusMatriculaAsync(int alunoId, int turmaId, bool ativo)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId);
            if (aluno == null)
                throw new KeyNotFoundException("Aluno não encontrado.");

            var matricula = aluno.AlunoTurmas?.FirstOrDefault(at => at.TurmaId == turmaId);
            if (matricula == null)
                throw new KeyNotFoundException("Matrícula não encontrada para esta turma.");

            matricula.Ativo = ativo;
            await _alunoRepository.AtualizarAsync(aluno);
           
        }

        public async Task<AlunoResponseDTO> AdicionarAsync(AlunoCreateDTO dto)
        {
            
            if (dto.TurmasIds == null || !dto.TurmasIds.Any())
                throw new ArgumentException("O aluno deve estar matriculado em pelo menos uma turma.");

            if (await _alunoRepository.CpfExisteAsync(dto.CPF))
                throw new ArgumentException("CPF já cadastrado.");

            if (dto.TurmasIds.Distinct().Count() != dto.TurmasIds.Count)
                throw new ArgumentException("Aluno não pode ser matriculado na mesma turma mais de uma vez.");

            if (!CpfValidator.IsValid(dto.CPF))
                throw new ArgumentException("CPF inválido.");


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

            if (dto.TurmasIds.Distinct().Count() != dto.TurmasIds.Count)
                throw new ArgumentException("Aluno não pode ser matriculado na mesma turma mais de uma vez.");

            if (aluno.CPF != dto.CPF && await _alunoRepository.CpfExisteAsync(dto.CPF))
                throw new ArgumentException("CPF já cadastrado para outro aluno.");

            if (aluno.CPF != dto.CPF)
            {
                if (!CpfValidator.IsValid(dto.CPF))
                    throw new ArgumentException("CPF inválido.");
            }

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
                DataCadastro = DateTime.Now,
                Ativo = true,
                DataNascimento = dto.DataNascimento,
                AlunoTurmas = dto.TurmasIds.Select(turmaId => new AlunoTurma
                {
                    TurmaId = turmaId,
                    DataMatricula = DateTime.Now,
                      Ativo = true
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
                Matriculas = aluno.AlunoTurmas?.Select(at => new MatriculaDTO
                {
                    TurmaId = at.TurmaId,
                    Ativo = at.Ativo,
                    DataMatricula = at.DataMatricula
                }).ToList() ?? new List<MatriculaDTO>()
            };
        }

 
        // Atualizar aluno e turmas
        private async Task AtualizarAluno(Aluno aluno, AlunoUpdateDTO dto)
        {
           
            aluno.Nome = dto.Nome;
            aluno.Email = dto.Email;
            aluno.CPF = dto.CPF;

            //caso lista de turmas esteja vazia
            if (aluno.AlunoTurmas == null)
                aluno.AlunoTurmas = new List<AlunoTurma>();

            //caso a lista atualizada seja diferente da guardada no banco
            if (dto.TurmasIds != null && dto.TurmasIds.Any())
            {
                //remove as turmas antigas
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
                            DataMatricula = DateTime.Now,
                            Ativo = true
                        });
                    }
                }
            }
        }
    }
   }
