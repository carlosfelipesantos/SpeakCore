using SpeakCore.Domain.Entities;
using SpeakCore.Domain.Interfaces;
using SpeakCore.Infrastructure.Data;

namespace SpeakCore.Infrastructure.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDbContext _context;

        public ProfessorRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AdicionarAsync(Professor professor)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarAsync(Professor professor)
        {
            throw new NotImplementedException();
        }

        public Task<Professor?> ObterPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Professor>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PossuiTurmasAsync(int professorId)
        {
            throw new NotImplementedException();
        }

        public Task RemoverAsync(Professor professor)
        {
            throw new NotImplementedException();
        }
    }
}
