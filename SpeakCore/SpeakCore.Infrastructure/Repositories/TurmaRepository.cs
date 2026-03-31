using Microsoft.EntityFrameworkCore;
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

        public async Task AdicionarAsync(Turma turma)
        {
            await _context.Turmas.AddAsync(turma);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteTurmaPorDisciplinaAsync(int disciplinaId)
        {
            return await _context.Turmas.AnyAsync(t => t.DisciplinaId == disciplinaId);
        }

        public async Task<Turma?> ObterPorIdAsync(int id)
        {
            return await _context.Turmas.Include(p => p.Professor).Include(t => t.Disciplina).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<int> ObterQuantidadeAlunosAsync(int turmaId)
        {
            return await _context.AlunoTurmas.CountAsync(at => at.TurmaId == turmaId && at.Ativo);
        }

        public async Task<List<Turma>> ObterTodosAsync()
        {
            return await _context.Turmas.Include(p => p.Professor).Include(t => t.Disciplina).OrderBy(t => t.AnoLetivo).ToListAsync();
        }

        public async Task AtualizarAsync(Turma turma)
        {
            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Turma turma)
        {
            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
        }

       

        public async Task<bool> PossuiAlunosAsync(int turmaId)
        {
            return await _context.AlunoTurmas.AnyAsync(a => a.TurmaId == turmaId && a.Ativo);
        }

       
    }
}
