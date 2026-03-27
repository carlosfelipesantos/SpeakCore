using SpeakCore.Domain.Entities;
using SpeakCore.Domain.Interfaces;
using SpeakCore.Infrastructure.Data;

namespace SpeakCore.Infrastructure.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly AppDbContext _context;

        public TurmaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AdicionarAsync(Turma turma)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarAsync(Turma turma)
        {
            throw new NotImplementedException();
        }

        public Task<Turma?> ObterPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> ObterQuantidadeAlunosAsync(int turmaId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Turma>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PossuiAlunosAsync(int turmaId)
        {
            throw new NotImplementedException();
        }

        public Task RemoverAsync(Turma turma)
        {
            throw new NotImplementedException();
        }
    }
}
