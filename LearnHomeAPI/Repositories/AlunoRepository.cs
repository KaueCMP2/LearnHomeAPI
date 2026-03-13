using LearnHomeAPI.Contexts;
using LearnHomeAPI.Domains;
using LearnHomeAPI.Interfaces;

namespace LearnHomeAPI.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly LearnHomeDbContext ctx;
        public AlunoRepository(LearnHomeDbContext context)
        {
            ctx = context;
        }

        public List<Aluno> Listar()
        {
            return ctx.Aluno.ToList();
        }

        public Aluno ObterPorId(int id)
        {
            return ctx.Aluno.FirstOrDefault(i => i.Id == id);
        }

        public Aluno ObterPorNome(string nome)
        {
            return ctx.Aluno.FirstOrDefault(a => a.Nome == nome);
        }

        public Aluno ObterPorEmail(string email)
        {
            return ctx.Aluno.FirstOrDefault(i => i.Email == email);
        }

        public bool AlunoExiste(string email)
        {
            return ctx.Aluno.Any(i => i.Email == email);
        }

        public void Adicionar(Aluno aluno)
        {
            ctx.Aluno.Add(aluno);
            ctx.SaveChanges();
        }

        public void Atualizar(int id, Aluno aluno)
        {
            var alunoExistente = ctx.Aluno.FirstOrDefault(i => i.Id == id);
            if (alunoExistente == null)
            {
                return;
            }

            alunoExistente.Nome = aluno.Nome;
            alunoExistente.Email = aluno.Email;
            alunoExistente.Senha = aluno.Senha;
            ctx.SaveChanges();
        }

        public void Remover(int id)
        {
            var aluno = ctx.Aluno.FirstOrDefault(i => i.Id == id);
            if (aluno != null)
            {
                ctx.Aluno.Remove(aluno);
                ctx.SaveChanges();
            }
        }
    }
}
