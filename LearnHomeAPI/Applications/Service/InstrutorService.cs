using LearnHomeAPI.Domains;
using LearnHomeAPI.DTOs.AlunoDto;
using LearnHomeAPI.Exceptions;
using LearnHomeAPI.Interfaces;
using LearnHomeAPI.Repositories;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace LearnHomeAPI.Applications.Service
{
    public class InstrutorService
    {
        private readonly IInstrutorRepository _repository;
        public InstrutorService(IInstrutorRepository instrutorRepository)
        {
            _repository = instrutorRepository;
        }

        public LerInstrutorDto ConverterInstrutorParaDto(Instrutor instrutor)
        {
            var instrutorDto = new LerInstrutorDto
            {
                Id = instrutor.Id,
                Nome = instrutor.Nome,
                Email = instrutor.Email,
                EspecializacaoNome = instrutor.AreaEspecializacao.Area
            };
            return instrutorDto;
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new Exception("Senha invalida!");

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }
            
        public List<LerInstrutorDto> Listar()
        {
            List<Instrutor> instrutores = _repository.Listar();
            if (instrutores == null || instrutores.Count == 0)
                throw new DomainException("Nenhum instrutor para ser listado!");

            List<LerInstrutorDto> instrutorDtos = instrutores.Select(instrutoresSelecionados => ConverterInstrutorParaDto(instrutoresSelecionados))
                .ToList();

            return instrutorDtos;
        }

        public LerInstrutorDto ObterPorId(int id)
        {
            Instrutor instrutor = _repository.ObterPorId(id);
            if (instrutor == null)
                throw new DomainException("Nenhum instrutor para ser listado!");

            LerInstrutorDto instrutorDto = ConverterInstrutorParaDto(instrutor);

            return instrutorDto;
        }

        public LerInstrutorDto ObterPorNome(string nome)
        {
            Instrutor instrutor = _repository.ObterPorNome(nome);
            if (instrutor == null)
                throw new DomainException("Nenhum instrutor para ser listado!");

            LerInstrutorDto instrutorDto = ConverterInstrutorParaDto(instrutor);

            return instrutorDto;
        }

        public LerInstrutorDto ObterPorEmail(string email)
        {
            Instrutor instrutor = _repository.ObterPorEmail(email);
            if (instrutor == null)
                throw new DomainException("Nenhum instrutor para encontrado!");

            LerInstrutorDto instrutorDto = ConverterInstrutorParaDto(instrutor);
            return instrutorDto;
        }

        public Instrutor ConverterDtoParaInstrutor(AdicionarInstrutorDto instrutorDto)
        {
            Instrutor instrutorConvert = new Instrutor
            {
                Id = instrutorDto.Id,
                Nome = instrutorDto.Nome,
                Email = instrutorDto.Email,
                Senha = HashSenha(instrutorDto.Senha)
            };
            return instrutorConvert;
        }
        public void Adicionar(AdicionarInstrutorDto instrutor)
        {
            Instrutor instrutorBanco = _repository.ObterPorNome(instrutor.Nome);
            if (instrutorBanco != null)
                throw new DomainException("Já existe um instrutor com esse nome!");

            instrutorBanco = ConverterDtoParaInstrutor(instrutor);
            _repository.Adicionar(instrutorBanco);
        }

        public void Atualizar(int id, AtualizarInstrutorDto instrutor)
        {
            Instrutor instrutorBanco = _repository.ObterPorId(id);
            if (instrutorBanco == null)
                throw new DomainException("Nenhum instrutor encontrado para ser atualizado!");

            instrutorBanco.Nome = instrutor.Nome;
            instrutorBanco.Email = instrutor.Email;
            instrutorBanco.Senha = HashSenha(instrutor.senha);
            instrutorBanco.AreaEspecializacao = instrutor.AreaEspecializacao;

            _repository.Atualizar(id, instrutorBanco);
        }

        public void Remover(int id)
        {
            Instrutor instrutorBanco = _repository.ObterPorId(id);
            if (instrutorBanco == null)
                throw new DomainException("Nenhum instrutor encontrado para ser removido!");

            _repository.Remover(id);
        }
    }
}
