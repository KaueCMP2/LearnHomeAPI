using LearnHomeAPI.Domains;
using LearnHomeAPI.Interfaces;
using Microsoft.Identity.Client;

namespace LearnHomeAPI.Applications.Service
{
    public class CursoService
    {
        private readonly ICursoRepository _repository;
        public CursoService(ICursoRepository cursoRepository)
        {
            _repository = cursoRepository;
        }

        public List<Curso> Listar()
        {
            List<Curso> cursos = _repository.Listar();
            if (cursos == null || cursos.Count == 0)
            {
                throw new Exception("Nenhum curso encontrado!");
            }

            return _repository.Listar();
        }

        public Curso ObterPorId(int id)
        {
            Curso curso = _repository.ObterPorId(id);
            if (curso == null)
            {
                throw new Exception("Nenhum curso encontrado!");
            }

            return curso;
        }

        public void Criar(Curso curso)
        {
            Curso cursoBanco = _repository.ObterPorNome(curso.Nome);
            if (cursoBanco != null)
            {
                throw new Exception("Já existe um curso com esse nome!");
            }

            _repository.Criar(curso);
        }

        public void Atualizar(int id, int instrutorId, Curso curso)
        {
            Curso cursoBanco = _repository.ObterPorId(id);
            if (cursoBanco == null)
                throw new Exception("Nenhum curso encontrado para ser atualizado!");
         
            _repository.Atualizar(id, instrutorId, curso);
        }
    }
}