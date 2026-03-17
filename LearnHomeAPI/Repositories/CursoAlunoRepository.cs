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

        public CursoAluno ObterPorId(int cursoId, int alunoId)
        {
            return ctx.CursoAluno.FirstOrDefault(ca => ca.CursoId == cursoId && ca.AlunoId == alunoId);
        }

        public bool CursoAlunoExiste(int cursoId, int alunoId)
        {
            return ctx.CursoAluno.Any(c => c.CursoId == cursoId && c.AlunoId == alunoId);
        }

        public bool NumeroMatriculaExiste(string numeroMatricula)
        {
            return ctx.CursoAluno.Any(c => c.NumeroMatricula == numeroMatricula);
        }

        public void Adicionar(CursoAluno cursoAluno)
        {
            CursoAluno cursoAlunoCriado = new CursoAluno
            {
                CursoId = cursoAluno.CursoId,
                AlunoId = cursoAluno.AlunoId,
                StatusMatricula = cursoAluno.StatusMatricula
            };
            ctx.CursoAluno.Add(cursoAlunoCriado);
            ctx.SaveChanges();
        }

        public void Atualizar(int cursoId, int alunoId, CursoAluno cursoAluno)
        {
            var cursoAlunoExistente = ctx.CursoAluno.FirstOrDefault(ca => ca.CursoId == cursoId && ca.AlunoId == alunoId);
            if (cursoAlunoExistente != null)
            {
                cursoAlunoExistente.CursoId = cursoId;
                cursoAlunoExistente.AlunoId = alunoId;
                cursoAlunoExistente.StatusMatricula = cursoAluno.StatusMatricula;

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
