using LearnHomeAPI.Domains;
using LearnHomeAPI.DTOs.AlunoDto;
using LearnHomeAPI.Exceptions;
using LearnHomeAPI.Interfaces;

namespace LearnHomeAPI.Applications.Service
{
    public class CursoAlunoService
    {
        private readonly ICursoAlunoRepository _repository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IAlunoRepository _alunoRepository;
        public CursoAlunoService(ICursoAlunoRepository repository, ICursoRepository cursoRepository, IAlunoRepository alunoRepository)
        {
            _repository = repository;
            _cursoRepository = cursoRepository;
            _alunoRepository = alunoRepository;
        }

        public LerCursoAlunoDto ConverterCursoAlunoParaDto(CursoAluno cursoAluno)
        {
            var cursoAlunoDto = new LerCursoAlunoDto
            {
                CursoId = cursoAluno.CursoId,
                AlunoId = cursoAluno.AlunoId,
                NumeroMatricula = cursoAluno.NumeroMatricula,
                StatusMatricula = cursoAluno.StatusMatricula
            };
            return cursoAlunoDto;
        }

        public List<LerCursoAlunoDto> Listar()
        {
            List<CursoAluno> cursoAluno = _repository.Listar();
            if (cursoAluno == null)
                throw new DomainException("Nenhum Curso ou aluno encontrado!");

            List<LerCursoAlunoDto> cursoAlunoDto = cursoAluno.Select(cursoAlunoSelecionado => ConverterCursoAlunoParaDto(cursoAlunoSelecionado))
                .ToList();

            return cursoAlunoDto;
        }

        public LerCursoAlunoDto ObterPorId(int cursoId, int alunoId)
        {
            CursoAluno cursoAluno = _repository.ObterPorId(cursoId, alunoId);
            if (cursoAluno == null)
                throw new DomainException("Nenhum curso ou aluno encontrado!");

            LerCursoAlunoDto alunoCursoDto = ConverterCursoAlunoParaDto(cursoAluno);
            return alunoCursoDto;
        }

        public void Adicionar(AdicionarCursoAlunoDto cursoAlunoDto)
        {
            if (cursoAlunoDto.CursoId <= 0 && cursoAlunoDto.AlunoId <= 0)
                throw new DomainException("Dados invalidos!");

            CursoAluno cursoAluno = new CursoAluno
            {
                AlunoId = cursoAlunoDto.AlunoId,
                CursoId = cursoAlunoDto.CursoId,
            };

            _repository.Adicionar(cursoAluno);
        }

        public void Atualizar(int cursoId, int alunoId, AtualizarCursoAlunoDto cursoAlunoDto)
        {
            if (cursoId <= 0 && alunoId <= 0)
                throw new DomainException("Dados invalidos!");

            if (_cursoRepository.ObterPorId(cursoId) == null)
                throw new DomainException("Curso não encontrado!");

            if (_alunoRepository.ObterPorId(alunoId) == null)
                throw new DomainException("Aluno não encontrado!");

            if (_repository.CursoAlunoExiste(cursoId, alunoId) == false)
                throw new DomainException("Relação entre aluno e curso não encontrada!");

            CursoAluno cursoAluno = _repository.ObterPorId(cursoId, alunoId);
            cursoAluno.AlunoId = cursoAlunoDto.AlunoId;
            cursoAluno.CursoId = cursoAlunoDto.CursoId;

            if (_repository.NumeroMatriculaExiste(cursoAlunoDto.NumeroMatricula) == true)
                throw new DomainException("Numero de matricula já cadastrado!");

            cursoAluno.NumeroMatricula = cursoAlunoDto.NumeroMatricula;
            cursoAluno.StatusMatricula = cursoAlunoDto.StatusMatricula;

            _repository.Atualizar(cursoAluno.CursoId, cursoAluno.AlunoId, cursoAluno);
        }

        public void Remover(int cursoId, int alunoId)
        {
            if (_repository.ObterPorId(cursoId, alunoId) == null)
                throw new DomainException("Relação entre aluno e curso não encontrada!");

            _repository.Remover(cursoId, alunoId);
        }
    }
}
