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
            var area = _areaRepository.ObterPorId(instrutor.AreaEspecializacaoId); 
            var InstrutorDto = new LerInstrutorDto
            {
                Nome = instrutor.Nome,
                Email = instrutor.Email,
                AreaEspecializacaoId = area.Id,
                AreaEspecializacao = area
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

            List<LerInstrutorDto> InstrutorDtos = Instrutores.Select(InstrutoresSelecionados => ConverterInstrutorParaDto(InstrutoresSelecionados))
                .ToList();

            return InstrutorDtos;
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
            if (_repository.EmailExiste(instrutor.Email) == true)
                throw new DomainException("Já existe um Instrutor com esse email!");

            var instrutorBanco = ConverterDtoParaInstrutor(instrutor);
            _repository.Adicionar(instrutorBanco);
            return ConverterInstrutorParaDto(instrutorBanco);
        }

        public LerInstrutorDto Atualizar(int id, AtualizarInstrutorDto Instrutor)
        {
            Instrutor InstrutorBanco = _repository.ObterPorId(id);
            if (InstrutorBanco == null)
                throw new DomainException("Nenhum Instrutor encontrado para ser atualizado!");

            InstrutorBanco.Nome = Instrutor.Nome;
            InstrutorBanco.Email = Instrutor.Email;
            InstrutorBanco.Senha = HashSenha(Instrutor.senha);
            InstrutorBanco.AreaEspecializacao = Instrutor.AreaEspecializacao;

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
