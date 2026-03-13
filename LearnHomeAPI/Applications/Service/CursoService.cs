using LearnHomeAPI.Domains;
using LearnHomeAPI.DTOs.AlunoDto;
using LearnHomeAPI.Exceptions;
using LearnHomeAPI.Interfaces;
using Microsoft.Identity.Client;

namespace LearnHomeAPI.Applications.Service
{
    public class CursoService
    {
        private readonly ICursoRepository _repository;
        private readonly IInstrutorRepository _instrutorRepository;
        public CursoService(ICursoRepository cursoRepository)
        {
            _repository = cursoRepository;
        }

        public LerCursoDto ConverterCursoParaDto(Curso curso)
        {
            var lerCursoDto = new LerCursoDto
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                CargaHoraria = curso.CargaHoraria
            };
            return lerCursoDto;
        }

        public List<LerCursoDto> Listar()
        {
            List<Curso> cursos = _repository.Listar();
            if (cursos == null || cursos.Count == 0)
                throw new Exception("Nenhum curso encontrado!");

            List<LerCursoDto> cursosDto = cursos.Select(cursosSelecionado => ConverterCursoParaDto(cursosSelecionado))
                .ToList();

            return cursosDto;
        }

        public LerCursoDto ObterPorId(int id)
        {
            Curso curso = _repository.ObterPorId(id);
            if (curso == null)
                throw new Exception("Nenhum curso encontrado!");

            LerCursoDto cursoDto = ConverterCursoParaDto(curso);

            return cursoDto;
        }

        public LerCursoDto ObterPorNome(string nome)
        {
            Curso curso = _repository.ObterPorNome(nome);
            if (curso == null)
                throw new Exception("Nenhum curso encontrado!");

            LerCursoDto cursoDto = ConverterCursoParaDto(curso);

            return cursoDto;
        }

        public Curso ConverterDtoAdicionarParaCurso(AdicionarCursoDto cursoDto)
        {
            Curso curso = new Curso
            {
                Nome = cursoDto.Nome,
                Descricao = cursoDto.Descricao,
                CargaHoraria = cursoDto.CargaHoraria,
                Instrutor = cursoDto.Instrutor
            };
            return curso;
        }

        public void Adicionar(AdicionarCursoDto cursoDto)
        {
            if (_repository.ObterPorNome(cursoDto.Nome) != null)
                throw new Exception("Já existe um curso com esse nome!");

            Curso curso = ConverterDtoAdicionarParaCurso(cursoDto);

            _repository.Adicionar(curso);
        }

        public Curso ConverterDtoAtualizarParaCurso(AtualizarCursoDto cursoDto)
        {
            Instrutor instrutor = _instrutorRepository.ObterPorId(cursoDto.InstrutorId);
            if (instrutor == null)
                throw new DomainException("Instrutor invalido");

            var curso = new Curso
            {
                Id = cursoDto.Id,
                Nome = cursoDto.Nome,
                CargaHoraria = cursoDto.CargaHoraria,
                Descricao = cursoDto.Descricao,
                Instrutor = (ICollection<Instrutor>)instrutor
            };

            return curso;
        }

        public void Atualizar(int cursoId, int instrutorId, AtualizarCursoDto curso)
        {
            if (_repository.ObterPorId(curso.Id) == null)
                throw new DomainException("Nenhum curso encontrado para ser atualizado!");

            _repository.Atualizar(cursoId, instrutorId, ConverterDtoAtualizarParaCurso(curso));
        }

        public void Remover(int id)
        {
            if (_repository.ObterPorId(id) == null)
                throw new DomainException("Nenhum curso encontrado para ser removido!");

            _repository.Remover(id);
        }
    }
}