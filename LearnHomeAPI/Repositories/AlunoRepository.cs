using LearnHomeAPI.Contexts;
using LearnHomeAPI.Domains;
using LearnHomeAPI.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        public List<Aluno> ObterPorNome(string nome)
        {
            return ctx.Aluno.Where(a => a.Nome.Contains(nome)).ToList();
        }

        public List<Aluno> ObterPorEmail(string email)
        {
            return ctx.Aluno.Where(i => i.Email.Contains(email)).ToList();
        }

        public bool EmailExiste(string email)
        {
            return ctx.Aluno.Any(i => i.Email == email);
        }   

        public bool AlunoExiste(string email)
        {
            return ctx.Aluno.Any(i => i.Email == email);
        }

        public void Adicionar(Aluno aluno)
        {
            Aluno alunoBanco = new Aluno
            {
                Nome = aluno.Nome,
                Email = aluno.Email,
                Senha = aluno.Senha
            };

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
