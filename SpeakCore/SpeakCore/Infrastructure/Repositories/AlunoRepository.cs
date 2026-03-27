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

        public Task AdicionarAsync(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarAsync(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CpfExisteAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailExisteAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Aluno?> ObterPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Aluno>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PossuiTurmasAsync(int alunoId)
        {
            throw new NotImplementedException();
        }

        public Task RemoverAsync(Aluno aluno)
        {
            throw new NotImplementedException();
        }
    }
}
