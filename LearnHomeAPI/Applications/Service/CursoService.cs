using LearnHomeAPI.Domains;
using LearnHomeAPI.DTOs.AlunoDto;
using LearnHomeAPI.Exceptions;
using LearnHomeAPI.Interfaces;
using Microsoft.Identity.Client;

namespace LearnHomeAPI.Applications.Service
{
    public class Cursoervice
    {
        private readonly ICursoRepository _repository;
        private readonly IInstrutorRepository _InstrutorRepository;
        public Cursoervice(ICursoRepository cursoRepository, IInstrutorRepository instrutor)
        {
            _repository = cursoRepository;
            _InstrutorRepository = instrutor;
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
            List<Curso> Curso = _repository.Listar();
            if (Curso == null || Curso.Count == 0)
                throw new Exception("Nenhum curso encontrado!");

            List<LerCursoDto> CursoDto = Curso.Select(CursoSelecionado => ConverterCursoParaDto(CursoSelecionado))
                .ToList();

            return CursoDto;
        }

        public LerCursoDto ObterPorId(int id)
        {
            Curso curso = _repository.ObterPorId(id);
            if (curso == null)
                throw new Exception("Nenhum curso encontrado!");

            LerCursoDto cursoDto = ConverterCursoParaDto(curso);

            return cursoDto;
        }

        public List<LerCursoDto> ObterPorNome(string nome)
        {
            List<Curso> curso = _repository.ObterPorNome(nome);
            if (curso == null)
                throw new Exception("Nenhum curso encontrado!");

            List<LerCursoDto> CursoDto = curso.Select(Cursoelecionado => ConverterCursoParaDto(Cursoelecionado))
                .ToList();

            return CursoDto;
        }

        public bool CursoExiste(string nome)
        {
            if (_repository.CursoExiste(nome) == true)
                return true;

            return false;
        }


        public LerCursoDto Adicionar(AdicionarCursoDto cursoDto)
        {
            if (_repository.CursoExiste(cursoDto.Nome) == true)
                throw new DomainException("Já existe um curso com esse nome!");

            var instrutor = _InstrutorRepository.ObterPorId(cursoDto.InstrutorId);

            Curso curso = new Curso
            {
                Nome = cursoDto.Nome,
                Descricao = cursoDto.Descricao,
                CargaHoraria = cursoDto.CargaHoraria,
                InstrutorId = cursoDto.InstrutorId
            };

            _repository.Adicionar(curso);
            return ConverterCursoParaDto(curso);
        }

        public Curso ConverterDtoAtualizarParaCurso(AtualizarCursoDto cursoDto)
        {
            Instrutor Instrutor = _InstrutorRepository.ObterPorId(cursoDto.InstrutorId);
            if (Instrutor == null)
                throw new DomainException("Instrutor invalido");

            var curso = new Curso
            {
                Nome = cursoDto.Nome,
                CargaHoraria = cursoDto.CargaHoraria,
                Descricao = cursoDto.Descricao,
                InstrutorId = cursoDto.InstrutorId
            };

            return curso;
        }

        public void Atualizar(int cursoId, AtualizarCursoDto curso)
        {
            if (_repository.ObterPorId(cursoId) == null)
                throw new DomainException("Nenhum curso encontrado para ser atualizado!");

            _repository.Atualizar(cursoId, ConverterDtoAtualizarParaCurso(curso));
        }

        public void Remover(int id)
        {
            if (_repository.ObterPorId(id) == null)
                throw new DomainException("Nenhum curso encontrado para ser removido!");

            _repository.Remover(id);
        }
    }
}