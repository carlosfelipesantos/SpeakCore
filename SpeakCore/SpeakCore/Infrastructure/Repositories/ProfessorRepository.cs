
using Microsoft.EntityFrameworkCore;
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

        public async Task AdicionarAsync(Professor professor)
        {
           await _context.Professores.AddAsync(professor);
            await _context.SaveChangesAsync();
        }

        public async Task<Professor?> ObterPorIdAsync(int id)
        {
            return await _context.Professores.FirstOrDefaultAsync(i => i.Id == id);
         }

        public async Task<List<Professor>> ObterTodosAsync()
        {
            return await _context.Professores.OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task AtualizarAsync(Professor professor)
        {
             _context.Professores.Update(professor);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Professor professor)
        {
            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PossuiTurmasAsync(int professorId)
        {
            return await _context.Turmas.AnyAsync(t => t.ProfessorId == professorId);
        }

       
    }
}
