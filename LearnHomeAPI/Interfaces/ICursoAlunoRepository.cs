using LearnHomeAPI.Domains;
using LearnHomeAPI.DTOs.AlunoDto;

namespace LearnHomeAPI.Interfaces
{
    public interface ICursoAlunoRepository
    {
        List<CursoAluno> Listar();
        CursoAluno ObterPorId(int cursoId, int alunoId);
        bool CursoAlunoExiste(int cursoId, int alunoId);
        bool NumeroMatriculaExiste(string numeroMatricula);
        void Adicionar(CursoAluno cursoAluno);
        public void Atualizar(int cursoId, int alunoId, CursoAluno cursoAlunoDto);
		void Remover(int cursoId, int alunoId);
    }
}
