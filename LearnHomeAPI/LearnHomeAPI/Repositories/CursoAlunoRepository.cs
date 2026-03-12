using LearnHomeAPI.Contexts;
using LearnHomeAPI.Domains;
using LearnHomeAPI.Interfaces;

namespace LearnHomeAPI.Repositories
{
    public class CursoAlunoRepository : ICursoAlunoRepository
    {
        private readonly LearnHomeDbContext ctx;
        public CursoAlunoRepository(LearnHomeDbContext context)
        {
            ctx = context;
        }

        public List<CursoAluno> Listar()
        {
            return ctx.CursoAluno.ToList();
        }

        public CursoAluno ObterPorId(int? cursoId = null, int? alunoId = null)
        {
            return ctx.CursoAluno.FirstOrDefault(ca => ca.CursoId == cursoId && ca.AlunoId == alunoId);
        }

        public void Criar(int cursoId, int alunoId)
        {
            var cursoAluno = new CursoAluno
            {
                CursoId = cursoId,
                AlunoId = alunoId
            };
            ctx.CursoAluno.Add(cursoAluno);
            ctx.SaveChanges();
        }

        public void Atualizar(int cursoId, int alunoId)
        {
            var cursoAlunoExistente = ctx.CursoAluno.FirstOrDefault(ca => ca.CursoId == cursoId && ca.AlunoId == alunoId);
            if (cursoAlunoExistente != null)
            {
                cursoAlunoExistente.CursoId = cursoId;
                cursoAlunoExistente.AlunoId = alunoId;
                ctx.SaveChanges();
            }
        }

        public void Remover(int cursoId, int alunoId)
        {
            var cursoAluno = ctx.CursoAluno.FirstOrDefault(ca => ca.CursoId == cursoId && ca.AlunoId == alunoId);
            if (cursoAluno != null)
            {
                ctx.CursoAluno.Remove(cursoAluno);
                ctx.SaveChanges();
            }
        }
    }
}
