using LearnHomeAPI.Domains;
using LearnHomeAPI.DTOs.AlunoDto;
using LearnHomeAPI.Exceptions;
using LearnHomeAPI.Interfaces;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using System.Text;

namespace LearnHomeAPI.Applications.Service
{
    public class AlunoService
    {
        private readonly IAlunoRepository _repository;
        public AlunoService(IAlunoRepository repository)
        {
            _repository = repository;
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new Exception("Senha invalida!");

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerAlunoDto ConverterAlunoParaDto(Aluno aluno)
        {
            LerAlunoDto alunoDto = new LerAlunoDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Email = aluno.Email,
            };

            return alunoDto;
        }

        public Aluno ConverterDtoParaAluno(AdicionarAlunoDto alunoDto)
        {
            Aluno aluno = new Aluno
            {
                Nome = alunoDto.Nome,
                Email = alunoDto.Email,
                Senha = HashSenha(alunoDto.Senha)
            };

            return aluno;
        }

        public List<LerAlunoDto> Listar()
        {
            List<Aluno> alunos = _repository.Listar();
            if (_repository.Listar == null)
                throw new DomainException("Nenhum aluno encontrada!");

            List<LerAlunoDto> alunosDto = alunos.Select(alunoSelecionado => ConverterAlunoParaDto(alunoSelecionado))
                .ToList();

            return alunosDto;
        }

        public LerAlunoDto ObterPorId(int id)
        {
            Aluno aluno = _repository.ObterPorId(id);
            if (aluno == null)
                throw new DomainException("Aluno não encontrado!");

            LerAlunoDto alunoDto = ConverterAlunoParaDto(aluno);
            return alunoDto;
        }

        public List<LerAlunoDto> ObterPorNome(string nome)
        {
            List<Aluno> aluno = _repository.ObterPorNome(nome);

            if (aluno == null)
                throw new DomainException("Nenhum aluno encontrado!");

            List<LerAlunoDto> alunosDto = aluno.Select(alunoSelecionado => ConverterAlunoParaDto(alunoSelecionado))
                .ToList();

            return alunosDto;
        }

        public List<LerAlunoDto> ObterPorEmail(string email)
        {
            List<Aluno> aluno = _repository.ObterPorEmail(email);
            if (aluno == null)
                throw new DomainException("Nenhum aluno encontrado!");

            List<LerAlunoDto> alunosDto = aluno.Select(alunoSelecionado => ConverterAlunoParaDto(alunoSelecionado))
                .ToList();

            return alunosDto;
        }

        public LerAlunoDto Adicionar(AdicionarAlunoDto alunoDto)
        {
            if (_repository.AlunoExiste(alunoDto.Email) == true)
                throw new DomainException("email já cadastrado");

            Aluno aluno = new Aluno
            {
                Nome = alunoDto.Nome,
                Email = alunoDto.Email,
                Senha = HashSenha(alunoDto.Senha)
            };

            _repository.Adicionar(aluno);
            return ConverterAlunoParaDto(aluno);
        }

        public LerAlunoDto Atualizar(int id, AtualizarAlunoDto aluno)
        {
            Aluno alunoBanco = _repository.ObterPorId(id);
            if (alunoBanco == null)
                throw new DomainException("Nenhum aluno encontrado!");

            alunoBanco.Nome = aluno.Nome;

            if (_repository.ObterPorEmail(aluno.Email) != null)
                throw new DomainException("Email em uso!");

            alunoBanco.Email = aluno.Email;
            alunoBanco.Senha = HashSenha(aluno.Senha);

            _repository.Atualizar(id, alunoBanco);
            return ConverterAlunoParaDto(alunoBanco);
        }

        public void Remover(int id)
        {
            Aluno alunoBanco = _repository.ObterPorId(id);
            if (alunoBanco == null)
                throw new DomainException("Nenhum aluno encontrado para ser removido!");

            _repository.Remover(id);
        }
    }
}
