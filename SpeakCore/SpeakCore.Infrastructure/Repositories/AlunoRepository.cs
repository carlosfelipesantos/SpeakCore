using Microsoft.EntityFrameworkCore;
using SpeakCore.Domain.Entities;
using SpeakCore.Domain.Interfaces;
using SpeakCore.Infrastructure.Data;

namespace SpeakCore.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task<Aluno?> ObterPorIdAsync(int id)
        {
            return await _context.Alunos.Include(a => a.AlunoTurmas).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Aluno>> ObterTodosAsync()
        {
            return await _context.Alunos.Include(a => a.AlunoTurmas).OrderBy(t => t.Nome).ToListAsync();
        }

        public async Task AtualizarAsync(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
           await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CpfExisteAsync(string cpf)
        {
            return await _context.Alunos.AnyAsync(a => a.CPF == cpf);
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _context.Alunos.AnyAsync(e => e.Email == email);
        }

       

        public async Task<bool> PossuiTurmasAsync(int alunoId)
        {
            return await _context.AlunoTurmas.AnyAsync(p => p.AlunoId == alunoId);
        }

      
    }
}
