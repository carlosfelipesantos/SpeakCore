using Microsoft.EntityFrameworkCore;
using SpeakCore.Domain.Entities;
using SpeakCore.Domain.Interfaces;
using SpeakCore.Infrastructure.Data;

namespace SpeakCore.Infrastructure.Repositories
{
    public class DisciplinaRepository : IDisciplinaRepository
    {
        private readonly AppDbContext _context;

        public DisciplinaRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Disciplina disciplina)
        {
            _context.Disciplinas.AddAsync(disciplina);
           await _context.SaveChangesAsync();
        }

        public async Task<Disciplina?> ObterPorIdAsync(int id)
        {
            return await _context.Disciplinas.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Disciplina>> ObterTodasAsync()
        {
            return await _context.Disciplinas.OrderBy(d => d.Nome).ToListAsync();
        }


        public async Task AtualizarAsync(Disciplina disciplina)
        {
             _context.Disciplinas.Update(disciplina);
              await _context.SaveChangesAsync();
        }


        public async Task RemoverAsync(Disciplina disciplina)
        {
            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();
        }
    }
}
