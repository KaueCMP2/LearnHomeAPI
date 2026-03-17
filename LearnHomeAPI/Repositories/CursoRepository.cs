using LearnHomeAPI.Contexts;
using LearnHomeAPI.Domains;
using LearnHomeAPI.Interfaces;
using Microsoft.Identity.Client;
using System.Collections.Immutable;

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
        
        public bool CursoExiste(string nome)
        {
            return ctx.Curso.Any(c => c.Nome == nome);
        }

        public void Adicionar(Curso curso)
        {
            Curso cursoAdicionado = new Curso
            {
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                CargaHoraria = curso.CargaHoraria,
                InstrutorId = curso.InstrutorId
            };

            ctx.Curso.Add(curso);
            ctx.SaveChanges();
        }

        public void Atualizar(int cursoid, Curso curso)
        {
            var cursoExistente = ctx.Curso.FirstOrDefault(c => c.Id == cursoid);
            if (cursoExistente == null)
                return;

            cursoExistente.Nome = curso.Nome;
            cursoExistente.Descricao = curso.Descricao;

            cursoExistente.InstrutorId = curso.InstrutorId;
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
