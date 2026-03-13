using LearnHomeAPI.Contexts;
using LearnHomeAPI.Domains;
using LearnHomeAPI.Interfaces;
using Microsoft.Identity.Client;

namespace LearnHomeAPI.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly LearnHomeDbContext ctx;
        public CursoRepository(LearnHomeDbContext context)
        {
            ctx = context;
        }

        public List<Curso> Listar()
        {
            return ctx.Curso.ToList();
        }

        public Curso ObterPorId(int id)
        {
            return ctx.Curso.FirstOrDefault(c => c.Id == id);
        }

        public Curso ObterPorNome(string nome)
        {
            return ctx.Curso.FirstOrDefault(c => c.Nome == nome);
        }


        public void Adicionar(Curso curso)
        {
            ctx.Curso.Add(curso);
            ctx.SaveChanges();
        }

        public void Atualizar(int Cursoid, int InstrutorId, Curso curso)
        {
            var cursoExistente = ctx.Curso.FirstOrDefault(c => c.Id == Cursoid);
            if (cursoExistente == null)
                return;

            cursoExistente.Nome = curso.Nome;
            cursoExistente.Descricao = curso.Descricao;

            Instrutor instrutorCurso = ctx.Instrutor.FirstOrDefault(i => i.Id == InstrutorId);
            if (instrutorCurso == null)
                return;

            cursoExistente.Instrutor = (ICollection<Instrutor>)instrutorCurso;
            ctx.SaveChanges();
        }

        public void Remover(int id)
        {
            var curso = ctx.Curso.FirstOrDefault(c => c.Id == id);
            if (curso == null)
                return;

            ctx.Curso.Remove(curso);
            ctx.SaveChanges();
        }
    }
}
