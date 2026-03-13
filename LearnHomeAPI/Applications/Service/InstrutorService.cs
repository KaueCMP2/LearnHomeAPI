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
        private readonly IAreaEspecializacaoRepository _areaRepository;
        public InstrutorService(IInstrutorRepository InstrutorRepository, IAreaEspecializacaoRepository areaEspecializacaoRepository)
        {
            _repository = InstrutorRepository;
            _areaRepository = areaEspecializacaoRepository;
        }

        public LerInstrutorDto ConverterInstrutorParaDto(Instrutor instrutor)
        {
            var InstrutorDto = new LerInstrutorDto
            {
                Id = instrutor.Id,
                Nome = instrutor.Nome,
                Email = instrutor.Email,
                AreaEspecializacaoId = instrutor.AreaEspecializacaoId ?? 0,
                AreaEspecializacao = instrutor.AreaEspecializacao
            };
            return InstrutorDto;
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
            List<Instrutor> Instrutores = _repository.Listar();
            if (Instrutores == null)
                throw new DomainException("Nenhum Instrutor para ser listado!");

            List<LerInstrutorDto> InstrutoresDtos = Instrutores.Select(InstrutoresSelecionados => ConverterInstrutorParaDto(InstrutoresSelecionados))
                .ToList();
            if (InstrutoresDtos == null)
                throw new DomainException("Nenhum Instrutor para ser listado!");


            return InstrutoresDtos;
        }

        public LerInstrutorDto ObterPorId(int id)
        {
            Instrutor Instrutor = _repository.ObterPorId(id);
            if (Instrutor == null)
                throw new DomainException("Nenhum Instrutor para ser listado!");

            LerInstrutorDto InstrutorDto = ConverterInstrutorParaDto(Instrutor);

            return InstrutorDto;
        }

        public List<LerInstrutorDto> ObterPorNome(string nome)
        {
            List<Instrutor> instrutor = _repository.ObterPorNome(nome);
            if (instrutor == null)
                throw new DomainException("Nenhum Instrutor para ser listado!");

            List<LerInstrutorDto> InstrutoresDto = instrutor.Select(instrutorSeleiconado => ConverterInstrutorParaDto(instrutorSeleiconado))
                .ToList();

            return InstrutoresDto;
        }

        public List<LerInstrutorDto> ObterPorEmail(string email)
        {
            List<Instrutor> instrutor = _repository.ObterPorEmail(email);
            if (instrutor == null)
                throw new DomainException("Nenhum Instrutor para encontrado!");

            List<LerInstrutorDto> InstrutorDto = instrutor.Select(instrutorSelecionado => ConverterInstrutorParaDto(instrutorSelecionado))
                .ToList();

            return InstrutorDto;
        }

        public Instrutor ConverterDtoParaInstrutor(AdicionarInstrutorDto InstrutorDto)
        {
            Instrutor InstrutorConvert = new Instrutor
            {
                Nome = InstrutorDto.Nome,
                Email = InstrutorDto.Email,
                Senha = HashSenha(InstrutorDto.Senha)
            };
            return InstrutorConvert;
        }
        public LerInstrutorDto Adicionar(AdicionarInstrutorDto instrutor)
        {
            if (_repository.EmailExiste(instrutor.Email))
                throw new DomainException("Já existe um Instrutor com esse email!");

            Instrutor instrutorBanco = new Instrutor
            {
                Nome = instrutor.Nome,
                Email = instrutor.Email,
                Senha = HashSenha(instrutor.Senha),
                AreaEspecializacaoId = instrutor.AreaEspecializacaoId
            };
            _repository.Adicionar(instrutorBanco);
            return ConverterInstrutorParaDto(instrutorBanco);
        }

        public LerInstrutorDto Atualizar(int id, AtualizarInstrutorDto Instrutor)
        {
            Instrutor InstrutorBanco = _repository.ObterPorId(id);
            if (InstrutorBanco == null)
                throw new DomainException("Nenhum Instrutor encontrado para ser atualizado!");

            var area = _areaRepository.ObterPorId(InstrutorBanco.Id);
            if(area == null)
                throw new DomainException("Nenhuma área encontrada!");

            InstrutorBanco.Nome = Instrutor.Nome;
            InstrutorBanco.Email = Instrutor.Email;
            InstrutorBanco.Senha = HashSenha(Instrutor.senha);
            InstrutorBanco.AreaEspecializacao = area;
            InstrutorBanco.AreaEspecializacao.Id = area.Id;

            _repository.Atualizar(id, InstrutorBanco);
            return ConverterInstrutorParaDto(InstrutorBanco);
        }

        public void Remover(int id)
        {
            Instrutor InstrutorBanco = _repository.ObterPorId(id);
            if (InstrutorBanco == null)
                throw new DomainException("Nenhum Instrutor encontrado para ser removido!");

            _repository.Remover(id);
        }
    }
}
