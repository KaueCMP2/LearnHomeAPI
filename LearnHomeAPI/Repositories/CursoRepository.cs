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

        public List<Curso> ObterPorNome(string nome)
        {
            return ctx.Curso.Where(a => a.Nome.Contains(nome)).ToList();

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

            Instrutor InstrutorCurso = ctx.Instrutor.FirstOrDefault(i => i.Id == InstrutorId);
            if (InstrutorCurso == null)
                return;

            cursoExistente.Instrutor = (ICollection<Instrutor>)InstrutorCurso;
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
