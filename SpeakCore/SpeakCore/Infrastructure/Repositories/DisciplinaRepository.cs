using Microsoft.AspNetCore.Http.HttpResults;
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
            await _context.Disciplina.AdicionarAsync(disciplina);
        }

        public async Task<Disciplina?> ObterPorIdAsync(int id)
        {
           
        }

        public async Task<List<Disciplina>> ObterTodasAsync()
        {
            
        }


        public async Task AtualizarAsync(Disciplina disciplina)
        {
            
        }


        public async Task RemoverAsync(Disciplina disciplina)
        {
         
        }
    }
}
